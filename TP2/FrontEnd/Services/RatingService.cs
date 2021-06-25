using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic.FileIO;

namespace FrontEnd.Services
{
    public class RatingService
    {

        private IWebHostEnvironment webHost { get; }

        private List<Rating> ratings;


        private string ratingPath
        {
            get { return Path.Combine(webHost.ContentRootPath, "Teste", "BX-Book-Ratings.csv"); }
        }


        public RatingService(IWebHostEnvironment webh)
        {

            this.webHost = webh;
            this.ratings = readRatings();

        }


        private List<Rating> readRatings()
        {
            List<Rating> lista = new List<Rating>(); 
            /*
            using (TextFieldParser parser = new TextFieldParser(ratingPath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.ReadLine();
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    try {
                        string[] fields = parser.ReadFields();
                        Rating r = new Rating();
                        r.UserID = fields[0];
                        r.bookIsbn = fields[1];
                        r.rating = Int32.Parse(fields[2]);
                        lista.Add(r);
                    }catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    

                }
            }*/

            return lista;

        }





        public void addRating(string userid, string isbn,int rating)
        {
            foreach (var entry in ratings)
            {
                if (entry.bookIsbn.Equals(isbn))
                {
                    entry.rating = rating;
                }
            }

        }

        public int getRating(string userid,string isbn)
        {
            foreach(var entry in ratings)
            {
                if (entry.bookIsbn.Equals(isbn))
                {
                    return entry.rating;
                }
            }
            return 0;
        }

    }
}
