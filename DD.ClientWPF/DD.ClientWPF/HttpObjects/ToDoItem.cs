using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.ClientWPF.HttpObjects
{
    internal class ToDoItem
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("nameTask")]
        public string TaskName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = "Description";

        [JsonProperty("systemName")]
        public string SystemName { get; set; }

        [JsonProperty("createTime")]
        public DateTime CreationTime { get; set; }

        [JsonProperty("worker")]
        public string Worker { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; } = 0;

        public ToDoItem() { }

        public ToDoItem(string nameTask, string descriprion)
        {
            this.ID = "";
            this.SystemName = ParametersKeeper.SystemName;
            this.TaskName = nameTask;
            this.Description = descriprion;
            this.CreationTime = DateTime.Now;
            this.Status = 0;
            this.Worker = "";
        }
    }
}
