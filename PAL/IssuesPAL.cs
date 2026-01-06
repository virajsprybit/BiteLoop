using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class IssuesPAL
    {
        public int ID { get; set; }
        public string CaseNumber { get; set; }
        public string OrderUniqueID { get; set; }
        public string IssueDescription { get; set; }
        public string Images { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; }
    }
}
