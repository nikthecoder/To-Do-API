using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI.Models
{
    public class Note
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
    }
}
