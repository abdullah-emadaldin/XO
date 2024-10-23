using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReposatoryPatternWithUOW.Core.Interfaces;
using ReposatoryPatternWithUOW.EF.Reposatories;

namespace XO.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController(IUnitOfWork unitOfWork) : ControllerBase
    {



        [HttpPost("AddPoints")]

        public async Task<IActionResult> AddPoints()
        {

            string id = TokenHandler.ExtractJwt(Request)?.Payload["id"].ToString()!;
            if (id is null || !int.TryParse(id, out int resultId))
                return BadRequest();
            
            if(await unitOfWork.GameRepository.AddPoints(resultId))
            {
                await unitOfWork.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            


        }

        [HttpPost("AddPoints/{points}")]
        public async Task<IActionResult> AddPoints(int points)
        {

            string id = TokenHandler.ExtractJwt(Request)?.Payload["id"].ToString()!;
            if (id is null || !int.TryParse(id, out int resultId))
                return BadRequest();
            if (await unitOfWork.GameRepository.AddPoints(resultId,points))
            {
                await unitOfWork.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }



        }



    }
}
