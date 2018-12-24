using Newtonsoft.Json;

namespace Model.DomainModel
{
    public class Rating
    {
        public Rating(int movieId, int userId, int value)
        {
            MovieId = movieId;
            UserId = userId;
            Value = value;
        }

        public int MovieId { get; private set; }
        public int UserId { get; private set; }

        public int Value { get; private set; }


        public Movie Movie { get; private set; }
        public User User { get; private set; }

        public void ReRate(int newValue)
        {
            Value = newValue;   
        }
    }
}