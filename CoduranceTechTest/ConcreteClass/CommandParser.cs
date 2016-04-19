using CoduranceTechTest.Interface;
using System;
using System.Collections.Generic;

namespace CoduranceTechTest
{
    public class CommandParser : ICommandParser
    {
        private readonly ICommand cmd;

        public ICommand Command
        {
            get { return cmd; }
        }

        public CommandParser(ICommand command)
        {
            cmd = command;
        }

        public void Parse(string userInput)
        {
            IList<string> userInputArray = userInput.Split(' ');
            IList<string> userInputArrayCopy = userInput.Split(' ');

            cmd.UserName = userInputArray.Count >= 1 ? userInputArray[0] : null;
            cmd.PostMessage = null;
            cmd.FollowingUserName = null;
            if (userInputArray.Count == 1)
                cmd.Action = CommandAction.Reading;
            else if (userInputArray.Count == 2 && userInputArray[1].ToLowerInvariant().Equals("wall"))
                cmd.Action = CommandAction.Wall;
            else if (userInputArray.Count > 2 && userInputArray[1].ToLowerInvariant().Equals("follows"))
            {
                cmd.Action = CommandAction.Following;
                cmd.FollowingUserName = userInput.Replace(String.Format("{0} follows ", cmd.UserName), String.Empty);
            }
            else if (userInputArray.Count > 2 && userInputArray[1].ToLowerInvariant().Equals("->"))
            {
                cmd.Action = CommandAction.Posting;
                cmd.PostMessage = userInput.Replace(String.Format("{0} -> ", cmd.UserName), String.Empty);
            }
            else
            {
                cmd.UserName = null;
                cmd.Action = CommandAction.None;
            }
        }
    }
}
