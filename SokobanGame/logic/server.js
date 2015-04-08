var Game = require('../public/javascripts/game');
var Player = require('./player');
var levels = require('../server/data/levels');


//Creates a new instance of class that represents game state on server side.

var GameServer = function (levelId) {
    this.createdAt = Date.now();

    // set the game state from image
    var gameState = this.setGameStateFromImage(levels[levelId]);

    // call the parent constructor game, to set the game state
    Game.call(this, gameState.stones, gameState.blocks,
        gameState.placeholders, gameState.players);


    this.freePlayers = gameState.freePlayers;
    this.levelId = levelId;
};

// create a GameServer.prototype that inherits from Game.prototype
GameServer.prototype = Object.create(Game.prototype);

GameServer.prototype.constructor = GameServer;

//Check for execution of given action.
GameServer.prototype.checkExecuteAction = function (actionName, playerId) {

    if (!(actionName in this.actions)) {
        return false;
    }

    // execute action for given user
    return this.executeAction(this.actions[actionName], playerId);
};


//Set the game state from image.

GameServer.prototype.setGameStateFromImage = function (levelImage) {
    var gameState = {
        stones: {},
        blocks: {},
        placeholders: {},
        players: {},
        freePlayers: []
    };

    // 10 different colors for players
    var colors = ["#00008B", "#8B0000", "#006400", "#000000", "#FF8C00", "#9400D3",
        "#00CED1", "#556B2F", "#B8860B", "#A9A9A9"];

    // set appropriate functions for given character
    var setFunction = {
        '.': function (position) {
            gameState.placeholders[position] = position;
        },
        '#': function (position) {
            gameState.stones[position] = position;
        },
        '$': function (position) {
            gameState.blocks[position] = position;
        },
        '*': function (position) {
            this['.'](position);
            this['$'](position);
        }
    };

    // game currently enables up to 9 different players
    for (var i = 0; i <= 9; i++) {
        setFunction[i] = function (position) {
            var playerId = levelImage[position[1]][position[0]];

            // create a new player
            var player = Object.create(Player.prototype);

            // call a constructor on the new player
            Player.call(player, playerId, position, colors[playerId]);

            // assign a player to id
            gameState.players[playerId] = player;

            // hold a list of available players
            gameState.freePlayers.push(playerId);
        }
    }

    // read image, first iterate over rows
    for (var y = 0; y < levelImage.length; y++) {
        // then over columns
        for (var x = 0; x < levelImage[y].length; x++) {

            if (levelImage[y][x] in setFunction) {
                setFunction[levelImage[y][x]]([x, y]);
            }
        }
    }

    // return the game state read from image
    return gameState;
};

module.exports = GameServer;
