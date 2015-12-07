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
        UserRegistrationModel storeDot(DotFromViewModel dot);
        List<Dot> getAllDots(CornersCorrds corners);
        Dot deleteDot(string dotId);
        List<CornersCorrds> getAllSquares(List<Dot> dots);
        List<Dot> getUserDots(CornersCorrds corners, Guid token);
    }
}
