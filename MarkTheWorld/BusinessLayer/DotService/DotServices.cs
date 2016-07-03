using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.DataHelpers;
using CSharpQuadTree;
using Data.DataHelpers.Map;

namespace BusinessLayer.DotService
{
    public partial class DotServices : IDotService
    {

        private Repository.GenericRepository.IGenericRepository repository = new Repository.GenericRepository.GenericRepository();
        private Repository.DotRepository.IDotRepository repositoryDot = new Repository.DotRepository.DotRepository();

        public Dot deleteDot(string dotId)
        {
            return repository.Delete<Dot>(dotId);
        }

        public UserRegistrationModel storegroupDots(List<DotFromViewModel> dot)
        {
            return repositoryDot.AddGroup(dot);
        }

        public List<Dot> getAllDots(CornersCorrds corners)
        {
            return repositoryDot.GetAll(corners);
        }

        public UserRegistrationModel storeDot(DotFromViewModel dot)
        {
            return repositoryDot.AddOne(dot);
        }

        public List<CornersCorrds> getAllSquares(List<Dot> dots)
        {
            List<CornersCorrds> squares = new List<CornersCorrds>();
           
            return squares;
        }

        public List<Dot> getUserDotsName(CornersCorrds corners, string name)
        {
            return repositoryDot.GetAllUserByName(corners, name);
        }

        public int getUserPointsName(Dot[] squares)
        {
            return maxConnection(squares);
        }

        public Dot[] getAlluserDots(string name)
        {
            return repositoryDot.GetAllDotsByName(name);
        }

        public CanMarkSpot CheckDot(DotFromViewModel dot)
        {
            return repositoryDot.CheckDotResults(dot);
        }

    }
}
