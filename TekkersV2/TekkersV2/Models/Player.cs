using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TekkersV2.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ParentFName { get; set; }

        public string ParentLName { get; set; }

        public string Email { get; set; }

        public string PhoneNum { get; set; }

        public int AgeGroup { get; set; }

        public virtual ICollection<Assessment> PlayerAssessments { get; set; }

        public virtual ICollection<Test> PlayerTests { get; set; }

        public virtual Team PlayersTeam { get; set; }
    }
}
