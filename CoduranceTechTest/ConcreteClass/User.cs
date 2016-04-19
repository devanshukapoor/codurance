using CoduranceTechTest.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoduranceTechTest
{
    public class User : IUser
    {
        
        private IList<IUser> _followingUsers;
        private IList<IPost> _postsList;

        public string UserName { get; set; }
        public IList<IPost> Posts
        {
            get { return _postsList; }
        }

        public User(IList<IPost> postsList)
        {
            // Remove the posts from the list injected by Ninject
            while (postsList.Count > 0)
            {
                postsList.RemoveAt(0);
            }
            _postsList = postsList;
        }

        public void Follows(IUser user)
        {
            if (_followingUsers == null)
            {
                _followingUsers = new List<IUser>();
            }
            var foundUser = _followingUsers
                                .Select(u => u)
                                .Where((u, index) => u.UserName.ToLowerInvariant().Equals(user.UserName.ToLowerInvariant()))
                                .FirstOrDefault();

            if (foundUser == null)
            {
                _followingUsers.Add(user);
            }
        }

        public void PostMessage(IPost post)
        {
            Posts.Add(post);
        }

        public IEnumerable<string> Read()
        {
            return Posts
                .OrderByDescending(p => p.PostDateTime)
                .Select(p =>
                {
                    TimeSpan timeSpan = DateTime.Now - p.PostDateTime;
                    return String.Format("{0} ({1} {2} ago)",
                        p.Message,
                        timeSpan.Minutes == 0 ? timeSpan.Seconds : timeSpan.Minutes,
                        timeSpan.Minutes == 0 ? "seconds" : "minutes");
                });
        }

        public IEnumerable<string> Wall()
        {
            IList<Tuple<string, IPost>> wallPosts
                = Posts.Select(p => new Tuple<string, IPost>(this.UserName, p)).ToList();

            foreach (IUser followingUser in _followingUsers)
            {
                foreach (IPost followingUserPost in followingUser.Posts)
                {
                    wallPosts.Add(new Tuple<string, IPost>(followingUser.UserName, followingUserPost));
                }
            }

            return wallPosts
                .OrderByDescending(t => t.Item2.PostDateTime)
                .Select(t =>
                {
                    TimeSpan timeSpan = DateTime.Now - t.Item2.PostDateTime;
                    return String.Format("{0} - {1} ({2} {3} ago)",
                        t.Item1,
                        t.Item2.Message,
                        timeSpan.Minutes == 0 ? timeSpan.Seconds :
                        timeSpan.Minutes,
                        timeSpan.Minutes == 0 ? "seconds" : "minutes");
                });
        }
    }
}
