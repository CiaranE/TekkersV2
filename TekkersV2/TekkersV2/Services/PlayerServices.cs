﻿using Plugin.RestClient;
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
        public async Task<List<Player>> GetPlayerByName(string name)
        {
            PlayerClient<Player> client = new PlayerClient<Player>();

            var listofPlayers = await client.GetPlayerByName(name);

            return listofPlayers;
        }

        //Get players by an age
        public async Task<List<Player>> GetPlayersByAgeAsync(int age)
        {
            PlayerClient<Player> client = new PlayerClient<Player>();

            var listofPlayers = await client.GetPlayersByAge(age);

            return listofPlayers;
        }

        public async Task<List<Player>> GetPlayersOnTeamAsync(string id)
        {
            PlayerClient<Player> client = new PlayerClient<Player>();
            var playersOnTeamList = await client.GetPlayersOnTeamAsync(id);
            return playersOnTeamList;
        }
    }
}
