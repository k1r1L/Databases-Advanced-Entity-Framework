namespace _01.ImportDataFromJson
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using AutoMapper;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using WeddingsPlanner.Data;
    using WeddingsPlanner.Dtos;
    using WeddingsPlanner.Models;
    using WeddingsPlanner.Utilities;

    public class ImportDataFromJson
    {
        public static void Main(string[] args)
        {
            UnitOfWork unit = new UnitOfWork();
            unit.InitializeDatabase();
            ConfigureAutomapper(unit);
            ImportAgencies(unit);
            ImportPeople(unit);
            ImportWeddingsAndInvitations(unit);
        }

        private static void ImportWeddingsAndInvitations(UnitOfWork unit)
        {
            string weddingsJson = File.ReadAllText(Constants.WeddingsImportLocation);

            IEnumerable<WeddingDto> weddingDtos = JsonConvert.DeserializeObject<IEnumerable<WeddingDto>>(weddingsJson);
            foreach (WeddingDto weddingDto in weddingDtos)
            {
                if (weddingDto.Agency == null || weddingDto.Bride == null
                    || weddingDto.Bridegroom == null || weddingDto.Date == null)
                {
                    Console.WriteLine(Constants.InvalidDataErrorMsg);
                    continue;
                }

                Wedding weddingEntity = Mapper.Map<Wedding>(weddingDto);
                unit.Weddings.Add(weddingEntity);
                unit.Commit();
                Console.WriteLine($"Successfully imported wedding of {weddingEntity.Bride.FirstName} and {weddingEntity.Bridegroom.FirstName}");
                if (weddingDto.Guests != null)
                {
                    foreach (GuestDto guestDto in weddingDto.Guests)
                    {
                        Person guest = GetPersonByFullName(guestDto.Name, unit);
                        if (guest == null)
                        {
                            continue;
                        }

                        try
                        {
                            weddingEntity.Invitations.Add(new Invitation()
                            {
                                Wedding = weddingEntity,
                                Guest = guest,
                                IsAttending = guestDto.Rsvp,
                                Family = guestDto.Family
                            });
                            unit.Commit();

                        }
                        catch (Exception)
                        {
                            Console.WriteLine(Constants.InvalidDataErrorMsg);
                        }
                    }
                }
            }
        }

        private static Person GetPersonByFullName(string fullName, UnitOfWork unit)
        {
            string[] nameTokens = fullName.Split();
            string firstName = string.Empty;
            string middle = string.Empty;
            string lastName = string.Empty;
            Person person = null;
            if (nameTokens.Length == 2)
            {
                firstName = nameTokens[0];
                lastName = nameTokens[1];
                person = unit.People.GetFirstOrDefault
                (p => p.FirstName == firstName && p.LastName == lastName);

            }
            else
            {
                firstName = nameTokens[0];
                middle = nameTokens[1];
                lastName = nameTokens[2];
                person = unit.People.GetFirstOrDefault
                (p => p.FirstName == firstName && p.MiddleNameInitial == middle && p.LastName == lastName);
            }


            return person;
        }

        private static void ImportPeople(UnitOfWork unit)
        {
            string peopleJson = File.ReadAllText(Constants.PeopleImportLocation);

            IEnumerable<PersonDto> dtos = JsonConvert.DeserializeObject<IEnumerable<PersonDto>>(peopleJson,
                new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            foreach (var dto in dtos)
            {
                if (dto.FirstName == null || dto.LastName == null || dto.MiddleInitial == null || dto.Gender == null)
                {
                    Console.WriteLine(Constants.InvalidDataErrorMsg);
                    continue;
                }

                Person personEntity = Mapper.Map<Person>(dto);
                try
                {
                    unit.People.Add(personEntity);
                    unit.Commit();
                    Console.WriteLine($"Successfully imported {personEntity.FullName}");
                }
                catch (Exception)
                {
                    unit.People.Remove(personEntity);
                    Console.WriteLine(Constants.InvalidDataErrorMsg);
                }

            }
        }

        private static void ImportAgencies(UnitOfWork unit)
        {
            string agenciesJson = File.ReadAllText(Constants.AgenciesImportLocation);

            IEnumerable<AgencyDto> agencyDtos = JsonConvert.DeserializeObject<IEnumerable<AgencyDto>>(agenciesJson);
            foreach (AgencyDto agencyDto in agencyDtos)
            {
                Agency agencyEntity = Mapper.Map<Agency>(agencyDto);
                unit.Agencies.Add(agencyEntity);
                Console.WriteLine($"Successfully imported {agencyEntity.Name}");
            }

            unit.Commit();
        }

        private static void ConfigureAutomapper(UnitOfWork unit)
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<AgencyDto, Agency>();
                config.CreateMap<PersonDto, Person>()
                .ForMember(person => person.MiddleNameInitial, expression =>
                expression.MapFrom(dto => dto.MiddleInitial))
                .ForMember(person => person.Birthdate, expression =>
                expression.MapFrom(dto => dto.Birthday));
                config.CreateMap<WeddingDto, Wedding>()
                   .ForMember(wedding => wedding.Bride, expression =>
                expression.MapFrom(dto => unit.People.GetFirst(p => dto.Bride.StartsWith(p.FirstName))))
                  .ForMember(wedding => wedding.Bridegroom, expression =>
                expression.MapFrom(dto => unit.People.GetFirst(p => dto.Bridegroom.StartsWith(p.FirstName))))
                  .ForMember(wedding => wedding.Agency, expression =>
                 expression.MapFrom(dto => unit.Agencies.GetFirst(a => a.Name == dto.Agency)));
            });
        }
    }
}
