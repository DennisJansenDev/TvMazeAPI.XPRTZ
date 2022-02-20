using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TvShows.Queries.GetTvShows
{
    public class GetTvShowsQuery : IRequest<List<GetTvShowsDto>>
    {
    }

    public class GetTvShowsQueryHandler : IRequestHandler<GetTvShowsQuery, List<GetTvShowsDto>>
    {
        private readonly ITvMazeApiDbContext _context;
        private readonly IMapper _mapper;

        public GetTvShowsQueryHandler(ITvMazeApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<GetTvShowsDto>> Handle(GetTvShowsQuery request, CancellationToken cancellationToken)
        {
            return await _context.TvMazeShows
                .AsNoTracking()
                .OrderBy(x => x.Premiered)
                .ProjectTo<GetTvShowsDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
