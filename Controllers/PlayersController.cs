using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using transferPortal.Models;
using transferPortal.Services;

namespace transferPortal.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class PlayersController : ControllerBase
  {
    private readonly PlayersService _ps;
    public PlayersController(PlayersService ps)
    {
      _ps = ps;
    }

    [HttpGet]
    public ActionResult<List<Player>> GetAllPlayers()
    {
      try
      {
        return Ok(_ps.GetAllPlayers());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{playerId}")]
    public ActionResult<Player> GetPlayerById(int playerId)
    {
      try
      {
        return Ok(_ps.GetPlayerById(playerId));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{playerId}")]
    public async Task<ActionResult<Player>> Update([FromBody] Player player, int playerId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        player.Id = playerId;
        return Ok(_ps.Update(player, userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}