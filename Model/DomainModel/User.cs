using System.Collections.Generic;

namespace Model.DomainModel
{
    public class User
    {
        public int UserId { get; private set; }
        public string Name { get; private set; }

        public List<Rating> Ratings { get; } = new List<Rating>();

        public User(string name)
        {
            Name = name;
        }

        public User(int userId, string name )
            : this(name)
        {
            UserId = userId;
        }
    }
}