using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.TestGenerator
{
    public class GenerateObjects
    {
        private Repository.UserRepository.UserRepository repository = new Repository.UserRepository.UserRepository();
        //private Repository.GenericRepository.IGenericRepository repository = new Repository.GenericRepository.GenericRepository();
        private Repository.DotRepository.IDotRepository repositoryDot = new Repository.DotRepository.DotRepository();

        public UserRegistrationModel generateUser()
        {
            User user = new User();
            string[] pavardės = { "varšys", "driugys", "banilskis", "bobtuskis", "lakonavičius", "berdonskis", "pralešavičius", "vinciškis", "druiginksis", "lepavičius", "skis", "bakskys" };
            Random rnd = new Random();
            char[] chars = "ABCDEFGHIJKLMNOPRSTUVYZ".ToCharArray();
            char[] chars2 = "aeiuoy".ToCharArray();
            int pavardė = rnd.Next(12);
            int pavardėRaid = rnd.Next(22);
            int pavardėRaid2 = rnd.Next(6);
            int pavardėRaid3 = rnd.Next(6);
            user.UserName = chars[pavardėRaid] + "" + chars2[pavardėRaid2] + "" + chars2[pavardėRaid3] + "" + pavardės[pavardė];
            user.PasswordHash = (pavardė * pavardė).ToString() + " vvvd";
            return repository.AddUser(user);
        }

        public UserRegistrationModel generateDots(Guid token)
        {
            DotFromViewModel dot = new DotFromViewModel();
            Random rnd = new Random();
            dot.lat = rnd.NextDouble() * 180;
            dot.lng = rnd.NextDouble() * (180 - 0) + 0;
            dot.message = "Same message";
            dot.username = token;
            return repositoryDot.AddOne(dot);
        }

        public void GenerateXUsersWithYDots (int xUsers, int yDots){
            UserRegistrationModel users;
            for (int i = 0; i < xUsers; i++)
            {
                users = generateUser();
                for (int j = 0; j < yDots; j++)
                {
                    generateDots(users.Token);
                }
            }

        }


    }
}
