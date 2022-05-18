using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.ClientWPF.HttpObjects
{
    internal class CompositionOfTheShift
    {
        [JsonProperty("systemGroup")]
        public string SystemGroup { get; set; }

        [JsonProperty("people")]
        public string[] People { get; set; }

        public CompositionOfTheShift() { }
    }
}
