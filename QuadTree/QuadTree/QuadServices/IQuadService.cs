using QuadTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuadTree.QuadServices
{
    public interface IQuadService
    {
        List<GroupedDotsForApi> groupDots(List<Dot> dots, CornersCorrds corners, double zoomLevel);
    }
}