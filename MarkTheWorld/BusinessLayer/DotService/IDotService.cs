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
        Dot storeDot(DotFromViewModel dot);
        List<Dot> getAllDots(CornersCorrds corners);
        Dot deleteDot(string dotId);
    }
}
