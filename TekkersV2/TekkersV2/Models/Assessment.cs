using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekkersV2.Models
{
    public class Assessment
    {
        public string Id { get; set; }
        public string AssessmentName { get; set; }

        public DateTime AssessmentDate { get; set; }

        public int AssessmentScore { get; set; }

        //public int AssessmentScore { get; set; }

        public virtual ICollection<Test> Tests { get; set; }

        public virtual Player Player { get; set; }

        public Assessment() { }

        public Assessment(Guid id, string assessmentname, DateTime assessmentdate, ICollection<Test> tests, string playerid)
        {
            this.Id = playerid;
            this.AssessmentName = assessmentname;
            this.AssessmentDate = assessmentdate;
            this.Tests = tests;
            this.Player.Id = playerid;
        }
    }
}
