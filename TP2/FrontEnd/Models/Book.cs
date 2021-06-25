using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace FrontEnd.Models
{
    public class Book
    {
        [JsonPropertyName("ISBN")]
        public string isbn { get; set; }
        
        [JsonPropertyName("Book-Title")]
        public string title { get; set;}

        [JsonPropertyName("Book-Author")]
        public string autor { get; set; }

        [JsonPropertyName("Year-Of-Publication")]
        public string yearRelease { get; set; }

        [JsonPropertyName("Publisher")]
        public string publisher { get; set; }

        [JsonPropertyName("Image-URL-S")]
        public string img_Url_Small { get; set; }

        [JsonPropertyName("Image-URL-M")]
        public string img_Url_Medium { get; set; }

        [JsonPropertyName("Image-URL-L")]
        public string img_Url_Big { get; set; }


       
    }

}

