using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpQuadTree;
using Data.DataHelpers.Map;
using System.Web;
using Data.Database;
using Data.DataHelpers;
using Data;

namespace BusinessLayer.DotService
{
    public partial class DotServices
    {
        private Repository.DotRepository.DotRepository repositoryDot = new Repository.DotRepository.DotRepository();

        public async Task<List<Dot>> getAllDots(CornersCorrds corners)
        {
            return await repositoryDot.GetAll(corners);
        }

        public async Task<UserRegistrationModel> storeDot(DotFromViewModel dot, string image = null)
        {
            string path = HttpRuntime.AppDomainAppPath;
            return await repositoryDot.AddOne(dot, image, path);
        }


        public async Task<List<Dot>> getUserDotsName(CornersCorrds corners, string name)
        {
            return await repositoryDot.GetAllUserByName(corners, name);
        }

        public int getUserPointsName(Dot[] squares)
        {
            return maxConnection(squares);
        }

        public async Task<Dot[]> getAlluserDots(string name)
        {
            return await repositoryDot.GetAllDotsByName(name);
        }

        public async Task<CanMarkSpot> CheckDot(DotFromViewModel dot)
        {
            return await repositoryDot.CheckDotResults(dot);
        }

        public async Task<DotClick> GetDotWithInfo(string id)
        {
            DotClick dot = await repositoryDot.GetDotById(id);
            dot.pathCountryFlag = "/Content/img/flags/" + dot.country + ".png";
            dot.country = CountriesList.getCountry(dot.country).name;
            if (!dot.profilePic.Contains("facebook"))
                dot.profilePic = "/Content/img/avatars/" + dot.profilePic;
            return dot;
        }
    }
}
