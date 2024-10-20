using Microsoft.AspNetCore.Mvc;
using ReposatoryPatternWithUOW.Core.Interfaces;
using ReposatoryPatternWithUOW.EF.Reposatories;

namespace XO.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController(IUnitOfWork unitOfWork) : ControllerBase
    {


        [HttpGet]

        public async Task<IActionResult> Details()
        {
            string id = TokenHandler.ExtractJwt(Request)?.Payload["id"].ToString()!;
            if (id is null || !int.TryParse(id, out int resultId))
                return BadRequest();
            var details = await unitOfWork.UserReposatory.GetByIdAsync(resultId);



            return Ok(new {details.UserName,details.Points});
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Details(int id)
        {
           
            var details = await unitOfWork.UserReposatory.GetByIdAsync(id);



            return Ok(new { details.UserName, details.Points });
        }





    }
}
