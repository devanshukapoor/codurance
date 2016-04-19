using System;
using NUnit.Framework;
using CoduranceTechTest.Interface;
using Ninject;
using System.Collections.Generic;

namespace CoduranceTechTest.UnitTest
{
    [TestFixture]
    public class CommandParserTest
    {
        private IKernel kernel;

        [SetUp]
        public void Init()
        {
            kernel = new StandardKernel();
            kernel.Bind<ICommand>().To<Command>();
            kernel.Bind<ICommandParser>().To<CommandParser>();
        }

        [TearDown]
        public void WindDown()
        {
            kernel.Dispose();
        }

        [TestCase("Alice -> I love the weather today")]
        public void CommandParserPosting(string userInput)
        {
            var resultCmd = new Command()
            {
                UserName = "Alice",
                Action = CommandAction.Posting,
                PostMessage = "I love the weather today",
                FollowingUserName = null
            };
            var cmdParser = kernel.Get<ICommandParser>();
            cmdParser.Parse(userInput);
            Assert.AreEqual(cmdParser.Command.Action, resultCmd.Action);
            Assert.AreEqual(cmdParser.Command.UserName, resultCmd.UserName);
            Assert.AreEqual(cmdParser.Command.PostMessage, resultCmd.PostMessage);
            Assert.AreEqual(cmdParser.Command.FollowingUserName, resultCmd.FollowingUserName);
        }

        [TestCase("Alice")]
        public void CommandParserReading(string userInput)
        {
            var resultCmd = new Command()
            {
                UserName = "Alice",
                Action = CommandAction.Reading,
                PostMessage = null,
                FollowingUserName = null
            };
            var cmdParser = kernel.Get<ICommandParser>();
            cmdParser.Parse(userInput);
            Assert.AreEqual(cmdParser.Command.Action, resultCmd.Action);
            Assert.AreEqual(cmdParser.Command.UserName, resultCmd.UserName);
            Assert.AreEqual(cmdParser.Command.PostMessage, resultCmd.PostMessage);
            Assert.AreEqual(cmdParser.Command.FollowingUserName, resultCmd.FollowingUserName);
        }

        [TestCase("Alice follows Bob")]
        public void CommandParserFollowing(string userInput)
        {
            var resultCmd = new Command()
            {
                UserName = "Alice",
                Action = CommandAction.Following,
                PostMessage = null,
                FollowingUserName = "Bob"
            };
            var cmdParser = kernel.Get<ICommandParser>();
            cmdParser.Parse(userInput);
            Assert.AreEqual(cmdParser.Command.Action, resultCmd.Action);
            Assert.AreEqual(cmdParser.Command.UserName, resultCmd.UserName);
            Assert.AreEqual(cmdParser.Command.PostMessage, resultCmd.PostMessage);
            Assert.AreEqual(cmdParser.Command.FollowingUserName, resultCmd.FollowingUserName);
        }

        [TestCase("Alice wall")]
        public void CommandParserWall(string userInput)
        {
            var resultCmd = new Command()
            {
                UserName = "Alice",
                Action = CommandAction.Wall,
                PostMessage = null,
                FollowingUserName = null
            };
            var cmdParser = kernel.Get<ICommandParser>();
            cmdParser.Parse(userInput);
            Assert.AreEqual(cmdParser.Command.Action, resultCmd.Action);
            Assert.AreEqual(cmdParser.Command.UserName, resultCmd.UserName);
            Assert.AreEqual(cmdParser.Command.PostMessage, resultCmd.PostMessage);
            Assert.AreEqual(cmdParser.Command.FollowingUserName, resultCmd.FollowingUserName);
        }

        [TestCase("Alice test none")]
        public void CommandParserNone(string userInput)
        {
            var resultCmd = new Command()
            {
                UserName = null,
                Action = CommandAction.None,
                PostMessage = null,
                FollowingUserName = null
            };
            var cmdParser = kernel.Get<ICommandParser>();
            cmdParser.Parse(userInput);
            Assert.AreEqual(cmdParser.Command.Action, resultCmd.Action);
            Assert.AreEqual(cmdParser.Command.UserName, resultCmd.UserName);
            Assert.AreEqual(cmdParser.Command.PostMessage, resultCmd.PostMessage);
            Assert.AreEqual(cmdParser.Command.FollowingUserName, resultCmd.FollowingUserName);
        }
    }
}
