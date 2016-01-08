using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.DataHelpers;
using CSharpQuadTree;

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

        public List<Dot> getUserDotsName(CornersCorrds corners, string name)
        {
            return repositoryDot.GetAllUserByName(corners, name);
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

        public List<GroupedDotsForApi> groupDots(List<Dot> dots, CornersCorrds corners)
        {
            TBQuadTreeNodeData node;
            quadTreeNode quadTree = new quadTreeNode(new TBBoundingBox(corners.swX, corners.swY, corners.neX, corners.neY), 1);
            foreach (Dot dot in dots)
            {
                node = new TBQuadTreeNodeData(dot.lat, dot.lon, dot);
                quadTree.addPoint(node);
            }

            int sqNum = 6;
            GroupedDots[] groupedDots = new GroupedDots[sqNum* sqNum];
            double squareX = (corners.neX - corners.swX)/ sqNum;
            double squareY = (corners.neY - corners.swY) / sqNum;
            /*corners.neX = Math.Ceiling(corners.neX/ squareX)* squareX;
            corners.swX = Math.Floor(corners.swX/ squareX) * squareX;
            corners.neY = Math.Ceiling(corners.neY / squareY) * squareY;
            corners.swY = Math.Floor(corners.swY / squareY) * squareY;*/
            List<GroupedDotsForApi> groupedDotsApi = new List<GroupedDotsForApi>();
            for (int j = 0; j < sqNum; j++)
            {
                for (int i = 0; i < sqNum; i++)
                {
                    groupedDots[i + sqNum * j] = new GroupedDots();
                    groupedDots[i + sqNum * j].neX = corners.neX - squareX * i;
                    groupedDots[i + sqNum * j].neY = corners.neY - squareY * j;
                    groupedDots[i + sqNum * j].swX = corners.neX - squareX * (i + 1);
                    groupedDots[i + sqNum * j].swY = corners.neY - squareY * (j + 1);
                }
            }

            for (int i = 0; i < dots.Count; i++)
            {
                bool found = false;
                int j = 0;
                while(!found && j < groupedDots.Length)
                {
                    if (dots[i].lon >= groupedDots[j].swX && dots[i].lon <= groupedDots[j].neX
                        && dots[i].lat >= groupedDots[j].swY && dots[i].lat <= groupedDots[j].neY)
                    {
                        found = true;
                        groupedDots[j].dots.Add(dots[i]);
                    }
                    j++;
                }
            }

            foreach(GroupedDots group in groupedDots)
            {
                if (group.dots.Count > 0)
                    groupedDotsApi.Add(new GroupedDotsForApi(group.dots));
            }

            return groupedDotsApi;
        }
    }
}
