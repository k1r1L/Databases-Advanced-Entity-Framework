namespace _04.ExportingToXml
{
    using MassDefect.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExportingToXml
    {
        public static void Main(string[] args)
        {
            MassDefectContext context = new MassDefectContext();
            var exportedAnomalies = context.Anomalies
                .OrderBy(a => a.Id)
                .Select(a => new
                {
                    id = a.Id,
                    originPlanetName = a.OriginPlanet.Name,
                    teleportPlanetName = a.TeleportPlanet.Name,
                    victims = a.Victims.Select(v => v.Name).ToList()
                });

            XElement xmlDoc = new XElement("anomalies");

            foreach (var exportedAnomaly in exportedAnomalies)
            {
                XElement anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", exportedAnomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", exportedAnomaly.originPlanetName));
                anomalyNode.Add(new XAttribute("teleported-planet", exportedAnomaly.teleportPlanetName));

                XElement victimsNode = new XElement("victims");
                foreach (string victim in exportedAnomaly.victims)
                {
                    XElement victimNode = new XElement("victims");
                    victimNode.Add(new XAttribute("name", victim));
                    victimsNode.Add(victimNode);
                }

                anomalyNode.Add(victimsNode);
                xmlDoc.Add(anomalyNode);
            }

            xmlDoc.Save("../../anomalies.xml");
        }
    }
}
