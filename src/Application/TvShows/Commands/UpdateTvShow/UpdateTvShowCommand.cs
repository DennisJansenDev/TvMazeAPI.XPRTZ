using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.TvShows.Commands.UpdateTvShow
{
    public class UpdateTvShowCommand : IRequest
    {
        public int Id { get; set; }
        public int? TvMazeId { get; set; }
        public string? Name { get; set; }
        public string? Language { get; set; }
        public string? Summary { get; set; }
    }

    public class UpdateTvShowCommandHandler : IRequestHandler<UpdateTvShowCommand>
    {
        private readonly ITvMazeApiDbContext _context;

        public UpdateTvShowCommandHandler(ITvMazeApiDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateTvShowCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TvMazeShows.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(TvShow), request.Id);

            entity.UpdateTvShow(request.Name, request.Language, request.Summary, request.TvMazeId);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
