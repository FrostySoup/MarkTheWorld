using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuadTree.Models;

namespace QuadTree.QuadServices
{
    public class QuadService : IQuadService
    {
        public List<GroupedDotsForApi> groupDots(List<Dot> dots, CornersCorrds corners, double zoomLevel)
        {
            double lenghtPerSquare = 16;
            if (zoomLevel > 2)
                lenghtPerSquare /= Math.Pow(2, (zoomLevel - 3));
            if (corners.neX < corners.swX)
                corners.neX = 179;
            corners.neX = lenghtPerSquare * ((int)(corners.neX / lenghtPerSquare) + 1);
            corners.neY = lenghtPerSquare * ((int)(corners.neY / lenghtPerSquare) + 1);
            corners.swX = lenghtPerSquare * (int)((corners.swX / lenghtPerSquare) - 1);
            corners.swY = lenghtPerSquare * (int)((corners.swY / lenghtPerSquare) - 1);

            double squarex = corners.neX - corners.swX;
            double squarey = corners.neY - corners.swY;
            double neX = corners.neX + squarex / 3;
            double neY = corners.neY + squarey / 3;
            double swX = corners.swX - squarex / 3;
            double swY = corners.swY - squarey / 3;
            if (neX < swX)
                neX = 179;
            if (squarex < 0)
                squarex = 179 - corners.swX;
            if (neX > 180 || neX < -180)
                neX = 179;
            if (swX > 180 || swX < -180)
                swX = -179;
            if (neY > 90 || neY < -90)
                neY = 89;
            if (swY > 90 || swY < -90)
                swY = -89;
            TBQuadTreeNodeData node;
            quadTreeNode quadTree = new quadTreeNode(new TBBoundingBox(swX, swY, neX, neY), 1);
            foreach (Dot dot in dots)
            {
                node = new TBQuadTreeNodeData(dot.lat, dot.lon, dot);
                quadTree.addPoint(node);
            }

            int howManySquaresX = (int)Math.Ceiling(squarex / lenghtPerSquare);
            int howManySquaresY = (int)Math.Ceiling(squarey / lenghtPerSquare);

            List<TBQuadTreeNodeData>[] block = new List<TBQuadTreeNodeData>[howManySquaresX * howManySquaresY];


            for (int j = 0; j < howManySquaresY; j++)
            {
                for (int i = 0; i < howManySquaresX; i++)
                {
                    neX = corners.neX - lenghtPerSquare * (howManySquaresX - i - 1);
                    neY = corners.neY - lenghtPerSquare * j;
                    swX = corners.neX - lenghtPerSquare * (howManySquaresX - i);
                    swY = corners.neY - lenghtPerSquare * (j + 1);
                    block[i + j * howManySquaresX] = new List<TBQuadTreeNodeData>();
                    quadTree.TBQuadTreeGatherDataInRange(quadTree, new TBBoundingBox(swX, swY, neX, neY), block[i + j * howManySquaresX]);
                }
            }

            List<GroupedDotsForApi> groupedDotsApi = new List<GroupedDotsForApi>();
            for (int i = 0; i < howManySquaresX * howManySquaresY; i++)
            {
                if (block[i].Count > 0)
                    groupedDotsApi.Add(new GroupedDotsForApi(block[i]));
            }

            combineGroupedDots(groupedDotsApi, (lenghtPerSquare * 0.6));
            groupedDotsApi.RemoveAll(item => item == null);
            return groupedDotsApi;
        }

        private void combineGroupedDots(List<GroupedDotsForApi> groupedDotsApi, double distanceAllowed)
        {
            for (int i = 0; i < groupedDotsApi.Count; i++)
            {
                for (int j = i + 1; j < groupedDotsApi.Count; j++)
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
            double distance = Math.Pow(Math.Pow((groupA.lat - groupB.lat), 2) + Math.Pow((groupA.lon - groupB.lon), 2), 0.5);
            if ((distanceAllowed - distance) > 0)
                return true;
            return false;
        }

    }
}