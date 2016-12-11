namespace _03.ExportToJson
{
    using Newtonsoft.Json;
    using PhotographyWorkshops.Data;
    using PhotographyWorkshops.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExportToJson
    {
        private const string RootDirectory = "../../../results";
        private const string OrderedPhotographersJsonExportLocation = RootDirectory + "/photographers-ordered.json";
        private const string LandscapePhotographersJsonExportLocation = RootDirectory + "/landscape-photographers.json";

        public static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            OrderedPhotographersExport(unit);
            LandscapePhotographersExport(unit);
        }

        private static void LandscapePhotographersExport(UnitOfWork unit)
        {
            var photographers = unit
                .Photographers.GetAll(photographer => photographer.PrimaryCamera is DslrCamera 
                && photographer.Lenses.All(lens => lens.FocalLength <= 30) && photographer.Lenses.Count(lens => lens.FocalLength <= 30) != 0)
                .OrderBy(photographer => photographer.FirstName)
                .Select(photographer => new
                {
                    firstName = photographer.FirstName,
                    lastName = photographer.LastName,
                    cameraMake = photographer.PrimaryCamera.Make,
                    lensesCount = photographer.Lenses.Count
                });

            SerializeAndExport(photographers, LandscapePhotographersJsonExportLocation);
        }

        private static void OrderedPhotographersExport(UnitOfWork unit)
        {
            var photographers = unit.Photographers
                .GetAll()
                .OrderBy(photographer => photographer.FirstName)
                .ThenByDescending(photographer => photographer.LastName)
                .Select(photographer => new
                {
                    firstName = photographer.FirstName,
                    lastName = photographer.LastName,
                    phone = photographer.PhoneNumber
                });

            SerializeAndExport(photographers, OrderedPhotographersJsonExportLocation);
        }

        private static void SerializeAndExport<T>(T entities, string path)
        {
            string json = JsonConvert.SerializeObject(entities, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}
