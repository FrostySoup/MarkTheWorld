using Data.Database;

namespace Data.DataHelpers
{
    public class DotSearch
    {
        public int number { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public DotSearch() { number = 0; }
        public DotSearch(Dot dot, int num)
        {
            lat = dot.lat;
            lon = dot.lon;
            number = num;
        }
    }
}
