using Data;
using Data.ReceivePostData;
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
            UserRegistrationPost user = new UserRegistrationPost();
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

        public void generateDots(string token, int numberToGen)
        {
            DotFromViewModel dot = new DotFromViewModel();
            Random rnd = new Random();
            dot.lat = rnd.NextDouble() * 90;
            dot.lng = rnd.NextDouble() * (180 - 0) + 0;
            dot.message = "Same message";
            dot.token = token;
            repositoryDot.AddOne(dot, null, null);
            for (int i = 1; i < numberToGen; i++)
            {
                DotFromViewModel dotNew = new DotFromViewModel();
                dotNew.lat = dot.lat + rnd.NextDouble() * 0.03;
                dotNew.lng = dot.lng + rnd.NextDouble() * 0.03;
                dotNew.message = "Same message";
                dotNew.token = token;
                repositoryDot.AddOne(dotNew, null, null);
            }
        }

        public void GenerateXUsersWithYDots (int xUsers, int yDots){
            UserRegistrationModel users;
            for (int i = 0; i < xUsers; i++)
            {
                users = generateUser();             
                generateDots(users.Token, yDots);
            }

        }


    }
}
