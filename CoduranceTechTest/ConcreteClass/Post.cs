using CoduranceTechTest.Interface;
using System;

namespace CoduranceTechTest
{
    public class Post : IPost
    {
        private string msg;
        private DateTime postDateTime;
        
        public string Message {
            get { return msg; }
            set { msg = value; postDateTime = DateTime.Now; }
        }

        public DateTime PostDateTime {
            get { return postDateTime; } 
        }

        public Post()
        { }
    }
}
