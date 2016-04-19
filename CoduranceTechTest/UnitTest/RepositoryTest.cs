using CoduranceTechTest.Interface;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoduranceTechTest.UnitTest
{
    public class RepositoryTest
    {
        private IKernel kernel;

        [SetUp]
        public void Init()
        {
            kernel = new StandardKernel();
            kernel.Bind<ICommand>().To<Command>();
            kernel.Bind<ICommandParser>().To<CommandParser>();
            kernel.Bind<IPost>().To<Post>();
            kernel.Bind<IUser>().To<User>();
            kernel.Bind<IRepository>().To<Repository>();
            kernel.Bind<IList<IUser>>().To<List<IUser>>();
            kernel.Bind<IList<IPost>>().To<List<IPost>>();
        }

        [TearDown]
        public void WindDown()
        {
            kernel.Dispose();
        }

        [Test]
        public void CreateAndReadTest()
        {
            var user = kernel.Get<IUser>();
            user.UserName = "Alice";
            var post = kernel.Get<IPost>();
            post.Message = "I love todays weather";
            user.PostMessage(post);

            var repo = kernel.Get<IRepository>();
            repo.Create(user);

            var foundUser = repo.Read("Alice");
            Assert.AreEqual(foundUser.UserName, user.UserName);
            Assert.AreEqual(foundUser.Posts.Count, user.Posts.Count);
            for (int i = 0; i < foundUser.Posts.Count;i++)
            {
                Assert.AreEqual(foundUser.Posts[i].Message, user.Posts[i].Message);
                Assert.AreEqual(foundUser.Posts[i].PostDateTime, user.Posts[i].PostDateTime);
            }
        }
    }
}
