namespace _05.HospitalDatabase
{
    using System;
    using _05.HospitalDatabase.Models;
    using System.Linq;
    public class Startup
    {
        private static HospitalContext db = new HospitalContext();
        public static void Main()
        {
            Doctor doctor = new Doctor()
            {
                Name = "Doc Brown",
            };
            db.SaveChanges();
            //Doctor doctor = null;
            //do
            //{
            //    try
            //    {c
            //        Console.WriteLine("Enter email address:");
            //        string email = Console.ReadLine();
            //        Console.WriteLine("Enter password:");
            //        string password = Console.ReadLine();
            //        doctor = GetDoctor(email, password);
            //    }
            //    catch (ArgumentException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }

            //} while (doctor == null);

            //bool logOff = false;
            //do
            //{
            //    Console.WriteLine("Choose command: (Patients/Visitations/LogOff)");
            //    string command = Console.ReadLine();

            //    switch (command)
            //    {
            //        case "Patients":
            //            PrintPatients(doctor);
            //            break;
            //        case "Visitations":
            //            PrintVisitations(doctor);
            //            break;
            //        case "LogOff":
            //            logOff = true;
            //            break;
            //        default:
            //            Console.WriteLine("Invalid command!");
            //            break;
            //    }
            //} while (!logOff);

        }

        private static void PrintVisitations(Doctor doctor)
        {
            foreach (var visitation in db.Visitations.Where(v => v.Doctor.DoctorId == doctor.DoctorId))
            {
                Console.WriteLine("Visitation time: ", visitation.Time);
                Console.WriteLine("Visitation comment: ", visitation.Comments);
                Console.WriteLine("Patient full name: ", visitation.Patient.FullName);
                Console.WriteLine("----------------------------------------------");
            }
        }

        private static void PrintPatients(Doctor doctor)
        {
            foreach (var visitation in db.Visitations.Where(v => v.Doctor.DoctorId == doctor.DoctorId))
            {
                Console.WriteLine("Patient name: ", visitation.Patient.FullName);
                Console.WriteLine("----------------------------------------------");
            }
        }

        private static Doctor GetDoctor(string email, string password)
        {
            Doctor doctor = db.Doctors.FirstOrDefault(d => d.Email == email && d.Password == password);

            if (doctor == null)
            {
                throw new ArgumentException("Invalid credentials!");
            }

            return doctor;
        }
    }
}
