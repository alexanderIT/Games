
var User = require('./user');
var GameServer = require('./server');
var uuid = require('node-uuid');


var gameRooms = {};


var GameRoom = function (roomName, levelId, description, userId, socketId) {

    this.roomId = uuid.v1();

    this.roomName = roomName;

    // create a game on server side for specified level

    this.gameServer = Object.create(GameServer.prototype);
    GameServer.call(this.gameServer, levelId);

    this.description = description;

    //all users that are currently connected to this room
    this.users = {};


    this.owner = this.joinGameRoom(userId, socketId);

    gameRooms[this.roomId] = this;
};


GameRoom.prototype.joinGameRoom = function (userId, socketId) {

    // pop the first available player. If the player is not
    // available, then it will get undefined, which is fine
    var freePlayerId = this.gameServer.freePlayers.pop();
    var player = this.gameServer.players[freePlayerId];
    var user = Object.create(User.prototype);
    User.call(user, userId, socketId, player);
    this.users[socketId] = user;

    // also check if there is no owner. If it is missing,
    // then set it to the current user.
    if (this.owner === undefined) {
        this.owner = user;
    }
    return user;
};


GameRoom.prototype.checkAllPlayersJoined = function () {

    // once the game is enabled do not check if all players are in
    if (this.gameServer.enabled) {
        return true;
    }

    // otherwise check for number of free players
    this.gameServer.enabled = (this.gameServer.freePlayers.length == 0);
    return this.gameServer.enabled;
};


GameRoom.prototype.gameServerState = function (user) {

    var users = {};
    for (var socketId in this.users) {
        users[this.users[socketId].id] = this.users[socketId].player;
    }
    this.gameServer.game
    return {
        roomId: this.roomId,
        player: user.player,
        users: users,
        stones: this.gameServer.stones,
        blocks: this.gameServer.blocks,
        placeholders: this.gameServer.placeholders,
        players: this.gameServer.players
    };
};


GameRoom.prototype.removeUserFromGame = function (socket, io) {
    var user = this.users[socket.id];

    // transfer ownership of the game room
    this.transferOwnership(socket.id);

    // if owner is still undefined, then there is no player left,
    // delete the game room
    if (this.owner === undefined) {
        var roomId = socket.roomId;

        // delete it after 5s
        setTimeout(function () {

            // we need to check if in the meantime the ownership of the game
            // room was taken by some other user
            if (this.owner === undefined) {
                delete gameRooms[roomId];
                io.sockets.emit('deleteRoom', roomId);
            }
        }, 5000);
    }

    // free player from game
    this.gameServer.freePlayers.push(this.users[socket.id].player.id);

    // remove user from game room
    delete this.users[socket.id];

    io.sockets.emit('updatePlayersIn', {
        roomId: this.roomId,
        playersIn: Object.keys(this.gameServer.players).length -
        this.gameServer.freePlayers.length
    });

    // broadcast to other users in the same room that the user has left
    // the game room
    socket.broadcast.to(this.roomId).emit('userLeft', user.id);
};


GameRoom.prototype.transferOwnership = function (disconnectedSocketId) {

    // check if disconnected user is owner of the room
    if (this.owner.socketId == disconnectedSocketId) {

        this.owner = undefined;

        for (var socketId in this.users) {
            if (this.users[socketId].socketId != disconnectedSocketId) {
                this.owner = this.users[socketId];
                break;
            }
        }
    }
};


var getGameRoom = function (roomId) {
    return gameRooms[roomId];
};


var deleteGameRoom = function (roomId) {
    delete gameRooms[roomId];
};

module.exports = {
    gameRooms: gameRooms,
    GameRoom: GameRoom,
    getGameRoom: getGameRoom,
    deleteGameRoom: deleteGameRoom
};
//