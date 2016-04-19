using CoduranceTechTest.Interface;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CoduranceTechTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IKernel kernel = new StandardKernel())
            {
                kernel.Bind<ICommand>().To<Command>();
                kernel.Bind<ICommandParser>().To<CommandParser>();
                kernel.Bind<IPost>().To<Post>();
                kernel.Bind<IUser>().To<User>();
                kernel.Bind<IRepository>().To<Repository>();
                kernel.Bind<IList<IUser>>().To<List<IUser>>();
                kernel.Bind<IList<IPost>>().To<List<IPost>>();

                var repo = kernel.Get<IRepository>();

                do
                {
                    var cmdParser = kernel.Get<ICommandParser>();

                    Console.Write("> ");

                    cmdParser.Parse(Console.ReadLine());

                    var user = repo.Read(cmdParser.Command.UserName);

                    switch (cmdParser.Command.Action)
                    {
                        case CommandAction.Posting:
                            if (user == null)
                            {
                                user = kernel.Get<IUser>();
                                user.UserName = cmdParser.Command.UserName;
                                var post = kernel.Get<IPost>();
                                post.Message = cmdParser.Command.PostMessage;
                                user.PostMessage(post);
                                repo.Create(user);
                            }
                            else
                            {
                                var post = kernel.Get<IPost>();
                                post.Message = cmdParser.Command.PostMessage;
                                user.PostMessage(post);
                            }
                            break;

                        case CommandAction.Reading:
                            if (user != null)
                            {
                                foreach (string postMsg in user.Read())
                                {
                                    Console.WriteLine(postMsg);
                                }
                            }
                            break;

                        case CommandAction.Wall:
                            if (user != null)
                            {
                                foreach (string postMsg in user.Wall())
                                {
                                    Console.WriteLine(postMsg);
                                }
                            }
                            break;

                        case CommandAction.Following:
                            if (user != null)
                            {
                                IUser followUser = repo.Read(cmdParser.Command.FollowingUserName);
                                if (followUser != null)
                                {
                                    user.Follows(followUser);
                                }
                            }
                            break;
                    }
                }
                while (true);
            }
        }
    }
}
