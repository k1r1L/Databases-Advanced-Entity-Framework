namespace _02.ImportinDataFromXml
{
    using MassDefect.Data;
    using MassDefect.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public class ImportingDataFromXml
    {
        public const string InvalidDataMsg = "Error: Invalid data.";
        public const string SuccessMsg = "Successfully imported anomaly.";
        public const string NewAnomaliesPath = "../../datasets/new-anomalies.xml";

        public static void Main(string[] args)
        {
            XDocument xml = XDocument.Load(NewAnomaliesPath);
            IEnumerable<XElement> anomlaies = xml.XPathSelectElements("anomalies/anomaly");

            using (MassDefectContext context = new MassDefectContext())
            {
                foreach (var anomaly in anomlaies)
                {
                    ImportAnomalyAndVictims(anomaly, context);
                }

                context.SaveChanges();
            }


        }
        private static void ImportAnomalyAndVictims(XElement anomaly, MassDefectContext context)
        {

            XAttribute originPlanetAttribute = anomaly.Attribute("origin-planet");
            XAttribute teleportPlanetAttribute = anomaly.Attribute("teleport-planet");

            if (originPlanetAttribute == null || teleportPlanetAttribute == null)
            {
                Console.WriteLine(InvalidDataMsg);
                return;
            }

            Anomaly anomalyEntity = new Anomaly()
            {
                OriginPlanet = GetPlanetByName(originPlanetAttribute.Value, context),
                TeleportPlanet = GetPlanetByName(teleportPlanetAttribute.Value, context)
            };

            if (anomalyEntity.OriginPlanet == null || anomalyEntity.TeleportPlanet == null)
            {
                Console.WriteLine(InvalidDataMsg);
                return;
            }


            IEnumerable<XElement> victims = anomaly.XPathSelectElements("victims/victim");
            foreach (XElement victim in victims)
            {
                ImportVictim(victim, context, anomalyEntity);
            }

            Console.WriteLine(SuccessMsg);
        }

        private static void ImportVictim(XElement victim, MassDefectContext context, Anomaly anomalyEntity)
        {
            XAttribute name = victim.Attribute("name");

            if (name == null)
            {
                Console.WriteLine(InvalidDataMsg);
                return;
            }

            Person victimEntity = GetVictimByName(name.Value, context);

            if (victimEntity == null)
            {
                Console.WriteLine(InvalidDataMsg);
                return;
            }

            anomalyEntity.Victims.Add(victimEntity);
            context.Anomalies.Add(anomalyEntity);
            context.SaveChanges();
        }

        private static Person GetVictimByName(string value, MassDefectContext context)
        {
            Person victim = context.People.FirstOrDefault(p => p.Name == value);

            return victim;
        }

        private static Planet GetPlanetByName(string homePlanet, MassDefectContext context)
        {
            Planet planet = context.Planets.FirstOrDefault(p => p.Name == homePlanet);

            return planet;
        }
    }
}
