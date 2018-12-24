using Model.DomainModel;

namespace Model.Services
{
    public interface IUserService
    {
        User GetOne(int id);
    }
}