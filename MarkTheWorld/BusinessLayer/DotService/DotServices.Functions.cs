using System;
using System.Collections.Generic;
using Data;
using Data.DataHelpers;
using CSharpQuadTree;
using Data.DataHelpers.Map;
using Data.Database;
using Data.QuadTrees;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace BusinessLayer.DotService
{

    public partial class DotServices
    {

        private readonly string URL = "http://localhost:56381/api/QuadTree";

        private Repository.UserRepository.UserRepository userRepository = new Repository.UserRepository.UserRepository();

        public CornersCorrds coordsToSquare(double lat, double lng)
        {
            CornersCorrds corners = new CornersCorrds();
            double late = (double)((int)(lat * 100)) / 100;
            double longt = (double)((int)(lng * 100)) / 100;
            late = System.Math.Round(late, 2);
            longt = System.Math.Round(longt, 2);
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

        public async Task<List<GroupedDotsForApi>> groupDots(List<Dot> dots, CornersCorrds corners, double zoomLevel)
        {
            GetGroupDotsDataReceived getGroupDotsDataReceived = new GetGroupDotsDataReceived()
            {
                dots = dots,
                corners = corners,
                zoomLevel = zoomLevel
            };

            var results = new List<GroupedDotsForApi>();

            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:56381/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(URL, getGroupDotsDataReceived);

                if (response.IsSuccessStatusCode)
                {
                    results = await response.Content.ReadAsAsync<List<GroupedDotsForApi>>();
                }else
                {
                    return null;
                }
            }
            
            return results;
        }      

        private void combineGroupedDots(List<GroupedDotsForApi> groupedDotsApi, double distanceAllowed)
        {
            for (int i = 0; i < groupedDotsApi.Count; i++)
            {
                for (int j = i+1; j < groupedDotsApi.Count; j++)
                {
                    if (checkTwoGroups(groupedDotsApi[i], groupedDotsApi[j], distanceAllowed))
                    {
                        groupedDotsApi[i].addDots(groupedDotsApi[j]);
                        groupedDotsApi[j] = null;
                    }
                }
            }
        }

        private bool checkTwoGroups(GroupedDotsForApi groupA, GroupedDotsForApi groupB, double distanceAllowed)
        {
            if (groupA == null || groupB == null)
                return false;
            double distance = Math.Pow(Math.Pow((groupA.lat - groupB.lat),2) + Math.Pow((groupA.lon - groupB.lon), 2), 0.5);
            if ((distanceAllowed - distance) > 0)
                return true;
            return false;
        }

        public int maxConnection(Dot[] squares)
        {
            int maxCon = 1;
            List<DotSearch> dotsToSearch = new List<DotSearch>();

            int[] checkedDots = new int[squares.Length];

            int nr = 0;
            foreach (Dot dot in squares)
            {
                dotsToSearch.Add(new DotSearch(dot, nr));
                nr++;
            }
            quadTreeNode quadTree = new quadTreeNode(new TBBoundingBox(-180, -90, 180, 90), 1);
            TBQuadTreeNodeData node;
            foreach (DotSearch dot in dotsToSearch)
            {
                node = new TBQuadTreeNodeData(dot.lat, dot.lon, new Dot(dot.lon, dot.lat), dot.number);
                quadTree.addPoint(node);
            }

            for (int i = 0; i < squares.Length; i++)
            {
                if (checkedDots[i] == 0)
                {
                    List<Dot> conectedSquares = new List<Dot>();
                    conectedSquares.Add(squares[i]);
                    int connected = 1;
                    checkedDots[i] = 1;
                    findConnections(ref connected, quadTree, squares[i], conectedSquares, checkedDots, ref maxCon);
                }
            }
            return maxCon;
        }

        private void findConnections(ref int connected, quadTreeNode quadTree, Dot dot, List<Dot> conectedSquares, int[] checkedDots, ref int max)
        {
            CornersCorrds square = coordsToSquare(dot.lat, dot.lon);
            double neX = square.neX + 0.011;
            double neY = square.neY + 0.011;
            double swX = square.swX - 0.011;
            double swY = square.swY - 0.011;
            List<TBQuadTreeNodeData> block = new List<TBQuadTreeNodeData>();
            quadTree.TBQuadTreeGatherDataInRange(quadTree, new TBBoundingBox(swX, swY, neX, neY), block);
            foreach(TBQuadTreeNodeData node in block)
            {
                if (checkedDots[node.number] == 0)
                    if (checkConnections(node, conectedSquares))
                    {
                        conectedSquares.Add(new Dot(node.x, node.y));
                        connected++;
                        checkedDots[node.number] = 1;
                        if (max < connected)
                            max = connected;
                        findConnections(ref connected, quadTree, new Dot(node.x, node.y), conectedSquares, checkedDots, ref max);                     
                    }
            }
        }

        private bool checkConnections(TBQuadTreeNodeData dot, List<Dot> conectedSquares)
        {
            foreach (Dot dotFromList in conectedSquares)
            {
                CornersCorrds squareOne = coordsToSquare(dot.y, dot.x);
                CornersCorrds squareTwo = coordsToSquare(dotFromList.lat, dotFromList.lon);
                double constant = 0.01;
                if (squareOne.neX == squareTwo.neX)
                {
                    if (Math.Abs(System.Math.Round(squareOne.neY + constant, 2) - System.Math.Round(squareTwo.neY, 2)) < 0.005 
                        || Math.Abs(System.Math.Round(squareOne.neY - constant, 2) - System.Math.Round(squareTwo.neY, 2)) < 0.005)
                        return true;
                }
                else if (squareOne.neY == squareTwo.neY)
                {
                    if (Math.Abs(System.Math.Round(squareOne.neX + constant, 2) - System.Math.Round(squareTwo.neX, 2)) < 0.005 
                        || Math.Abs(System.Math.Round(squareOne.neX - constant, 2) - System.Math.Round(squareTwo.neX, 2)) < 0.005)
                        return true;
                }
            }
            return false;
        }

    }
}
