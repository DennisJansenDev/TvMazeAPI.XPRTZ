using Application.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.TvShows.Commands.CreateTvShow
{
    public class CreateTvShowCommandValidator : AbstractValidator<CreateTvShowCommand>
    {
        private readonly ITvMazeApiDbContext _context;

        public CreateTvShowCommandValidator(ITvMazeApiDbContext context)
        {
            _context = context;

            RuleFor(tvShow => tvShow.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(tvShow => tvShow.Language)
                .NotEmpty()
                .WithMessage("Language is required.");

            RuleFor(tvShow => tvShow.Summary)
                .NotEmpty()
                .WithMessage("Summary is required.");

            RuleFor(tvShow => tvShow.AverageRating)
                .NotEmpty()
                .WithMessage("AverageRating is required.");

            RuleFor(tvShow => tvShow.Genres)
                .NotEmpty()
                .WithMessage("Atleast one genre(s) is/are required.");

            RuleFor(tvShow => tvShow.Genres)
                .Must(v => v.Count <= 3)
                .WithMessage("Can't have more than 3 genres for a TvShow.");

            RuleFor(tvShow => tvShow.TvMazeId)
                .MustAsync(BeUniqueTvMazeId)
                .WithMessage("TvMazeId is already present, please supply a unique one, or update existing one.");
        }

        public async Task<bool> BeUniqueTvMazeId(int? tvMazeId, CancellationToken cancellationToken)
        {
            return await _context.TvMazeShows.AllAsync(tvShow => tvShow.TvMazeId != tvMazeId, cancellationToken);
        }
    }
}
