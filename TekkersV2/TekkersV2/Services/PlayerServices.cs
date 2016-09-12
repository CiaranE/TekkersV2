using Plugin.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekkersV2.Models;

namespace TekkersV2.Services
{
    public class PlayerServices
    {
       
        public async Task<List<Player>> GetPlayersAsync()
        {
            PlayerClient<Player> client = new PlayerClient<Player>();

            var listofPlayers = await client.GetAsync();
       
            return listofPlayers;
        }

        public async Task PostPlayerAsync(Player p)
        {
            PlayerClient<Player> client = new PlayerClient<Player>();
            var sendPlayer = await client.PostAsync(p);
        }

        public async Task PutPlayerAsync(string Id, Player p)
        {
            PlayerClient<Player> client = new PlayerClient<Player>();
            var updatePlayer = await client.PutAsync(p.Id, p);
        }

        public async Task DeletePlayerAsync(string Id)
        {
            PlayerClient<Player> client = new PlayerClient<Player>();
            var deletePlayer = await client.DeleteAsync(Id);

        }

        //Find a player by their surname
        public async Task<List<Player>> GetPlayerByNameAsync(string name)
        {
            PlayerClient<Player> client = new PlayerClient<Player>();

            var listofPlayers = await client.GetByNameAsync(name);

            return listofPlayers;
        }
    }
}
