var auth = require('./auth');
var controllers = require('../controllers');
var gameLevels = require('../data/levels');
var Levels = require('mongoose').model('Levels');


module.exports = function (app) {
    app.get('/register', controllers.users.getRegister);
    app.post('/register', controllers.users.postRegister);

    app.get('/login', controllers.users.getLogin);
    app.post('/login', auth.login);
    app.get('/logout', auth.logout);

    app.get('/', function (req, res) {
        res.render('main', {currentUser: req.user});
    });

    app.get('*', function (req, res) {
        res.render('main', {currentUser: req.user});

    });

    app.get('/game', function (req, res) {
        // get all current rooms on the server
        var gameRooms = require('../logic/gameRoom').gameRooms;

        // rooms for display
        var rooms = [], levels = [];

        // add all current game rooms on the server
        for (var roomId in gameRooms) {
            rooms.push({
                roomId: gameRooms[roomId].roomId,
                roomName: gameRooms[roomId].roomName,
                description: gameRooms[roomId].description,
                levelId: gameRooms[roomId].levelId,
                playersIn: Object.keys(gameRooms[roomId].gameServer.players).length -
                gameRooms[roomId].gameServer.freePlayers.length,
                allPlayers: Object.keys(gameRooms[roomId].gameServer.players).length
            });
        }

        // add levels
        for (var levelId in gameLevels) {
            levels.push(levelId);
        }

        res.render('main', {title: "Test", gameRooms: rooms, levels: levels});
    });

    //add new level in Db

    app.post('/addLevel',function(req, res, next){

        var level = new Levels({
           level:req.body.level
        })

        level.save(function(err, newlevel){

           if(err) return next(err);

            res.render('main', {title: "Test", gameRooms: rooms, levels: newlevel});

        })
    });




};