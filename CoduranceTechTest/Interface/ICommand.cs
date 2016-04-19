
namespace CoduranceTechTest.Interface
{
    public interface ICommand
    {
        string UserName { get; set; }
        string PostMessage { get; set; }
        string FollowingUserName { get; set; }
        CommandAction Action { get; set; }
    }
}
