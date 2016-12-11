namespace _02.ImportingDataFromXml
{
    using PhotographyWorkshops.Data;
    using PhotographyWorkshops.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class ImportingDataFromXml
    {
        private const string RootDirectory = "../../../datasets";
        private const string AccessoriesXmlDirectory = RootDirectory + "/accessories.xml";
        private const string WorkshopsXmlDirectory = RootDirectory + "/workshops.xml";
        private const string ErrorMsg = "Error. Invalid data provided";

        public static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            ImportAccessories(unit);
            ImportWorkshops(unit);
        }

        private static void ImportWorkshops(UnitOfWork unit)
        {
            XDocument doc = XDocument.Load(WorkshopsXmlDirectory);

            XElement root = doc.Root;
            List<Photographer> photographers = unit.Photographers.GetAll().ToList();
            foreach (XElement workshopElement in root.Elements())
            {
                if (workshopElement.Attribute("name") == null || workshopElement.Attribute("location") == null 
                    || workshopElement.Attribute("price") == null || workshopElement.Element("trainer") == null)
                {
                    Console.WriteLine(ErrorMsg);
                    continue;
                }

                string name = workshopElement.Attribute("name").Value;
                string location = workshopElement.Attribute("location").Value;
                decimal price = decimal.Parse(workshopElement.Attribute("price").Value);
                string trainerFullName = workshopElement.Element("trainer").Value;
                string trainerFirstName = trainerFullName.Split()[0];
                string trainerLastName = trainerFullName.Split()[1];
                DateTime? startDate = null;
                DateTime? endDate = null;
                Photographer trainer = GetPhotographerByBothNames(trainerFirstName, trainerLastName, photographers);

                if (workshopElement.Attribute("start-date") != null)
                {
                    startDate = DateTime.Parse(workshopElement.Attribute("start-date").Value);
                }

                if (workshopElement.Attribute("end-date") != null)
                {
                    endDate = DateTime.Parse(workshopElement.Attribute("end-date").Value);
                }

                Workshop workshop = new Workshop()
                {
                    Name = name,
                    Location = location,
                    PricePerParticipant = price,
                    Trainer = trainer,
                    StartDate = startDate,
                    EndDate = endDate
                };

                unit.Workshops.Add(workshop);
                unit.Commit();
                Console.WriteLine($"Successfully imported {name}");

                XElement participantsRoot = workshopElement.Element("participants");
                if (participantsRoot != null)
                {
                    foreach (XElement participantElement in participantsRoot.Elements())
                    {
                        string participantFirstName = participantElement.Attribute("first-name").Value;
                        string participantLastName = participantElement.Attribute("last-name").Value;
                        Photographer participant = GetPhotographerByBothNames(participantFirstName, participantLastName, photographers);
                        workshop.Participants.Add(participant);
                    }
                }

                unit.Commit();
            }
        }

        private static void ImportAccessories(UnitOfWork unit)
        {
            XDocument doc = XDocument.Load(AccessoriesXmlDirectory);

            XElement root = doc.Root;
            List<Photographer> owners = unit.Photographers.GetAll().ToList();
            Random rnd = new Random();
            foreach (XElement accessoryElement in root.Elements())
            {
                string name = accessoryElement.Attribute("name").Value;
                Photographer owner = owners[rnd.Next(0, owners.Count)];
                unit.Accessories.Add(new Accessory()
                {
                    Name = name,
                    Owner = owner
                });

                Console.WriteLine($"Successfully imported {name}");
            }

            unit.Commit();
        }

        private static Photographer GetPhotographerByBothNames(string firstName, string secondName, List<Photographer> photographers)
        {
            Photographer photographer = photographers.First(ph => ph.FirstName == firstName && ph.LastName == secondName);

            return photographer;
        }
    }
}
