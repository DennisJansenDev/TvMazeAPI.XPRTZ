using Application;
using Application.TvShows.Commands;
using Application.TvShows.Commands.DeleteTvShow;
using Application.TvShows.Commands.UpdateTvShow;
using Application.TvShows.Queries.GetTvShows;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class TvShowsController : ApiControllerBase
    {

        [HttpGet()]
        public async Task<ActionResult<List<GetTvShowsDto>>> Get()
        {
            return await Mediator.Send(new GetTvShowsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTvShowByIdDto>> GetById(int id)
        {
            return await Mediator.Send(new GetTvShowByIdQuery { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTvShowCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTvShowCommand command)
        {
            if(id == command.Id)
            {
                await Mediator.Send(command);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTvShowCommand
            {
                Id = id
            });

            return Ok();
        }
    }
}
