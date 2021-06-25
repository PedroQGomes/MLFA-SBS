using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace FrontEnd.Models
{
    public class User
    {

        [JsonPropertyName("User-ID")]
        public string id { get; set; }

        [JsonPropertyName("Location")]
        public string country { get; set; }

        [JsonPropertyName("favorit_author")]
        public string favorit_author { get; set; }

    }
}
