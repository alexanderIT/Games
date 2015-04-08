var express = require('express');
var io = require('./logic/io');

var env = process.env.NODE_ENV || 'development';

var app = express();
var config = require('./server/config/config')[env];

require('./server/config/express')(app, config);
require('./server/config/mongoose')(config);
require('./server/config/passport')();
require('./server/config/routes')(app);

var server = app.listen(config.port, function () {

    console.log("Server running on port: " + config.port
    )
});

io.attach(server);