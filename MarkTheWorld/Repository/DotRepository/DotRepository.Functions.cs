using Data;
using Data.Database;
using Data.DataHelpers;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DotRepository
{
    public partial class DotRepository
    {
        const double circleR = 0.0025;
        private string cutImageName(string photoUrl)
        {
            if (photoUrl == null)
                return null;
            return photoUrl.Substring(photoUrl.LastIndexOf('/') + 1);           
        }

        private Dot changeDotValues(DateTime today, string message, double lng, double lat, string userName, string v, Dot oldDot = null)
        {
            Dot dot = new Dot();
            if (oldDot != null)
            {               
                dot = oldDot;
                double[] hold = generateCaptureSpot(dot.lon, dot.lat);
                dot.nextCapLon = hold[0];
                dot.nextCapLat = hold[1];
            }
            dot.lat = lat;
            dot.lon = lng;
            dot.message = message;
            dot.photoPath = v;
            dot.username = userName;
            dot.date = today;
            double[] holder = generateCaptureSpot(dot.lon, dot.lat);
            dot.nextCapLat = holder[1];
            dot.nextCapLon = holder[0];
            return dot;
        }

        // coords[0] - x - lon
        // coords[1] - y - lat
        private double[] generateCaptureSpot(double lon, double lat)
        {
            double[] coords = new double[2];
            CornersCorrds corners = coordsToSquare(lon, lat);
            double minimumX = corners.swX + circleR;
            double maximumX = corners.neX - circleR;
            double minimumY = corners.swY + circleR;
            double maximumY = corners.neY - circleR;
            Random random = new Random();
            coords[0] = random.NextDouble() * (maximumX - minimumX) + minimumX;
            coords[1] = random.NextDouble() * (maximumX - minimumY) + minimumY;
            return coords;
        }

        // coords[0] - x - lon
        // coords[1] - y - lat
        private double[] centreCapturePoint(double lon, double lat)
        {
            double[] coords = new double[2];
            CornersCorrds corners = coordsToSquare(lon, lat);
            Random random = new Random();
            coords[0] = Math.Round(((corners.neX + corners.swX) / 2), 3);
            coords[1] = Math.Round(((corners.neY + corners.swY) / 2), 3);
            return coords;
        }

        private double[] centerCoords(DotFromViewModel dot)
        {
            double[] coords = centreCapturePoint(dot.lng, dot.lat);
            return coords;
        }

        private bool checkCenter(double lat, double lng)
        {
            double[] center = centreCapturePoint(lat, lng);
            if (checkIfInside(lat, lng, center[1], center[0]))
                return true;
            return false;
        }

        private bool checkTerritory(Dot dot)
        {
            double[] center = centreCapturePoint(dot.lat, dot.lon);
            if (checkIfInside(dot.lat, dot.lon, dot.nextCapLat, dot.nextCapLon))
                return true;
            return false;
        }

        private bool checkIfInside(double lat, double lng, double allowedLat, double allowedLng)
        {
            double range = Math.Pow(Math.Pow((lat - allowedLat), 2) + Math.Pow((lng - allowedLng), 2), 0.5);
            if (range < 0.0025)
                return true;
            return false;
        }

        private CornersCorrds coordsToSquare(double lat, double lng)
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

        private void removeOld(string oldPhoto, string path)
        {
            if (oldPhoto == null)
                return;
            string pathRem = path.Replace("\\", "/") + "Content/img/dotLocation/" + oldPhoto;
            try
            {
                System.IO.File.Delete(pathRem);
            }
            catch {
                using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(path + "Content/failedToRemovePhoto.txt", true))
                {
                    file.WriteLine(oldPhoto);
                }
            }
        }

        private void addEvent(User userChanged, User user, Dot dot, IDocumentSession session)
        {
            Events myEvent = new Events("You captured square", dot.lon, dot.lat, userChanged.UserName);
            Events opponentEvent = new Events("Player captured your square", dot.lon, dot.lat, user.UserName);
            session.Store(myEvent);
            session.Store(opponentEvent);

            if (userChanged.eventsID == null)
                userChanged.eventsID = new List<string>();           

            userChanged.eventsID.Add(opponentEvent.id);
            if (userChanged.eventsID.Count > 10)
            {
                Events eventToRemove = session.Load<Events>(userChanged.eventsID[10]);
                if (eventToRemove != null)
                    session.Delete(eventToRemove);
                userChanged.eventsID.RemoveRange(10, 1);
            }

            if (user.eventsID == null)
                user.eventsID = new List<string>();
            user.eventsID.Add(myEvent.id);
            if (user.eventsID.Count > 10)
            {
                Events eventToRemove = session.Load<Events>(user.eventsID[10]);
                if (eventToRemove != null)
                    session.Delete(eventToRemove);
                user.eventsID.RemoveRange(10, 1);
            }
        }

        private void addEventNewDot(IDocumentSession session, User user, double lon, double lat)
        {
            Events myEvent = new Events("You marked new square", lon, lat, null);
            session.Store(myEvent);
            if (user.eventsID == null)
                user.eventsID = new List<string>();
            user.eventsID.Add(myEvent.id);
            if (user.eventsID.Count > 10)
            {
                Events eventToRemove = session.Load<Events>(user.eventsID[10]);
                if (eventToRemove != null)
                    session.Delete(eventToRemove);
                user.eventsID.RemoveRange(10, 1);
            }
        }
    }
}
