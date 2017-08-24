using ContactApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public bool IsFavorite { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime Dob { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public string Rel { get; set; }
        public string Des { get; set; }
    }
}