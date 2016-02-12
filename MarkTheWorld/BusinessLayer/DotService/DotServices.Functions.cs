﻿using System;
using System.Collections.Generic;
using Data;
using Data.DataHelpers;
using CSharpQuadTree;

namespace BusinessLayer.DotService
{
    public partial class DotServices
    {
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

            return groupedDotsApi;
        }


        public int maxConnection(Dot[] squares)
        {
            int maxCon = 0;
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
                    findConnections(connected, quadTree, squares[i], conectedSquares, checkedDots, ref maxCon);
                }
            }
            return maxCon;
        }

        private void findConnections(int connected, quadTreeNode quadTree, Dot dot, List<Dot> conectedSquares, int[] checkedDots, ref int max)
        {
            CornersCorrds square = coordsToSquare(dot.lat, dot.lon);
            double neX = square.neX + 0.01;
            double neY = square.neY + 0.01;
            double swX = square.swX - 0.01;
            double swY = square.swY - 0.01;
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
                        findConnections(connected, quadTree, new Dot(node.x, node.y), conectedSquares, checkedDots, ref max);                     
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
                    if (System.Math.Round(squareOne.neY + constant, 2) == System.Math.Round(squareTwo.neY, 2) || System.Math.Round(squareOne.neY - constant, 2) == System.Math.Round(squareTwo.neY, 2))
                        return true;
                }
                else if (squareOne.neY == squareTwo.neY)
                {
                    if (System.Math.Round(squareOne.neX + constant, 2) == System.Math.Round(squareTwo.neX, 2) || System.Math.Round(squareOne.neX - constant, 2) == System.Math.Round(squareTwo.neX, 2))
                        return true;
                }
            }
            return false;
        }

    }
}