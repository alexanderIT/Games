var Levels = require('mongoose').model('Levels');

var levels = {};

var level_0 = new Levels();
level_0.level = [
    "  ###",
    "  #.#",
    "  # ####",
    "###$ $.#",
    "#. $0###",
    "####$#",
    "   #.#",
    "   ###"
];
level_0.save(function (err, level) {
    if (err) return console.log(err);
});

levels[0] = Levels.find('level_0', function (err, data) {
    if (err) return console.log(err);
    return data;
});


var level_1 = new Levels();
level_0.level = [
    "#####",
    "#   #",
    "# $ # ###",
    "# $0# #.#",
    "###$###.#",
    " ##    .#",
    " #1  #  #",
    " #   ####",
    " #####"
];
level_1.save(function (err, level) {
    if (err) return console.log(err);
});

levels[1] = Levels.find('level_1', function (err, data) {
    if (err) return console.log(err);
    return data;
});

var level_2 = new Levels();
level_2.level = [
    "   ###",
    "  ## # ####",
    " ##  ###  #",
    "## $      #",
    "#   0$ #  #",
    "### $###  #",
    "  #  #..  #",
    " ## ##.# ##",
    " #      ##",
    " #     ##",
    " #######"
];
level_2.save(function (err, level) {
    if (err) return console.log(err);
});

levels[2] = Levels.find('level_2', function (err, data) {
    if (err) return console.log(err);
    return data;
});

var level_3 = new Levels();
level_3.level = [
    "   #######",
    "####1    #",
    "#   .### #",
    "# # #    ##",
    "# # $ $#. #",
    "# #  *  # #",
    "# .#$ $ # #",
    "##    # # ###",
    " # ###.    0#",
    " #2    ##   #",
    " ############"
];
level_3.save(function (err, level) {
    if (err) return console.log(err);
});
levels[3] = Levels.find('level_3', function (err, data) {
    if (err) return console.log(err);
    return data;
});


module.exports = levels;




