namespace Application.Interfaces
{
    public class TvMazeShow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public DateTime Premiered { get; set; }
        public string Genre { get; set; }
        public string Summary { get; set; }
    }
}