using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpQuadTree
{
    public class TBQuadTreeNodeData
    {
        public double x;
        public double y;
        public Dot data;
        public int number;
        public TBQuadTreeNodeData(double y, double x, Dot dat)
        {
            this.x = x;
            this.y = y;
            data = dat;
        }
        public TBQuadTreeNodeData(double y, double x, Dot dat, int num)
        {
            this.x = x;
            this.y = y;
            data = dat;
            number = num;
        }

    }

    public class TBBoundingBox
    {
        public double x0; public double y0;
        public double xf; public double yf;
        public TBBoundingBox(double x0, double y0, double xf, double yf)
        {
            this.x0 = x0;
            this.y0 = y0;
            this.xf = xf;
            this.yf = yf;
        }
    }

    public class quadTreeNode
    {
        public quadTreeNode northWest;
        public quadTreeNode northEast;
        public quadTreeNode southWest;
        public quadTreeNode southEast;
        public TBBoundingBox boundingBox;
        public int bucketCapacity;
        public List<TBQuadTreeNodeData> points;
        public int count;

        public void addPoint(TBQuadTreeNodeData data)
        {
            if (TBQuadTreeNodeInsertData(this, data))
            {
                //points.Add(data);
            }
            
        }

        public void TBQuadTreeNodeSubdivide(quadTreeNode node)
        {
            TBBoundingBox box = node.boundingBox;

            double xMid = (box.xf + box.x0) / 2.0;
            double yMid = (box.yf + box.y0) / 2.0;

            TBBoundingBox northWest = new TBBoundingBox(box.x0, yMid, xMid, box.yf);
            node.northWest = new quadTreeNode(northWest, node.bucketCapacity);

            TBBoundingBox northEast = new TBBoundingBox(xMid, yMid, box.xf, box.yf);
            node.northEast = new quadTreeNode(northEast, node.bucketCapacity);

            TBBoundingBox southWest = new TBBoundingBox(box.x0, box.y0, xMid, yMid);
            node.southWest = new quadTreeNode(southWest, node.bucketCapacity);

            TBBoundingBox southEast = new TBBoundingBox(xMid, box.y0, box.xf, yMid);
            node.southEast = new quadTreeNode(southEast, node.bucketCapacity);
        }

        public bool TBQuadTreeNodeInsertData(quadTreeNode node, TBQuadTreeNodeData data)
        {
            // Bail if our coordinate is not in the boundingBox
            if (!TBBoundingBoxContainsData(node.boundingBox, data))
            {
                return false;
            }

            // Add the coordinate to the points array
            if (node.count < node.bucketCapacity)
            {
                node.count++;
                node.points.Add(data);
                return true;
            }

            // Check to see if the current node is a leaf, if it is, split
            if (node.northWest == null)
            {
                TBQuadTreeNodeSubdivide(node);
            }

            // Traverse the tree
            if (TBQuadTreeNodeInsertData(node.northWest, data)) return true;
            if (TBQuadTreeNodeInsertData(node.northEast, data)) return true;
            if (TBQuadTreeNodeInsertData(node.southWest, data)) return true;
            if (TBQuadTreeNodeInsertData(node.southEast, data)) return true;

            return false;
        }

        public void TBQuadTreeGatherDataInRange(quadTreeNode node, TBBoundingBox range, List<TBQuadTreeNodeData> block)
        {
            // If range is not contained in the node's boundingBox then bail
            if (!TBBoundingBoxIntersectsBoundingBox(node.boundingBox, range))
            {
                return;
            }

            for (int i = 0; i < node.count; i++)
            {
                // Gather points contained in range
                if (TBBoundingBoxContainsData(range, node.points[i]))
                {
                    block.Add(node.points[i]);
                }
            }

            // Bail if node is leaf
            if (node.northWest == null)
            {
                return;
            }

            // Otherwise traverse down the tree
            TBQuadTreeGatherDataInRange(node.northWest, range, block);
            TBQuadTreeGatherDataInRange(node.northEast, range, block);
            TBQuadTreeGatherDataInRange(node.southWest, range, block);
            TBQuadTreeGatherDataInRange(node.southEast, range, block);
        }

        private bool TBBoundingBoxIntersectsBoundingBox(TBBoundingBox boundingBox, TBBoundingBox range)
        {
            if (boundingBox.xf > range.x0 && boundingBox.yf > range.y0)
                return true;
            if (boundingBox.x0 < range.xf && boundingBox.yf > range.y0)
                return true;
            if (boundingBox.xf > range.x0 && boundingBox.y0 < range.yf)
                return true;
            if (boundingBox.x0 < range.xf && boundingBox.y0 < range.yf)
                return true;
            else return false;
        }

        private bool TBBoundingBoxContainsData(TBBoundingBox boundingBox, TBQuadTreeNodeData data)
        {
            if (boundingBox.x0 <= data.x && boundingBox.xf >= data.x && boundingBox.y0 <= data.y && boundingBox.yf >= data.y)
                return true;
            else return false;
        }

        public quadTreeNode(TBBoundingBox boundary, int bucketCapacity)
        {
            points = new List<TBQuadTreeNodeData>();
            count = 0;
            this.bucketCapacity = bucketCapacity;
            boundingBox = boundary;
        }
    }
}