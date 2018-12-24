using Model.Data;
using Model.DomainModel;

namespace DAL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(CinemaContext context)
            : base(context)
        { }

        

        public CinemaContext CinemaContext
        {
            get { return context as CinemaContext; }
        }
        
    }
}
