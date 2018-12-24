using Microsoft.EntityFrameworkCore;
using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class DbSeed
    {


        public static User[] Users = new User[] {
            new User(1, "User1"),
            new User(2, "User2"),
            new User(3, "User3"),
            new User(4, "User4"),
            new User(5, "User5"),
            new User(6, "User6"),
            new User(7, "User7"),
            new User(8, "User8"),
            new User(9, "User9"),
            new User(10, "User10")
        };

        public static Movie[] Movies = new Movie[] {
            new Movie(1, "The Shawshank Redemption", GenreEnum.Drama, 1994, 142), //0
            new Movie(2, "The Godfather", GenreEnum.Crime, 1972, 175), //1
            new Movie(3, "The Godfather: Part II", GenreEnum.Crime, 1974, 202), //2
            new Movie(4, "The Dark Knight", GenreEnum.Action, 2008, 152), //3
            new Movie(5, "12 Angry Men", GenreEnum.Drama, 1957, 96), //4
            new Movie(6, "Schindler's List", GenreEnum.Drama, 1993, 195), //5
            new Movie(7, "The Lord of the Rings: The Return of the King", GenreEnum.Adventure, 2003, 201), //6
            new Movie(8, "Pulp Fiction", GenreEnum.Drama, 1994, 154), //7
            new Movie(9, "The Good, the Bad and the Ugly", GenreEnum.Western, 1966, 148), //8
            new Movie(10, "Fight Club", GenreEnum.Action, 1999, 139), // 9
        };

        public static Rating[] Ratings = new Rating[] {
            new Rating(Movies[0].MovieId, Users[0].UserId, 5),

            new Rating(Movies[1].MovieId, Users[1].UserId, 5),
            new Rating(Movies[1].MovieId, Users[2].UserId, 4),
            new Rating(Movies[1].MovieId, Users[3].UserId, 3),
            new Rating(Movies[1].MovieId, Users[4].UserId, 3),
            new Rating(Movies[1].MovieId, Users[5].UserId, 4),
            new Rating(Movies[1].MovieId, Users[6].UserId, 5),

            new Rating(Movies[2].MovieId, Users[1].UserId, 5),

            new Rating(Movies[3].MovieId, Users[1].UserId, 5),
            new Rating(Movies[3].MovieId, Users[2].UserId, 4),
            new Rating(Movies[3].MovieId, Users[3].UserId, 4),
            new Rating(Movies[3].MovieId, Users[4].UserId, 4),
            new Rating(Movies[3].MovieId, Users[5].UserId, 4),
            new Rating(Movies[3].MovieId, Users[6].UserId, 3),
            new Rating(Movies[3].MovieId, Users[7].UserId, 5),
            new Rating(Movies[3].MovieId, Users[8].UserId, 5),

            new Rating(Movies[4].MovieId, Users[1].UserId, 5),
            new Rating(Movies[4].MovieId, Users[2].UserId, 1),

            new Rating(Movies[5].MovieId, Users[1].UserId, 5),
            new Rating(Movies[5].MovieId, Users[2].UserId, 5),
            new Rating(Movies[5].MovieId, Users[3].UserId, 5),
            new Rating(Movies[5].MovieId, Users[4].UserId, 5),
            new Rating(Movies[5].MovieId, Users[5].UserId, 5),
            new Rating(Movies[5].MovieId, Users[6].UserId, 5),
            new Rating(Movies[5].MovieId, Users[7].UserId, 5),
            new Rating(Movies[5].MovieId, Users[8].UserId, 5),

            new Rating(Movies[6].MovieId, Users[1].UserId, 5),
            new Rating(Movies[6].MovieId, Users[2].UserId, 5),
            new Rating(Movies[6].MovieId, Users[3].UserId, 5),
            new Rating(Movies[6].MovieId, Users[4].UserId, 4),

            new Rating(Movies[7].MovieId, Users[3].UserId, 5),
            new Rating(Movies[7].MovieId, Users[4].UserId, 4),

            new Rating(Movies[8].MovieId, Users[3].UserId, 2),
            new Rating(Movies[8].MovieId, Users[4].UserId, 4)
        };

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                Users
            );

            modelBuilder.Entity<Movie>().HasData(
                Movies
            );

            modelBuilder.Entity<Rating>().HasData(
                Ratings
            );

        }
    }
}
