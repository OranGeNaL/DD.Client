using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.ClientWPF
{
    internal class Alert
    {
        public string ID { get; set; }

        public string Header { get; set; } = "Header";

        public DateTime CreateTime { get; set; }

        public string Description { get; set; } = "Description";

        public int Status { get; set; } = 0;

        public string NameSystem { get; set; }

        public string Worker { get; set; }

        public string CommentToClose { get; set; }


        public Alert() { }
    }
}
