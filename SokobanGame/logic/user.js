
//Creates a new instance of class that represents User.

var User = function (id, socketId, player) {
    this.id = id;
    this.socketId = socketId;
    this.player = player;
};

module.exports = User;