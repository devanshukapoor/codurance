
namespace CoduranceTechTest.Interface
{
    public interface IRepository
    {
        void Create(IUser user);
        IUser Read(string userName);
    }
}
