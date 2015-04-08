var Game = function (stones, blocks, placeholders, players) {
    this.stones = stones;
    this.blocks = blocks;
    this.placeholders = placeholders;
    this.players = players;
    this.enabled = false;
};


Game.prototype.actions = Object.freeze({
    UP: [0, -1],
    DOWN: [0, 1],
    LEFT: [-1, 0],
    RIGHT: [1, 0]
});


Game.prototype.solved = function () {
    for (var block in this.blocks) {
        if (!(block in this.placeholders))
            return false;
    }
    return true;
};


Game.prototype.executeAction = function (action, playerId) {

    var playerPosition = this.players[playerId].position;
    var newPlayerPosition = this.newPosition(playerPosition, action);


    if (newPlayerPosition in this.stones) {
        return false;
    }

    if (newPlayerPosition in this.blocks) {

        var newBlockPosition = this.newPosition(newPlayerPosition, action);


        if (newBlockPosition in this.blocks || newBlockPosition in this.stones ||
            this.inPlayer(newBlockPosition)) {
            return false;
        }


        this.blocks[newBlockPosition] = newBlockPosition;


        delete this.blocks[newPlayerPosition];
    }


    else if (this.inPlayer(newPlayerPosition)) {

        var newOppositePlayerPosition = this.newPosition(newPlayerPosition, action);

        if (newOppositePlayerPosition in this.blocks ||newOppositePlayerPosition in this.stones ||
            this.inPlayer(newOppositePlayerPosition)) {
            return false;
        }


        var oppositePlayerId = this.getPlayerId(newPlayerPosition);
        this.players[oppositePlayerId].position = newOppositePlayerPosition;
    }


    this.players[playerId].position = newPlayerPosition;

    return true;
};


Game.prototype.synchronized = function (blocks, players) {
    for (var block in this.blocks) {
        if (!(block in blocks)) {
            return false;
        }
    }

    for (var player in this.players) {
        if (!(this.players[player].position[0] === players[player].position[0] &&
            this.players[player].position[1] === players[player].position[1])) {
            return false;
        }
    }

    return true;
};


Game.prototype.inPlayer = function (position) {
    for (var player in this.players) {
        if (this.players[player].position[0] === position[0] &&
            this.players[player].position[1] === position[1]) {
            return true;
        }
    }
    return false;
};


Game.prototype.getPlayerId = function (position) {
    for (var player in this.players) {
        if (this.players[player].position[0] === position[0] &&
            this.players[player].position[1] === position[1]) {
            return this.players[player].id;
        }
    }
    return undefined;
};


Game.prototype.newPosition = function (position, action) {
    return [position[0] + action[0], position[1] + action[1]];
};


if (typeof module !== "undefined" && module.exports) {

    module.exports = Game;
}