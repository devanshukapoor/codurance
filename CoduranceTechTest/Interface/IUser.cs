using System.Collections.Generic;

namespace CoduranceTechTest.Interface
{
    public interface IUser
    {
        string UserName { get; set; }
        IList<IPost> Posts { get; }
        void Follows(IUser user);
        void PostMessage(IPost post);
        IEnumerable<string> Read();
        IEnumerable<string> Wall();
    }
}
