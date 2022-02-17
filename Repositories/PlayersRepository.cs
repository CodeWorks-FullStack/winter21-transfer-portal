using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using transferPortal.Models;

namespace transferPortal.Repositories
{
  public class PlayersRepository
  {
    private readonly IDbConnection _db;
    public PlayersRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Player> GetAllPlayers()
    {
      string sql = @"
      SELECT
      p.*,
      t.*
      FROM players p 
      LEFT JOIN teams t on t.id = p.teamId;";
      return _db.Query<Player, Team, Player>(sql, (p, t) =>
      {
        if (p.TeamId != null)
        {
          p.Team = t;
          p.TeamId = t.Id;
        }
        return p;
      }).ToList();
    }

    internal void RemovePlayerFromTeam(Player originalPlayer)
    {
      string sql = @"
      UPDATE players
      SET
      teamId = @teamId
      WHERE id = @id;";
      _db.Execute(sql, originalPlayer);
    }

    internal Player GetPlayerById(int playerId)
    {
      // NOTE you can use SELECT * instead of t.*, a.*, p.*
      string sql = @"
      SELECT *
      FROM players p 
      LEFT JOIN teams t on t.id = p.teamId
      LEFT JOIN accounts a on a.id = t.creatorId
      WHERE p.Id = @playerId;";
      // NOTE when you are passing in the data to dapper, it needs to match to order in the SQL query
      return _db.Query<Player, Team, Profile, Player>(sql, (p, t, a) =>
      {
        if (t != null)
        {
          p.Team = t;
          t.Owner = a;
        }
        return p;
      }, new { playerId }).FirstOrDefault();
    }

    internal List<Player> GetPlayersByTeam(int teamId)
    {
      string sql = @"SELECT * FROM players WHERE teamId = @teamId";
      return _db.Query<Player>(sql, new { teamId }).ToList();
    }

    internal Player Update(Player updatedPlayer)
    {
      string sql = @"
      UPDATE players
      SET
      teamId = @teamId
      WHERE id = @id;";
      var rowsAffected = _db.Execute(sql, updatedPlayer);
      if (rowsAffected == 0)
      {
        throw new Exception("Unable to edit");
      }
      return updatedPlayer;
    }
  }
}