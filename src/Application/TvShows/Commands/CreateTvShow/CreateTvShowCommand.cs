using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using MediatR;

namespace Application.TvShows.Commands
{
    public class CreateTvShowCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public DateTime Premiered { get; set; }
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public string Summary { get; set; }
        public double AverageRating { get; set; }
        public int? TvMazeId { get; set; }
    }

    public class CreateTvShowCommandHandler : IRequestHandler<CreateTvShowCommand, int>
    {
        private readonly ITvMazeApiDbContext _context;

        public CreateTvShowCommandHandler(ITvMazeApiDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateTvShowCommand request, CancellationToken cancellationToken)
        {
            var tvShowEntity = new TvShow(
                request.Name, 
                request.Language, 
                request.Premiered, 
                request.Genres, 
                request.Summary, 
                Rating.FromDouble(request.AverageRating), 
                request.TvMazeId ?? null);

            _context.TvMazeShows.Add(tvShowEntity);

            await _context.SaveChangesAsync(cancellationToken);

            return tvShowEntity.Id;
        }
    }
}
