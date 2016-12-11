namespace _01.ImportingData
{
    using MassDefect.Data;
    using MassDefect.Models;
    using MassDefect.Models.DTO_s;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ImportingData
    {
        public const string InvalidDataMessage = "Error: Invalid data.";
        public const string RootDirectory = "../../";
        public const string SolarSystemPath = RootDirectory + "datasets/solar-systems.json";
        public const string StarsPath = RootDirectory + "datasets/stars.json";
        public const string PlanetsPath = RootDirectory + "datasets/planets.json";
        public const string PeoplePath = RootDirectory + "datasets/persons.json";
        public const string AnomaliesPath = RootDirectory + "datasets/anomalies.json";
        public const string AnomalyVictimsPath = RootDirectory + "datasets/anomaly-victims.json";

        public static void Main(string[] args)
        {
            //ImportSolarSystems();
            //ImportStars();
            //ImportPlanets();
            //ImportPeople();
            //ImportAnomalies();
            //ImportAnomalyVictims();
        }

        private static void ImportAnomalyVictims()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                string anomalyVictimsJson = File.ReadAllText(AnomalyVictimsPath);
                IEnumerable<AnomalyVictimDTO> anomalyVictims = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimDTO>>(anomalyVictimsJson);

                foreach (AnomalyVictimDTO anomalyVictim in anomalyVictims)
                {
                    if (anomalyVictim.Id == null || anomalyVictim.Person == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    Anomaly anomalyEntity = GetAnomalyById(anomalyVictim.Id, context);
                    Person personEntity = GetPersonByName(anomalyVictim.Person, context);

                    if (anomalyEntity == null || personEntity == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    anomalyEntity.Victims.Add(personEntity);
                }

                context.SaveChanges();
            }
        }

        private static void ImportAnomalies()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                string anomaliesJson = File.ReadAllText(AnomaliesPath);
                IEnumerable<AnomalyDTO> anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyDTO>>(anomaliesJson);

                foreach (AnomalyDTO anomaly in anomalies)
                {
                    if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    Anomaly anomalyEnity = new Anomaly()
                    {
                        OriginPlanet = GetPlanetByName(anomaly.OriginPlanet, context),
                        TeleportPlanet = GetPlanetByName(anomaly.TeleportPlanet, context)
                    };

                    if (anomalyEnity.OriginPlanet == null || anomalyEnity.TeleportPlanet == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    context.Anomalies.Add(anomalyEnity);
                    Console.WriteLine($"Successfully imported anomaly.");
                }

                context.SaveChanges();
            }
        }

        private static void ImportPeople()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                string peopleJson = File.ReadAllText(PeoplePath);
                IEnumerable<PersonDTO> people = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(peopleJson);

                foreach (PersonDTO person in people)
                {
                    if (person.Name == null || person.HomePlanet == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    Person personEntity = new Person()
                    {
                        Name = person.Name,
                        HomePlanet = GetPlanetByName(person.HomePlanet, context)
                    };

                    if (personEntity.HomePlanet == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    context.People.Add(personEntity);
                    Console.WriteLine($"Successfully imported Person {personEntity.Name}.");
                }

                context.SaveChanges();
            }
        }

        private static void ImportPlanets()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                string planetsJson = File.ReadAllText(PlanetsPath);
                IEnumerable<PlanetDTO> planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(planetsJson);

                foreach (PlanetDTO planet in planets)
                {
                    if (planet.Name == null || planet.SolarSystem == null || planet.Sun == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    Planet planetEntity = new Planet()
                    {
                        Name = planet.Name,
                        Sun = GetSunByName(planet.Sun, context),
                        SolarSystem = GetSolarSystemByName(planet.SolarSystem, context)
                    };

                    if (planetEntity.Sun == null || planet.SolarSystem == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    context.Planets.Add(planetEntity);
                    Console.WriteLine($"Successfully imported Planet {planetEntity.Name}");
                }

                context.SaveChanges();
            }
        }

        private static void ImportStars()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                string starsJson = File.ReadAllText(StarsPath);
                IEnumerable<StarDTO> stars = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(starsJson);

                foreach (var star in stars)
                {
                    if (star.Name == null || star.SolarSystem == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    Star starEntity = new Star()
                    {
                        Name = star.Name,
                        SolarSystem = GetSolarSystemByName(star.SolarSystem, context),
                    };


                    if (starEntity == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    context.Stars.Add(starEntity);
                    Console.WriteLine($"Successfully imported Star {starEntity.Name}");
                }

                context.SaveChanges();
            }
        }

        private static void ImportSolarSystems()
        {
            using (MassDefectContext context = new MassDefectContext())
            {
                string solarSystemsJson = File.ReadAllText(SolarSystemPath);
                IEnumerable<SolarSystemDTO> solarSystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(solarSystemsJson);

                foreach (SolarSystemDTO solarSystem in solarSystems)
                {
                    if (solarSystem.Name == null)
                    {
                        Console.WriteLine(InvalidDataMessage);
                        continue;
                    }

                    SolarSystem solarSystemEntity = new SolarSystem()
                    {
                        Name = solarSystem.Name,
                    };

                    context.SolarSystems.Add(solarSystemEntity);
                    Console.WriteLine($"Successfully imported SolarSystem {solarSystemEntity.Name}");
                }

                context.SaveChanges();
            }
        }

        private static SolarSystem GetSolarSystemByName(string name, MassDefectContext context)
        {
            SolarSystem solarSystem = context.SolarSystems.FirstOrDefault(ss => ss.Name == name);

            return solarSystem;
        }

        private static Star GetSunByName(string name, MassDefectContext context)
        {
            Star sun = context.Stars.FirstOrDefault(s => s.Name == name);

            return sun;
        }

        private static Planet GetPlanetByName(string homePlanet, MassDefectContext context)
        {
            Planet planet = context.Planets.FirstOrDefault(p => p.Name == homePlanet);

            return planet;
        }

        private static Anomaly GetAnomalyById(int? id, MassDefectContext context)
        {
            Anomaly anomaly = context.Anomalies.Find(id);

            return anomaly;
        }

        private static Person GetPersonByName(string name, MassDefectContext context)
        {
            Person person = context.People.FirstOrDefault(p => p.Name == name);

            return person;
        }
    }
}
