using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace FrontEnd.Models
{
    public class Rating
    {
        [JsonPropertyName("User-ID")]
        public string UserID { get;set; }

        [JsonPropertyName("ISBN")]
        public string bookIsbn { get; set; }

        [JsonPropertyName("Book-Rating")]
        public int rating { get; set; }

    }
}
