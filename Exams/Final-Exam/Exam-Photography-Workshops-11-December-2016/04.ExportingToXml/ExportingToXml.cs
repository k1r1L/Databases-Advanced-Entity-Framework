namespace _04.ExportingToXml
{
    using System.Linq;
    using System.Xml.Linq;
    using PhotographyWorkshops.Data;
    using PhotographyWorkshops.Models;

    public class ExportingToXml
    {
        public static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            ExportPhotographersWithSameCameras(unit);
            ExportWorkshopsByLocation(unit);
        }

        private static void ExportWorkshopsByLocation(UnitOfWork unit)
        {
            var workshops = unit.Workshops
                .GetAll()
                .GroupBy(workshop => workshop.Location)
                .Select(workshop => workshop.Key);

            XElement root = new XElement("locations");
            foreach (var location in workshops)
            {
                XElement locationEl = new XElement("location");
                locationEl.SetAttributeValue("name", location);
                Workshop workshop = unit.Workshops.GetFirst(w => w.Location == location);
                var participants = unit.Photographers
                    .GetAll(p => p.ParicipantWorkshops.Count(w => w.Location == location) != 0)
                    .Select(p => new
                    {
                        p.FirstName,
                        p.LastName
                    });
                if (participants.Count() < 5)
                {
                    continue;
                }
                decimal totalProfit = participants.Count() * workshop.PricePerParticipant;
                totalProfit -= 0.2m * totalProfit;
                XElement workshopEl = new XElement("workshop");
                workshopEl.SetAttributeValue("name", workshop.Name);
                workshopEl.SetAttributeValue("total-profit", totalProfit);
                XElement participantsRoot = new XElement("participants");
                participantsRoot.SetAttributeValue("count", participants.Count());
                foreach (var participant in participants)
                {
                    XElement participantEl = new XElement("participant");
                    participantEl.SetAttributeValue("first-name", participant.FirstName);
                    participantEl.SetAttributeValue("last-name", participant.LastName);
                    participantsRoot.Add(participantEl);
                }
                workshopEl.Add(participantsRoot);
                locationEl.Add(workshopEl);
                root.Add(locationEl);
            }

            root.Save("../../../results/workshops-by-location.xml");
           
        }

        private static void ExportPhotographersWithSameCameras(UnitOfWork unit)
        {
            var photographers = unit.Photographers
                .GetAll(photographer => photographer.PrimaryCamera.Make == photographer.SecondaryCamera.Make)
                .Select(photographer => new
                {
                    photographer.FirstName,
                    photographer.LastName,
                    primaryCamera = new
                    {
                        make = photographer.PrimaryCamera.Make,
                        model = photographer.PrimaryCamera.Model
                    },
                    lenses = photographer.Lenses.
                    Select(lens => new
                    {
                        lens.Make,
                        lens.FocalLength,
                        lens.MaxAperture
                    })
                });

            XElement root = new XElement("photographers");
            foreach (var photographer in photographers)
            {
                XElement photographerEl = new XElement("photographer");
                photographerEl.SetAttributeValue("name", photographer.FirstName + " " + photographer.LastName);
                photographerEl.SetAttributeValue("primary-camera", photographer.primaryCamera.make + " " + photographer.primaryCamera.model);
                if (photographer.lenses.Count() > 0)
                {
                    XElement lensesRoot = new XElement("lenses");
                    foreach (var lens in photographer.lenses)
                    {
                        XElement lensEl = new XElement("lens");
                        lensEl.SetValue($"{lens.Make} {lens.FocalLength}mm f{lens.MaxAperture}");
                        lensesRoot.Add(lensEl);
                    }

                    photographerEl.Add(lensesRoot);
                }
                root.Add(photographerEl);
            }

            root.Save("../../../results/same-cameras-photographers.xml");

        }
    }
}
