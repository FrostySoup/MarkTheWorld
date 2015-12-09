using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.DataHelpers;

namespace BusinessLayer.DotService
{
    public class DotServices : IDotService
    {

        private Repository.GenericRepository.IGenericRepository repository = new Repository.GenericRepository.GenericRepository();
        private Repository.DotRepository.IDotRepository repositoryDot = new Repository.DotRepository.DotRepository();

        public Dot deleteDot(string dotId)
        {
            return repository.Delete<Dot>(dotId);
        }

        public List<Dot> getAllDots(CornersCorrds corners)
        {
            return repositoryDot.GetAll(corners);
        }

        public UserRegistrationModel storeDot(DotFromViewModel dot)
        {
            return repositoryDot.AddOne(dot);
        }

        public CornersCorrds coordsToSquare(double lat, double lng)
        {
            CornersCorrds corners = new CornersCorrds();
            double late = (double)((int)(lat*100))/100;
            double longt = (double)((int)(lng * 100)) / 100;
            corners.swX = longt + 0.01;
            corners.neX = longt;
            corners.neY = late + 0.01;
            corners.swY = late;
            if (corners.neX < corners.swX)
            {
                double laikinas = corners.neX;
                corners.neX = corners.swX;
                corners.swX = laikinas;
            }
            if (corners.neY < corners.swY)
            {
                double laikinas = corners.neY;
                corners.neY = corners.swY;
                corners.swY = laikinas;
            }
            return corners;
        }

        public List<CornersCorrds> getAllSquares(List<Dot> dots)
        {
            List<CornersCorrds> squares = new List<CornersCorrds>();
           
            return squares;
        }

        public List<Dot> getUserDots(CornersCorrds corners, Guid token)
        {
            return repositoryDot.GetAllUser(corners, token);
        }

        public List<SquaresWithInfo> groupSquares(List<Square> squares)
        {
            List<SquaresWithInfo> grSquares = new List<SquaresWithInfo>();
            foreach (Square square in squares)
            {
                bool foundMatch = false;
                foreach (SquaresWithInfo grSquare in grSquares)
                {
                    if (!foundMatch)
                        if (Math.Abs(square.neX - grSquare.neX) < 0.0001 && Math.Abs(square.neY - grSquare.neY) < 0.0001)
                        {
                            foundMatch = true;
                            grSquare.markers.Add(new Markers(square.date, square.username, square.message));
                        }                  
                }
                if (!foundMatch)
                {
                    grSquares.Add(new SquaresWithInfo(square.neX, square.neY, square.swX, square.swY, new Markers(square.date, square.username, square.message)));
                }
            }

            return grSquares;
        }


    }
}
