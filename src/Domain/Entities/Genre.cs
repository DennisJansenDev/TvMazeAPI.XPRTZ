namespace Domain.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public GenreType GenreType { get; set; }

        public override string ToString() => GenreType.ToString();
    }
}