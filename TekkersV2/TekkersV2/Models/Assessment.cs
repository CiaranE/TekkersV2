using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekkersV2.Models
{
    public class Assessment
    {
        public string AssessmentName { get; set; }

        public DateTime AssessmentDate { get; set; }

        public virtual ICollection<Test> Tests { get; set; }

        public Player Player { get; set; }
    }
}
