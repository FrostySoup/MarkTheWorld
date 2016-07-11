using Data;
using Data.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataHelpers.Map;

namespace Repository.DotRepository
{
    public interface IDotRepository
    {
        UserRegistrationModel AddOne(DotFromViewModel dot, string name, string path);
        List<Dot> GetAll(CornersCorrds corners);
        List<Dot> GetAllUserByName(CornersCorrds corners, string name);
        UserRegistrationModel AddGroup(List<DotFromViewModel> dot);
        Dot[] GetAllDotsByName(string name);
        CanMarkSpot CheckDotResults(DotFromViewModel dot);
        DotClick GetDotById(string id);
    }
}
