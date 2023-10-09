using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using TryBets.Bets.Repository;
using TryBets.Bets.DTO;
using TryBets.Bets.Services;
using TryBets.Bets.Models;


namespace TryBets.Bets.Controllers;

[Route("[controller]")]
public class BetController : Controller
{
    private readonly IBetRepository _repository;
    private readonly IOddService _oddService;
    public BetController(IBetRepository repository, IOddService oddService)
    {
        _repository = repository;
        _oddService = oddService;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "Client")]
    public IActionResult Post([FromBody] BetDTORequest request)
    {
        try
        {
            var token = HttpContext.User.Identity as ClaimsIdentity;
            var email = token?.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email)?.Value;
            var response = _repository.Post(request, email!);

            return Created("", response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{BetId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "Client")]
    public IActionResult Get(int BetId)
    {
        try
        {
            var token = HttpContext.User.Identity as ClaimsIdentity;
            var email = token?.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email)?.Value;
            var response = _repository.Get(BetId, email!);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}