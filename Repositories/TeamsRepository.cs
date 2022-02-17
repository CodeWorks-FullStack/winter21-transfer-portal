using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using transferPortal.Models;

namespace transferPortal.Repositories
{
  public class TeamsRepository
  {
    private readonly IDbConnection _db;
    public TeamsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Team> GetAllTeams()
    {
      string sql = @"
        SELECT
        t.*,
        a.*
        FROM teams t 
        JOIN accounts a on a.id = t.creatorId;";
      // NOTE we are passing in the two types for Dapper, and telling it what type to return to us
      return _db.Query<Team, Profile, Team>(sql, (t, p) =>
      {
        t.Owner = p;
        return t;
      }).ToList();
    }

    internal Team GetTeamById(int teamId)
    {
      string sql = @"
      SELECT
      t.*,
      a.*
      FROM teams t
      JOIN accounts a on a.id = t.creatorId
      WHERE t.Id = @teamId;";
      return _db.Query<Team, Profile, Team>(sql, (t, p) =>
      {
        t.Owner = p;
        return t;
      }, new { teamId }).FirstOrDefault();
    }

    internal void Delete(int teamId)
    {
      string sql = "DELETE FROM teams WHERE id = @teamId LIMIT 1;";
      var deletedRows = _db.Execute(sql, new { teamId });
      if (deletedRows == 0)
      {
        throw new Exception("Unable to delete");
      }
    }

    internal Team Create(Team newTeam)
    {
      string sql = @"
      INSERT INTO teams(creatorId, name, conference, division, picture)
      VALUES(@CreatorId, @Name, @Conference, @Division, @Picture);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newTeam);
      newTeam.Id = id;
      return newTeam;
    }
  }
}