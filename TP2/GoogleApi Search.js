//js request
//
const axios = require('axios');
const fs = require('fs');
const inputFile = 'input2.csv';
const CsvReader = require('promised-csv');
var isbn = require('node-isbn');


var fields = ['.items[0].volumeInfo.industryIdentifiers[0].identifier', '.items[0].volumeInfo.categories[0]', '.items[0].volumeInfo.maturityRating', '.items[0].volumeInfo.language'];
var fieldsName = ['ISBN', 'Category', 'MatureRating', 'Language'];
//var parse = require('csv-parse');
var i = 1234;
var dataString = fs.readFileSync("input_continuacao.csv").toString();
var dataArray = dataString.split('\n');
console.log(dataArray);
dataArray.forEach(function(value){
    var data = isbn.resolve(value, { timeout: 15000 }, function (err, book) {
          if (err) {
          console.log('Book not found', err);
      } else {
          fs.appendFile('output.csv', value + ';' + book.categories[0] + ';' + book.maturityRating + ';' + book.language + '\n' , function (err) { //
              console.log('Book found %j', book);
            });
      }
    });
});
