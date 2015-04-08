
var GameRoom = require('./room').GameRoom;
var getGameRoom = require('./room').getGameRoom;
var levels = require('../server/data/levels');

var io = require('socket.io')();


// Represents a connection events for incoming socket.

io.on('connection', function (socket) {

    console.log('a user connected with id: ' + socket.id);


    //Event from client for creating a new game room.

    socket.on('createGameRoom', function (roomName, description, levelId, userId) {

        var gameRoom = Object.create(GameRoom.prototype);
        GameRoom.call(gameRoom, roomName, levelId, description, userId, socket.id);

        // check if user is already a member of room
        if (socket.roomId) {
            var joinedGameRoom = getGameRoom(socket.roomId);
            joinedGameRoom.removeUserFromGame(socket, io);
            socket.leave(socket.roomId);
        }

        // join the room on socket level
        socket.join(gameRoom.roomId);
        socket.roomId = gameRoom.roomId;

        // send starting state to the creator
        socket.emit('gameServerState', gameRoom.gameServerState(gameRoom.owner));

        if (gameRoom.checkAllPlayersJoined()) {

            io.sockets.in(socket.roomId).emit('gameEnabled', true);
        }

        // show the new room to all players, even the one
        io.sockets.emit('newGameRoom', {
            roomId: gameRoom.roomId,
            roomName: gameRoom.roomName,
            description: gameRoom.description,
            levelId: gameRoom.gameServer.levelId,
            playersIn: Object.keys(gameRoom.gameServer.players).length -
            gameRoom.gameServer.freePlayers.length,
            allPlayers: Object.keys(gameRoom.gameServer.players).length
        });
    });


    //Event for joining a game room.
    socket.on('joinGameRoom', function (roomId, userId) {
        // first we need to get the game room from the passed id
        var gameRoom = getGameRoom(roomId);

        if (!gameRoom) {
            return false;
        }

        if (gameRoom.gameServer.freePlayers.length == 0) {
            return false;
        }

        var user = gameRoom.joinGameRoom(userId, socket.id);

        if (socket.roomId) {
            var joinedGameRoom = getGameRoom(socket.roomId);

            // call logic for handling user removal
            joinedGameRoom.removeUserFromGame(socket, io);
            socket.leave(socket.roomId);
        }

        socket.join(roomId);
        socket.roomId = roomId;

        socket.emit('gameServerState', gameRoom.gameServerState(user));

        socket.broadcast.to(socket.roomId).emit('userJoined', user.id, user.player);

        if (gameRoom.checkAllPlayersJoined()) {
            // broadcast that game is enabled to all players
            io.sockets.in(socket.roomId).emit('gameEnabled', true);
        }

        // update the number of players in game.
        io.sockets.emit('updatePlayersIn', {
            roomId: roomId,
            playersIn: Object.keys(gameRoom.gameServer.players).length -
            gameRoom.gameServer.freePlayers.length,
            allPlayers: Object.keys(gameRoom.gameServer.players).length
        });
    });


    //Event for executing a action, where action can be:
    //- UP
    //- DOWN
    //- LEFT
    //- RIGHT

    socket.on('executeAction', function (actionName, blocks, players, callback) {
        console.log('action executed:', actionName, 'from user', socket.id);

        // first we need to get game room of the socket
        var gameRoom = getGameRoom(socket.roomId);

        if (!gameRoom) {
            return false;
        }

        // Execute given action on server. If the action is
        var isExecuted = gameRoom.gameServer.checkExecuteAction(actionName, gameRoom.users[socket.id].player.id);
        if (!isExecuted) {

            callback({
                'synchronized': false,
                'blocks': gameRoom.gameServer.blocks,
                'players': gameRoom.gameServer.players
            });
            return false;
        }

        // the client action is executable, but now we need to check if game
        if (gameRoom.gameServer.synchronized(blocks, players)) {
            callback({'synchronized': true});
        } else {

            // the game state on client side does not match with game state on
            callback({
                'synchronized': false,
                'blocks': gameRoom.gameServer.blocks,
                'players': gameRoom.gameServer.players
            });
        }

        if (gameRoom.gameServer.solved()) {
            io.sockets.in(socket.roomId).emit('chatMessage', {message: "SOLVED!"});
        }

        // emit new move to all other players (broadcast) for given room.
        socket.broadcast.to(socket.roomId).emit('newMove', actionName, gameRoom.gameServer.blocks,
            gameRoom.gameServer.players, gameRoom.users[socket.id].player.id);
    });


    //Event when user disconnects.

    socket.on('disconnect', function () {
        if (!socket.roomId) {
            console.log('user without being connected to game room disconnected');
            return false;
        }

        var gameRoom = getGameRoom(socket.roomId);

        if (!gameRoom) {
            return false;
        }

        gameRoom.removeUserFromGame(socket, io);
        socket.leave(socket.roomId);

        console.log('user with game room disconnected');
    });


    //Event for restarting a game server.
    socket.on('restart', function () {
        var gameRoom = getGameRoom(socket.roomId);

        if (!gameRoom) {
            return false;
        }

        // set them to empty
        gameRoom.gameServer.blocks = {};
        gameRoom.gameServer.players = {};

        // reread starting game state
        var gameState = gameRoom.gameServer.setGameStateFromImage(levels[gameRoom.gameServer.levelId]);

        gameRoom.gameServer.blocks = gameState.blocks;
        gameRoom.gameServer.players = gameState.players;

        // send message to everyone in the game room, including the sender
        io.sockets.in(socket.roomId).emit('restart', gameRoom.gameServer.blocks, gameRoom.gameServer.players);
    });

});

module.exports = io;