using Model.DomainModel;

namespace WebApi.DTO
{
    public class MovieSearchCriteria
    {
        public string Title { get; set; }
        public GenreEnum? Genre { get; set; }
        public int? MinYearOfRelease { get; set; } = null;
        public int? MaxYearOfRelease { get; set; }
        public int? MinRunningTimeInMinutes { get; set; }
        public int? MaxRunningTimeInMinutes { get; set; }
    }
}
