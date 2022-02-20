using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TvShows.Queries.GetTvShowById
{
    public class GetTvShowByIdQuery : IRequest<GetTvShowByIdDto>
    {
        public int Id { get; set; }
    }

    public class GetTvShowQueryHandler : IRequestHandler<GetTvShowByIdQuery, GetTvShowByIdDto>
    {
        private readonly ITvMazeApiDbContext _context;

        public GetTvShowQueryHandler(ITvMazeApiDbContext context)
        {
            _context = context;
        }
        public async Task<GetTvShowByIdDto> Handle(GetTvShowByIdQuery request, CancellationToken cancellationToken)
        {
            var tvShowEntity = await _context.TvMazeShows.AsNoTracking().Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (tvShowEntity == null)
                throw new ArgumentNullException(nameof(tvShowEntity), $"TvShowEntity not found for ID: {request.Id}");

            var tvShowDto = new GetTvShowByIdDto
            {
                Id = tvShowEntity.Id,
                Genres = tvShowEntity.Genres.ToList(),
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