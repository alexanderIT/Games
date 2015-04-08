var mongoose = require('mongoose');

module.exports.init = function () {

    var levelsSchema = mongoose.Schema({
        level: [String]
    });

    var Levels = mongoose.model('Levels', levelsSchema);
};





