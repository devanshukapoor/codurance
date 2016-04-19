using CoduranceTechTest.Interface;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoduranceTechTest.UnitTest
{
    class UserTest
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

        [TestCase()]
        public void UserNameTest()
        {
            var user = kernel.Get<IUser>();
            user.UserName = "Alice";
            
            Assert.AreEqual("Alice", user.UserName);
        }

        [TestCase()]
        public void UserPostTest()
        {
            var user = kernel.Get<IUser>();
            user.UserName = "Alice";
            var post = kernel.Get<IPost>();
            post.Message = "I love the weather today";
            user.PostMessage(post);
            
            Assert.AreEqual(user.Posts[0].Message, "I love the weather today");
        }

        [TestCase()]
        public void UserReadTest()
        {
            var user = kernel.Get<IUser>();
            user.UserName = "Alice";
            var post = kernel.Get<IPost>();
            post.Message = "I love the weather today";
            user.PostMessage(post);
            
            Thread.Sleep(1000);
            
            var post1 = kernel.Get<IPost>();
            post1.Message = "Tommorow's weather is good as well";
            user.PostMessage(post1);
            
            Assert.AreEqual(user.Read().ElementAt(0).Split('(')[0], "Tommorow's weather is good as well ");
            Assert.AreEqual(user.Read().ElementAt(1).Split('(')[0], "I love the weather today ");
        }

        [TestCase()]
        public void UserReadMinuteTest()
        {
            var user = kernel.Get<IUser>();
            user.UserName = "Alice";
            var post = kernel.Get<IPost>();
            post.Message = "I love the weather today";
            user.PostMessage(post);

            Thread.Sleep(60000);

            var post1 = kernel.Get<IPost>();
            post1.Message = "Tommorow's weather is good as well";
            user.PostMessage(post1);

            Assert.AreEqual(user.Read().ElementAt(0).Split('(')[0], "Tommorow's weather is good as well ");
            Assert.AreEqual(user.Read().ElementAt(1).Split('(')[0], "I love the weather today ");
        }

        [TestCase()]
        public void UserFollowsAndWallTest()
        {
            var user = kernel.Get<IUser>();
            user.UserName = "Alice";
            var post = kernel.Get<IPost>();
            post.Message = "I love the weather today";
            user.PostMessage(post);

            Thread.Sleep(1000);
            
            var user1 = kernel.Get<IUser>();
            user1.UserName = "Bob";
            var post1 = kernel.Get<IPost>();
            post1.Message = "Tommorow's weather is good as well";
            user1.PostMessage(post1);

            user.Follows(user1);

            Assert.AreEqual(user.Wall().ElementAt(0).Split('(')[0], "Bob - Tommorow's weather is good as well ");
            Assert.AreEqual(user.Wall().ElementAt(1).Split('(')[0], "Alice - I love the weather today ");
        }

        [TestCase()]
        public void UserFollowsAndWallMinuteTest()
        {
            var user = kernel.Get<IUser>();
            user.UserName = "Alice";
            var post = kernel.Get<IPost>();
            post.Message = "I love the weather today";
            user.PostMessage(post);

            Thread.Sleep(60000);

            var user1 = kernel.Get<IUser>();
            user1.UserName = "Bob";
            var post1 = kernel.Get<IPost>();
            post1.Message = "Tommorow's weather is good as well";
            user1.PostMessage(post1);

            user.Follows(user1);

            Assert.AreEqual(user.Wall().ElementAt(0).Split('(')[0], "Bob - Tommorow's weather is good as well ");
            Assert.AreEqual(user.Wall().ElementAt(1).Split('(')[0], "Alice - I love the weather today ");
        }
    }
}
