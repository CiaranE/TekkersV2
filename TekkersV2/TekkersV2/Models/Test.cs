using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekkersV2.Models
{
    public class Test
    {
        public string Id { get; set; }

        public string TestName { get; set; }

        public string TestDescription { get; set; }

        public DateTime TestDate { get; set; }

        public int TestScore { get; set; }

        public virtual Assessment AssessmentTest { get; set; }

        public virtual Player PlayerTest { get; set; }


        public Test() { }

        public Test(Guid id, string testname, string testdesc, DateTime testdate, int testscore)
        {
            this.Id = id.ToString();
            this.TestName = testname;
            this.TestDescription = testdesc;
            this.TestDate = testdate;
            this.TestScore = testscore;
        }
    }
}
