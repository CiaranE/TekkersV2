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

        public DateTime TestDate { get; set; }

        public int TestScore { get; set; }

        public Player Player { get; set; }

        public Assessment Assessment { get; set; }
    }
}
