using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.TvShows.Commands.DeleteTvShow
{
    public class DeleteTvShowCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTvShowCommandHandler : IRequestHandler<DeleteTvShowCommand>
    {
        private readonly ITvMazeApiDbContext _context;

        public DeleteTvShowCommandHandler(ITvMazeApiDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteTvShowCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TvMazeShows.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
                throw new NotFoundException(nameof(TvShow), request.Id);

            _context.TvMazeShows.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
