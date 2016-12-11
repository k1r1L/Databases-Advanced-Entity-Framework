namespace _01.ImportingDataFromJson
{
    using AutoMapper;
    using Newtonsoft.Json;
    using PhotographyWorkshops.Data;
    using PhotographyWorkshops.Dtos;
    using PhotographyWorkshops.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class ImportingDataFromJson
    {
        private const string RootDirectory = "../../../datasets";
        private const string LensesJsonLocation = RootDirectory + "/lenses.json";
        private const string CamerasJsonLocation = RootDirectory + "/cameras.json";
        private const string PhotographersJsonLocation = RootDirectory + "/photographers.json";
        private const string ErrorMsg = "Error. Invalid data provided";

        public static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            unit.InitializeDatabase();
            ConfigureMapper(unit);
            ImportLenses(unit);
            ImportCameras(unit);
            ImportPhotographers(unit);
        }

        private static void ImportPhotographers(UnitOfWork unit)
        {
            string photographersJson = File.ReadAllText(PhotographersJsonLocation);
            IEnumerable<PhotographerDto> photographerDtos = JsonConvert
                .DeserializeObject<IEnumerable<PhotographerDto>>(photographersJson);
            List<Camera> camerasInDb = unit.Cameras.GetAll().ToList();
            Random rnd = new Random();
            foreach (PhotographerDto photographerDto in photographerDtos)
            {
                if (photographerDto.FirstName == null || photographerDto.LastName == null)
                {
                    Console.WriteLine(ErrorMsg);
                    continue;
                }

                if (photographerDto.Phone != null && !Regex.IsMatch(photographerDto.Phone, @"\+[\d]{1,3}\/[\d]{8,10}"))
                {
                    Console.WriteLine(ErrorMsg);
                    continue;
                }

                Photographer photographer = new Photographer()
                {
                    FirstName = photographerDto.FirstName,
                    LastName = photographerDto.LastName,
                    PhoneNumber = photographerDto.Phone,
                    PrimaryCamera = camerasInDb[rnd.Next(0, camerasInDb.Count)],
                    SecondaryCamera = camerasInDb[rnd.Next(0, camerasInDb.Count)]
                };


                foreach (int lensId in photographerDto.Lenses)
                {
                    Lens lens = unit.Lenses.Find(lensId);
                    if (lens == null)
                    {
                        continue;
                    }

                    if (lens.Make != photographer.PrimaryCamera.Make || lens.Make != photographer.SecondaryCamera.Make)
                    {
                        continue;
                    }

                    photographer.Lenses.Add(lens);
                }

                try
                {
                    unit.Photographers.Add(photographer);
                    unit.Commit();
                    Console.WriteLine($"Successfully imported {photographer.FirstName}  {photographer.LastName} | Lenses: {photographer.Lenses.Count}");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                    {
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            Console.WriteLine(error.ErrorMessage);
                        }
                    }
                    Console.WriteLine(ErrorMsg);
                }

            }
        }

        private static void ImportCameras(UnitOfWork unit)
        {
            string camerasJson = File.ReadAllText(CamerasJsonLocation);
            IEnumerable<CameraDto> cameraDtos = JsonConvert.DeserializeObject<IEnumerable<CameraDto>>(camerasJson);
            foreach (CameraDto cameraDto in cameraDtos)
            {
                if (cameraDto.MinIso == null || cameraDto.Make == null || cameraDto.Model == null)
                {
                    Console.WriteLine(ErrorMsg);
                    continue;
                }

                switch (cameraDto.Type)
                {
                    case "DSLR":
                        DslrCamera dslrCamera = Mapper.Map<DslrCamera>(cameraDto);
                        unit.Cameras.Add(dslrCamera);                     
                        break;
                    case "Mirrorless":
                        MirrorlessCamera mirrorlessCamera = Mapper.Map<MirrorlessCamera>(cameraDto);
                        unit.Cameras.Add(mirrorlessCamera);
                        break;
                    default:
                        Console.WriteLine(ErrorMsg);
                        break;
                }

                unit.Commit();
                Console.WriteLine($"Successfully imported {cameraDto.Type} {cameraDto.Make}  {cameraDto.Model}");
            }
        }

        private static void ImportLenses(UnitOfWork unit)
        {
            string lensesJson = File.ReadAllText(LensesJsonLocation);
            IEnumerable<LensDto> lensDtos = JsonConvert.DeserializeObject<IEnumerable<LensDto>>(lensesJson);
            foreach (LensDto lensDto in lensDtos)
            {
                Lens lensEntity = Mapper.Map<Lens>(lensDto);

                unit.Lenses.Add(lensEntity);
                Console.WriteLine($"Successfully imported {lensEntity.Make} {lensEntity.FocalLength}mm f{lensEntity.MaxAperture}");
            }

            unit.Commit();
        }

        private static void ConfigureMapper(UnitOfWork unit)
        {
            List<Camera> camerasInDb = unit.Cameras.GetAll().ToList();
            Random rnd = new Random();
            Mapper.Initialize(config =>
            {
                config.CreateMap<LensDto, Lens>();
                config.CreateMap<CameraDto, DslrCamera>();
                config.CreateMap<CameraDto, MirrorlessCamera>();
                config.CreateMap<PhotographerDto, Photographer>();
            });
        }

        private static void ValidateCamera(Camera camera)
        {
            if (camera.Make == null || camera.Model == null || camera.MinIso == 0)
            {
                Console.WriteLine("MinIso is 0");
                throw new ArgumentException(ErrorMsg);
            }
        }
    }
}
