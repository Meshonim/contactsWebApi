using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactApp.Models
{
    public class PatchData
    {
        [JsonProperty("status")]
        public bool IsFavorite { get; set; }
    }
}