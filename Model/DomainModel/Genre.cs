namespace Model.DomainModel
{
    public class Genre
    {
        public Genre(GenreEnum @enum)
        {
            Id = (int)@enum;
            Name = @enum.ToString();
        }

        protected Genre() { } //For EF

        public int Id { get; set; }

        public string Name { get; set; }

        public static implicit operator Genre(GenreEnum @enum) => new Genre(@enum);
        public static implicit operator GenreEnum(Genre genre) => (GenreEnum)genre.Id;
    }
}