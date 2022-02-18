﻿using Domain.Entities;

namespace Application
{
    public class TvShowDto
    {
        public int Id { get; set; }
        public int TvMazeId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public DateTime Premiered { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public string Summary { get; set; }
        public Rating Rating { get; set; }
    }
}