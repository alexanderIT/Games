var mongoose = require('mongoose'),
    UserModel = require('../data/models/User'),
    LevelsModel = require('../data/models/Level');


module.exports = function (config) {

    //connect to local mongodb database
    mongoose.connect(config.db);
    var db = mongoose.connection;

    //attache lister to connection event
    db.once('open', function (err) {
        if (err) {
            console.log('Database could not be opened: ' + err);
            return;
        }
        console.log('Database up and running...')
    });

    db.on('error', function (err) {
        console.log('Database error: ' + err);
    });

    UserModel.init();
    LevelsModel.init();
};