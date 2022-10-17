using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailConsole
{
    internal class EmailEntity
    {
        public string senderAddress { get; set; }
        public string recipeintAddress { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public DateTime sendTime { get; set; }
    }
}
