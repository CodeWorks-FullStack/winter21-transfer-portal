using System;
using System.Collections.Generic;
using transferPortal.Models;
using transferPortal.Repositories;

namespace transferPortal.Services
{
  public class PlayersService
  {
    private readonly PlayersRepository _pr;
    public PlayersService(PlayersRepository pr)
    {
      _pr = pr;
    }

    internal void RemovePlayerFromTeam(int playerId, string userId)
    {
      Player originalPlayer = GetPlayerById(playerId);
      if (originalPlayer.Team.CreatorId != userId)
      {
        throw new Exception("Not your player BRUH");
      }
      originalPlayer.TeamId = null;
      _pr.RemovePlayerFromTeam(originalPlayer);
    }

    internal List<Player> GetAllPlayers()
    {
      return _pr.GetAllPlayers();
    }

    internal Player GetPlayerById(int playerId)
    {
      Player foundPlayer = _pr.GetPlayerById(playerId);
      if (foundPlayer == null)
      {
        throw new Exception("Unable to find that player");
      }
      return foundPlayer;
    }

    internal Player Update(Player player, string userId)
    {
      Player foundPlayer = GetPlayerById(player.Id);
      if (foundPlayer.TeamId != null)
      {
        throw new Exception("Player is already on a team");
      }
      foundPlayer.TeamId = player.TeamId;
      return _pr.Update(foundPlayer);
    }

    internal List<Player> GetPlayersByTeam(int teamId)
    {
      return _pr.GetPlayersByTeam(teamId);
    }
  }
}