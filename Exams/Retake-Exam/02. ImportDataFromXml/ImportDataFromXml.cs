namespace _02.ImportDataFromXml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using WeddingsPlanner.Data;
    using WeddingsPlanner.Models;
    using WeddingsPlanner.Models.Presents;
    using WeddingsPlanner.Utilities;

    public class ImportDataFromXml
    {
        public static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            ImportVenues(unit);
            ImportIntoWeddingVenues(unit);
            ImportPresents(unit);
        }

        private static void ImportPresents(UnitOfWork unit)
        {
            XDocument doc = XDocument.Load(Constants.PresentsImportLocation);
            XElement presentsRoot = doc.Root;

            foreach (XElement presentElement in presentsRoot.Elements())
            {
                XAttribute presentTypeAttr = presentElement.Attribute("type");
                XAttribute invitationIdAttr = presentElement.Attribute("invitation-id");

                if (presentTypeAttr == null || invitationIdAttr == null)
                {
                    Console.WriteLine(Constants.InvalidDataErrorMsg);
                    continue;
                }

                Invitation invitation = GetInvitationById(int.Parse(invitationIdAttr.Value), unit);

                if (invitation == null)
                {
                    Console.WriteLine("No such invitation in database");
                    continue;
                }

                switch (presentTypeAttr.Value)
                {
                    case "cash":
                        ImportCashPresent(unit, presentElement, invitation);
                        break;
                    case "gift":
                        ImportGiftPresent(unit, presentElement, invitation);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ImportIntoWeddingVenues(UnitOfWork unit)
        {
            List<Venue> venues = unit.Venues.GetAll().ToList();
            List<Wedding> weddings = unit.Weddings.GetAll().ToList();
            Random rnd = new Random();
            foreach (Wedding wedding in weddings)
            {
                Venue firstVenue = venues[rnd.Next(0, weddings.Count)];
                Venue secondVenue = venues[rnd.Next(0, weddings.Count)];

                wedding.Venues.Add(firstVenue);
                wedding.Venues.Add(secondVenue);
                unit.Commit();
            }
        }

        private static void ImportVenues(UnitOfWork unit)
        {
            List<Wedding> weddings = unit.Weddings.GetAll().ToList();
            Random rnd = new Random();
            XDocument doc = XDocument.Load(Constants.VenuesImportLocation);
            XElement root = doc.Root;

            foreach (XElement venueElement in root.Elements())
            {
                string name = venueElement.Attribute("name").Value;
                int capacity = int.Parse(venueElement.Element("capacity").Value);
                string town = venueElement.Element("town").Value;

                Venue venueEntity = new Venue()
                {
                    Name = name,
                    Capacity = capacity,
                    Town = town
                };
                unit.Venues.Add(venueEntity);
                unit.Commit();
                Console.WriteLine($"Successfully imported {name}");

            }
        }

        private static void ImportCashPresent(UnitOfWork unit, XElement presentElement, Invitation invitation)
        {
            XAttribute amountAttr = presentElement.Attribute("amount");
            if (amountAttr == null)
            {
                Console.WriteLine(Constants.InvalidDataErrorMsg);
                return;
            }

            int amount = int.Parse(amountAttr.Value);
            Cash cashEntity = null;
            try
            {
                cashEntity = new Cash()
                {
                    Amount = amount,
                    Owner = invitation.Guest,
                    Invitation = invitation
                };

                unit.Presents.Add(cashEntity);
                unit.Commit();
                Console.WriteLine($"Succesfully imported present from {cashEntity.Owner.FullName}");
            }
            catch (Exception)
            {
                unit.Presents.Remove(cashEntity);
                Console.WriteLine(Constants.InvalidDataErrorMsg);
            }
        }

        private static void ImportGiftPresent(UnitOfWork unit, XElement presentElement, Invitation invitation)
        {
            XAttribute nameAttr = presentElement.Attribute("present-name");
            if (nameAttr == null)
            {
                Console.WriteLine(Constants.InvalidDataErrorMsg);
                return;
            }

            string presentName = nameAttr.Value;
            XAttribute sizeAttr = presentElement.Attribute("size");
            string size = "";
            if (sizeAttr == null)
            {
                size = "NotSpecified";
            }
            else
            {
                size = sizeAttr.Value;
            }

            Gift giftEntity = null;
            try
            {
                giftEntity = new Gift()
                {
                    Name = presentName,
                    Invitation = invitation,
                    Owner = invitation.Guest,
                    Size = size
                };
                unit.Presents.Add(giftEntity);
                unit.Commit();
                Console.WriteLine($"Succesfully imported present from {giftEntity.Owner.FullName}");

            }
            catch (Exception)
            {
                unit.Presents.Remove(giftEntity);
                Console.WriteLine(Constants.InvalidDataErrorMsg);
            }

        }

        private static Invitation GetInvitationById(int invitationId, UnitOfWork unit)
        {
            Invitation invitation = unit.Invitations.GetFirstOrDefault(inv => inv.Id == invitationId);

            return invitation;
        }
    }
}
