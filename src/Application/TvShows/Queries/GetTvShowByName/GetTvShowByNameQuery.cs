using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TvShows.Queries.GetTvShowByName
{
    public class GetTvShowByNameQuery : IRequest<List<GetTvShowByNameDto>>
    {
        public string SearchPartTvShowName { get; set; }
    }

    public class GetTvShowByNameQueryHandler : IRequestHandler<GetTvShowByNameQuery, List<GetTvShowByNameDto>>
    {
        private readonly ITvMazeApiDbContext _context;
        private readonly IMapper _mapper;

        public GetTvShowByNameQueryHandler(ITvMazeApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<GetTvShowByNameDto>> Handle(GetTvShowByNameQuery request, CancellationToken cancellationToken)
        {
            return await _context.TvMazeShows
                .AsNoTracking()
                .Where(x => x.Name.ToLower().Contains(request.SearchPartTvShowName.ToLower()))
                .ProjectTo<GetTvShowByNameDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
