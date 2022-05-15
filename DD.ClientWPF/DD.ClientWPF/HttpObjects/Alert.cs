using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.ClientWPF
{
    internal class Alert
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("header")]
        public string Header { get; set; } = "Header";

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = "Description";

        [JsonProperty("idSystem")]
        public string IdSystem { get; set; }

        [JsonProperty("worker")]
        public string Worker { get; set; }

        [JsonProperty("commentToClose")]
        public string CommentToClose { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; } = 0;
        
        public Alert() { }
    }
}
