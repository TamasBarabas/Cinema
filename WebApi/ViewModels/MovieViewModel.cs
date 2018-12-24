
using Model.DomainModel;
using System;
using System.Collections.Generic;

namespace WebApi.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public int YearOfRelease { get; private set; }
        public int RunningTimeInMinutes { get; private set; }
        public double AverageRating { get; private set; }
        public double RatingCount { get; private set; }

        public MovieViewModel(int id, string title, Genre genre, int yearOfRelease, int runningTimeInMinutes, double averageRating, double ratingCount)
        {
            Id = id;
            Title = title;
            Genre = genre.Name;
            YearOfRelease = yearOfRelease;
            RunningTimeInMinutes = runningTimeInMinutes;
            AverageRating = averageRating;
            RatingCount = ratingCount;
        }
    }
}
