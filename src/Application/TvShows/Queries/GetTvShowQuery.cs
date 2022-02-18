using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class GetTvShowQuery : IRequest<TvShowDto>
    {
        public int Id { get; set; }
    }

    public class GetTvShowQueryHandler : IRequestHandler<GetTvShowQuery, TvShowDto>
    {
        private readonly ITvMazeApiDbContext _context;

        public GetTvShowQueryHandler(ITvMazeApiDbContext context)
        {
            _context = context;
        }
        public async Task<TvShowDto> Handle(GetTvShowQuery request, CancellationToken cancellationToken)
        {
            var tvShowEntity = await _context.TvMazeShows.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (tvShowEntity == null)
                throw new ArgumentNullException(nameof(tvShowEntity), $"TvShowEntity not found for ID: {request.Id}");

            var tvShowDto = new TvShowDto
            {
                Id = tvShowEntity.Id,
                Genres = tvShowEntity.Genres,
                Language = tvShowEntity.Language,
                Name = tvShowEntity.Name,
                Premiered = tvShowEntity.Premiered,
                Rating = tvShowEntity.Rating,
                Summary = tvShowEntity.Summary,
                TvMazeId = tvShowEntity.TvMazeId
            };
            
            return tvShowDto;
        }
    }
}