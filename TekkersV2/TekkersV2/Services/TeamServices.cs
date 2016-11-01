using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;
using TekkersV2.RestClient;

namespace TekkersV2.Services
{
    class TeamServices
    {
        public async Task<List<Team>> GetTeamsAsync()
        {
            TeamClient<Team> client = new TeamClient<Team>();

            var listofTeams = await client.GetAsync();

            return listofTeams;
        }

        public async Task PostTeamAsync(Team t)
        {
            TeamClient<Team> client = new TeamClient<Team>();
            var sendTeam = await client.PostAsync(t);
        }

        public async Task PutTeamAsync(string Id, Team t)
        {
            TeamClient<Team> client = new TeamClient<Team>();
            var updateTeam = await client.PutAsync(t.Id, t);
        }

        public async Task DeleteTeamAsync(string Id)
        {
            TeamClient<Team> client = new TeamClient<Team>();
            var deleteTeam = await client.DeleteAsync(Id);

        }

        //Find a Team by their surname
        public async Task<List<Team>> GetTeamByNameAsync(string teamname)
        {
            TeamClient<Team> client = new TeamClient<Team>();

            var listofTeams = await client.GetTeamByNameAsync(teamname);

            return listofTeams;
        }

        //Get Teams by an age
        public async Task<List<Team>> GetTeamsByAgeAsync(int age)
        {
            TeamClient<Team> client = new TeamClient<Team>();

            var listofTeams = await client.GetTeamsByAgeAsync(age);

            return listofTeams;
        }
    }
}
