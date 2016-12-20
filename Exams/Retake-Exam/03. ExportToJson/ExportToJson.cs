namespace _03.ExportToJson
{
    using System;
    using System.IO;
    using System.Linq;
    using WeddingsPlanner.Data;
    using WeddingsPlanner.Utilities;
    using Newtonsoft.Json;

    public class ExportToJson
    {
        public static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            OrderedAgencies(unit);
            GuestLists(unit);
        }

        private static void GuestLists(UnitOfWork unit)
        {
            var guests = unit.Weddings
                .GetAll()
                .Select(wedding => new
                {
                    bride = wedding.Bride.FullName,
                    bridegroom = wedding.Bridegroom.FullName,
                    agency = new
                    {
                        name = wedding.Agency.Name,
                        town = wedding.Agency.Town
                    },
                    invitedGuests = wedding.Invitations.Count,
                    brideGuests = wedding.Invitations.Count(i => i.Family == "Bride"),
                    brideGroomGuests = wedding.Invitations.Count(i => i.Family == "Bridegroom"),
                    attendingGuests = wedding.Invitations.Count(i => i.IsAttending),
                    guests =  wedding.Invitations.Select(i => new
                    {
                        i.Guest.FullName
                    })
                })
                .OrderByDescending(wedding => wedding.invitedGuests)
                .ThenBy(wedding => wedding.attendingGuests);

            SerializeAndExport(guests, Constants.GuestsExportLocation);
        }

        private static void OrderedAgencies(UnitOfWork unit)
        {
            var agencies = unit.Agencies
                .GetAll()
                .OrderByDescending(agency => agency.EmployeesCount)
                .ThenBy(agency => agency.Name)
                .Select(agency => new
                {
                    agency.Name,
                    agency.EmployeesCount,
                    agency.Town
                });

            SerializeAndExport(agencies, Constants.OrderedAgenciesExportLocation);
        }

        private static void SerializeAndExport<T>(T entities, string path)
        {
            string json = JsonConvert.SerializeObject(entities, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}
