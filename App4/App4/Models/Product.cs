
using Newtonsoft.Json;
using System;

namespace App4.Models
{
    public class Product
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVer { get; set; }

        public DateTime Date { get; set; }

        public string EmotionString { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay { get { return Date.ToLocalTime().ToString("d"); } }

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay { get { return Date.ToLocalTime().ToString("t"); } }
    }
}
