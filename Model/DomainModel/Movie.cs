using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Model.DomainModel
{
    public class Movie
    {
        public int MovieId { get; private set; }
        public string Title { get; private set; }
        public virtual GenreEnum Genre { get; private set; }
        public int YearOfRelease { get; private set; }
        public int RunningTimeInMinutes { get; private set; }

        public List<Rating> Ratings { get; } = new List<Rating>();

        public Movie(int movieId, string title, GenreEnum genre, int yearOfRelease, int runningTimeInMinutes)
            : this(title, genre, yearOfRelease, runningTimeInMinutes)
        {
            MovieId = movieId;
        }

        public Movie(string title, GenreEnum genre, int yearOfRelease, int runningTimeInMinutes)
        {
            Title = title;
            Genre = genre;
            YearOfRelease = yearOfRelease;
            RunningTimeInMinutes = runningTimeInMinutes;
        }

        public void Rate(User user, int rate)
        {
            if (rate < 1 || rate > 5) throw new ArgumentOutOfRangeException("Rate must be between 1 and 5");

            Rating userRating = Ratings.SingleOrDefault(r => r.UserId == user.UserId);
            if (userRating != null)
            {
                userRating.ReRate(rate);
            }
            else
            {
                Ratings.Add(new Rating(MovieId, user.UserId, rate));
            }
        }

        public double AverageRating => Ratings.Count>0 ? Ratings.Average(r => r.Value) : 0;
        public double RatingCount => Ratings.Count;
        public int? RatingBy(int userId) => Ratings.SingleOrDefault(r => r.UserId == userId)?.Value;
    }
}