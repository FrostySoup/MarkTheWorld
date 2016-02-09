using CSharpQuadTree;
using Data;
using Data.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DotService
{
    public interface IDotService
    {
        UserRegistrationModel storegroupDots(List<DotFromViewModel> dot);
        UserRegistrationModel storeDot(DotFromViewModel dot);
        List<Dot> getAllDots(CornersCorrds corners);
        Dot deleteDot(string dotId);
        List<CornersCorrds> getAllSquares(List<Dot> dots);
        List<Dot> getUserDotsName(CornersCorrds corners, string name);


        //functions
        List<SquaresWithInfo> groupSquares(List<Square> squares);
        List<GroupedDotsForApi> groupDots(List<Dot> dots, CornersCorrds corners, double zoomLevel);
        int maxConnection(string name, Dot[] squares);
    }
}
