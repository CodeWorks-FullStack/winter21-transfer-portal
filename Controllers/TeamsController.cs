using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using transferPortal.Models;
using transferPortal.Services;

namespace transferPortal.Controllers
{
  // NOTE do this if you want to lock down the entire controller
  // [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class TeamsController : ControllerBase
  {
    private readonly TeamsService _ts;
    private readonly PlayersService _ps;
    public TeamsController(TeamsService ts, PlayersService ps)
    {
      _ts = ts;
      _ps = ps;
    }

    [HttpGet]
    public ActionResult<List<Team>> GetAllTeams()
    {
      try
      {
        return Ok(_ts.GetAllTeams());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{teamId}")]
    public ActionResult<Team> GetTeamById(int teamId)
    {
      try
      {
        return Ok(_ts.GetTeamById(teamId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{teamId}/players")]
    public ActionResult<List<Player>> GetPlayersByTeam(int teamId)
    {
      try
      {
        return Ok(_ps.GetPlayersByTeam(teamId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // NOTE if you want to lock down a specific route, put the authorize tag directly above it
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Team>> Create([FromBody] Team newTeam)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // NOTE think of this as req.body.creatorId = req.userInfo.id
        newTeam.CreatorId = userInfo.Id;
        // NOTE setting the owner object on the created team BEFORE it gets sent back to the client
        Team createdTeam = _ts.Create(newTeam);
        createdTeam.Owner = userInfo;
        return Ok(createdTeam);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpDelete("{teamId}")]

    // NOTE must use Task when wanting to async await something
    public async Task<ActionResult<string>> Delete(int teamId)
    {
      try
      {
        // Auth here!
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ts.Delete(teamId, userInfo.Id);
        return Ok("Team was delorted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]

    [HttpDelete("{teamId}/players/{playerId}")]
    public async Task<ActionResult<Player>> RemovePlayerFromTeam(int playerId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ps.RemovePlayerFromTeam(playerId, userInfo.Id);
        return Ok("Player removed from team");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}