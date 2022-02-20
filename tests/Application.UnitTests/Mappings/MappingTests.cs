using Application.Mappings;
using Application.TvShows.Queries.GetTvShows;
using AutoMapper;
using Domain.Entities;
using System;
using Xunit;

namespace Application.UnitTests.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldContainValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(TvShow), typeof(GetTvShowByIdDto))]
        [InlineData(typeof(TvShow), typeof(GetTvShowsDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
