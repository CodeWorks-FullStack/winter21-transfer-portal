using System;
using System.Collections.Generic;
using transferPortal.Models;
using transferPortal.Repositories;

namespace transferPortal.Services
{
  public class TeamsService
  {
    private readonly TeamsRepository _tr;
    public TeamsService(TeamsRepository tr)
    {
      _tr = tr;
    }

    internal List<Team> GetAllTeams()
    {
      return _tr.GetAllTeams();
    }

    internal Team GetTeamById(int teamId)
    {
      Team foundTeam = _tr.GetTeamById(teamId);
      if (foundTeam == null)
      {
        throw new Exception("Unable to find that team");
      }
      return foundTeam;
    }

    internal void Delete(int teamId, string userId)
    {
      Team teamToDelete = GetTeamById(teamId);
      if (teamToDelete.CreatorId != userId)
      {
        throw new Exception("Unauthorized to delete");
      }
      _tr.Delete(teamId);
    }

    internal Team Create(Team newTeam)
    {
      return _tr.Create(newTeam);
    }
  }
}