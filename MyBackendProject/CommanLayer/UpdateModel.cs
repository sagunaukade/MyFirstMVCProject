using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer
{
    public class UpdateModel
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
    }
}
