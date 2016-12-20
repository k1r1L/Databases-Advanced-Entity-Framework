namespace _04.ExportToXml
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using WeddingsPlanner.Data;
    using WeddingsPlanner.Models.Presents;
    using WeddingsPlanner.Utilities;

    public class ExportToXml
    {
        public static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            ExportSofiaVenues(unit);
            ExportAgenciesByTown(unit);
        }

        private static void ExportAgenciesByTown(UnitOfWork unit)
        {
            var agenciesByTown = unit.Agencies.GetAll()
                .GroupBy(agency => agency.Town, a => a, (town, agency) => new
                {
                    Town = town,
                    Agencies = agency.Where(a => a.Weddings.Count >= 2)
                })
                .Where(aByTown => aByTown.Town.Length >= 6)
                .Select(aByTown => new
                {
                    Town = aByTown.Town,
                    Agencies = aByTown.Agencies.Select(agency => new
                    {
                        Name = agency.Name,
                        Weddings = agency.Weddings.Select(wedding => new
                        {
                            BrideFullName = wedding.Bride.FullName,
                            BridegroomFullName = wedding.Bridegroom.FullName,
                            Guests = wedding.Invitations.Select(invitation => new
                            {
                                FullName = invitation.Guest.FullName,
                                Family = invitation.Family
                            }),
                            Cash = wedding.Invitations.Where(inv => inv.Present is Cash)
                            .Sum(inv => ((Cash)inv.Present).Amount),
                            PresentsCount = wedding.Invitations.Where(inv => inv.Present is Gift)
                            .Count(inv => ((Gift)inv.Present).Size != "NotSpecified")
                        }),
                    })
                });

            XElement xmlRoot = new XElement("towns");
            foreach (var aByTown in agenciesByTown)
            {
                XElement townNode = new XElement("town");
                townNode.SetAttributeValue("name", aByTown.Town);
                XElement agenciesRoot = new XElement("agencies");
                foreach (var agency in aByTown.Agencies)
                {
                    XElement agencyNode = new XElement("agency");
                    agencyNode.SetAttributeValue("name", agency.Name);
                    decimal totalProfit = agency.Weddings.Sum(a => a.Cash) * 0.2m;
                    agencyNode.SetAttributeValue("profit", totalProfit);
                    foreach (var wedding in agency.Weddings)
                    {
                        XElement weddingNode = new XElement("wedding");
                        weddingNode.SetAttributeValue("cash", wedding.Cash);
                        weddingNode.SetAttributeValue("presents", wedding.PresentsCount);
                        weddingNode.Add(new XElement("bride", wedding.BrideFullName));
                        weddingNode.Add(new XElement("bridegroom", wedding.BridegroomFullName));
                        XElement guestsRoot = new XElement("guests");
                        foreach (var guest in wedding.Guests)
                        {
                            XElement guestNode = new XElement("guest");
                            guestNode.SetAttributeValue("family", guest.Family);
                            guestNode.SetValue(guest.FullName);
                            guestsRoot.Add(guestNode);
                        }

                        weddingNode.Add(guestsRoot);
                        agencyNode.Add(weddingNode);
                    }
                    agenciesRoot.Add(agencyNode);
                }

                townNode.Add(agenciesRoot);
                xmlRoot.Add(townNode);
            }

            xmlRoot.Save(Constants.AgenciesByTownExportLocation);
        }

        private static void ExportSofiaVenues(UnitOfWork unit)
        {
            var venues = unit.Venues
                .GetAll(venue => venue.Town == "Sofia" && venue.Weddings.Count >= 3)
                .OrderBy(venue => venue.Capacity)
                .Select(venue => new
                {
                    venue.Name,
                    venue.Capacity,
                    WeddingsCount = venue.Weddings.Count
                });

            XElement root = new XElement("venues");
            root.SetAttributeValue("town", "Sofia");
            foreach (var venue in venues)
            {
                XElement venueElement = new XElement("venue");
                venueElement.SetAttributeValue("name", venue.Name);
                venueElement.SetAttributeValue("capacity", venue.Capacity);
                venueElement.Add(new XElement("weddings-count", venue.WeddingsCount));
                root.Add(venueElement);
            }

            root.Save(Constants.SofiaVenuesExportLocation);
        }
    }
}
