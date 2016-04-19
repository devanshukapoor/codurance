using CoduranceTechTest.Interface;

namespace CoduranceTechTest
{
    public class Command : ICommand
    {
        public string UserName { get; set; }
        public string PostMessage { get; set; }
        public string FollowingUserName { get; set; }
        public CommandAction Action { get; set; }
    }
}
