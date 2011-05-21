//
// Copyright (C) 2011 Thomas Mitchell
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//

using System;
using System.Collections.Generic;
using Chat.Main.Model;

namespace Chat.Main.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IDictionary<long, IUser> _users;
        private readonly IDictionary<string, IUser> _usersByUsername;

        public UserService(IServiceLocator serviceLocator) 
            : base(serviceLocator)
        {
            _users = new Dictionary<long, IUser>();
            _usersByUsername = new Dictionary<string, IUser>();
        }
        
        protected IUserFactoryService UserFactoryService
        {
            get { return ServiceLocator.GetService<IUserFactoryService>(); }
        }

        public ISignUpInfo CreateSignUpInfo(string username, string password, string emailAddress)
        {
            return UserFactoryService.CreateSignUpInfo(username, password, emailAddress);
        }

        public IUser CreateUser(ISignUpInfo user)
        {
            var newUser = UserFactoryService.CreateUser(user);
            _users.Add(newUser.Id, newUser);
            _usersByUsername.Add(newUser.Username, newUser);
            return newUser;
        }

        public void RemoveUser(IUser user)
        {
            _users.Remove(user.Id);
            _usersByUsername.Remove(user.Username);
        }

        public IUser GetUser(long userId)
        {
            return _users[userId];
        }

        public bool TryGetUser(long id, out IUser user)
        {
            return _users.TryGetValue(id, out user);
        }

        public IUser GetUser(string username)
        {
            return _usersByUsername[username];
        }

        public bool TryGetUser(string username, out IUser user)
        {
            return _usersByUsername.TryGetValue(username, out user);
        }
    }
}
