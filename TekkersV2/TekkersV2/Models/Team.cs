using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekkersV2.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string TeamName { get; set; }

        public int TeamAgeGroup { get; set; }

        public virtual ICollection<Player> TeamPlayers { get; set; }
    }
}
