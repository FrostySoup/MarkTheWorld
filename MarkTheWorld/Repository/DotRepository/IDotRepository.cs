using Data;
using Data.DataHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DotRepository
{
    public interface IDotRepository
    {
        UserRegistrationModel AddOne(DotFromViewModel dot);
        List<Dot> GetAll(CornersCorrds corners);
        List<Dot> GetAllUserByName(CornersCorrds corners, string name);
        UserRegistrationModel AddGroup(List<DotFromViewModel> dot);
        Dot[] GetAllDotsByName(string name);
    }
}
