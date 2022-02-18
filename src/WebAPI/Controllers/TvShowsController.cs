using Application;
using Application.TvShows.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class TvShowsController : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<TvShowDto>> GetById(int id)
        {
            return await Mediator.Send(new GetTvShowQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTvShowCommand command)
        {
            return await Mediator.Send(command);
        }

        
    }
}
