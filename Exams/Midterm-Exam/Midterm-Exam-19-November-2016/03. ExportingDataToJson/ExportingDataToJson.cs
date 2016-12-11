using MassDefect.Data;
using MassDefect.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.ExportingDataToJson
{
    public class ExportingDataToJson
    {
        public const string RootDirectory = "../../";

        public static void Main(string[] args)
        {
            //ExportPlanetsThatAreNotOriginPlanet();
            //PeopleWhichHaveNotBeenVictims();
            AnomalyWhichHasAffectedTheMostVictims();
        }

        private static void AnomalyWhichHasAffectedTheMostVictims()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                var mostAffectiveAnomaly = context.Anomalies
                    .OrderByDescending(a => a.Victims.Count)
                    .Select(a => new
                    {
                        id = a.Id,
                        originPlanet = new { name = a.OriginPlanet.Name },
                        teleportPlanet = new { name = a.TeleportPlanet.Name },
                        victimsCount = a.Victims.Count
                    })
                    .First();

                string anomalyJson = JsonConvert.SerializeObject(mostAffectiveAnomaly, Formatting.Indented);

                File.WriteAllText(RootDirectory + "/anomaly.json", anomalyJson);
            }
        }

        private static void PeopleWhichHaveNotBeenVictims()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                var people = context.People
                    .Where(p => !p.Anomalies.Any())
                    .Select(p => new
                    {
                        name = p.Name,
                        homePlanet = new { name = p.HomePlanet.Name }
                    });

                string peopleJson = JsonConvert.SerializeObject(people, Formatting.Indented);

                File.WriteAllText(RootDirectory + "/people.json", peopleJson);
            }
        }

        private static void ExportPlanetsThatAreNotOriginPlanet()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                var planets = context.Planets
                    .Where(p => !p.Anomalies.Any(a => a.OriginPlanetId == p.Id))
                    .Select(p => new
                    {
                        name = p.Name
                    });

                string planetsJson = JsonConvert.SerializeObject(planets, Formatting.Indented);

                File.WriteAllText(RootDirectory + "/planets.json", planetsJson);
            }
        }
    }
}
