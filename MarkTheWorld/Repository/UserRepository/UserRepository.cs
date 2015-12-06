using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public class UserRepository : GenericRepository.GenericRepository, IUserRepository
    {
        public UserRegistrationModel AddUser(User user)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = false;
                    check.message = "Unknown error";
                    check.Token = System.Guid.NewGuid();
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(user.UserName));
                    if (oneObject == null)
                    {                       
                        user.Token = check.Token;
                        session.Store(user);
                        session.SaveChanges();
                        check.success = true;
                        check.message = "User added";
                        return check;
                    }
                    check.success = false;
                    check.message = "Username already taken";
                    return check;
                }
                catch
                {
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = true;
                    check.Token = System.Guid.NewGuid();
                    user.Token = check.Token;
                    session.Store(user);
                    session.SaveChanges();
                    check.message = "User added";
                    return check;
                }
            }
        }

        public UserRegistrationModel GetOneUser(User user)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = false;
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(user.UserName));
                    if (oneObject == null)
                    {
                        check.success = false;
                        check.message = "Wrong username";
                        return check;
                    }

                    if (!oneObject.PasswordHash.Equals(user.PasswordHash))
                    {
                        check.success = false;
                        check.message = "Wrong password";
                        return check;
                    }

                    if (oneObject != null)
                    {
                        check.success = true;
                        check.message = "User found";
                        check.Token = oneObject.Token;
                        return check;
                    }
                    return check;
                }
                catch
                {
                    UserRegistrationModel check = new UserRegistrationModel();
                    check.success = false;
                    check.message = "Wrong username";
                    return check;
                }
            }
        }

        public User GetOneByName(string name)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(name));
                    return oneObject;
                }
                catch
                {
                    return null;
                }
            }
        }

        public User GetOneByToken(Guid token)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User oneObject = session.Query<User>().First(x => x.Token.Equals(token));
                    return oneObject;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Guid GetTokenByName(string name)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                try
                {
                    User oneObject = session.Query<User>().First(x => x.UserName.Equals(name));
                    return oneObject.Token;
                }
                catch
                {
                    return new Guid();
                }
            }
        }
    }
}