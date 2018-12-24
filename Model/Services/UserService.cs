using Model.Data;
using Model.DomainModel;

namespace Model.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User GetOne(int id)
        {
            return _repository.GetOne(id);
        }
    }
}
