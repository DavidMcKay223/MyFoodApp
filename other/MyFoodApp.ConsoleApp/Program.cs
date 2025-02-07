using Microsoft.EntityFrameworkCore;
using MyFoodApp.ConsoleApp.Utility;
using MyFoodApp.Domain.Entities;
using MyFoodApp.Infrastructure.Persistence;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.NetworkInformation;

namespace MyFoodApp.ConsoleApp
{
    public class Program
    {
        public static void Main()
        {
            string projectRoot = FileUtility.GetProjectRoot();
            string jsonFilePath = Path.Combine(projectRoot, "Data\\seed_data.json");

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyFoodApp;Trusted_Connection=True;")
                .Options;

            DataSeeder dataSeeder = new DataSeeder(jsonFilePath, options);

            //dataSeeder.ResetDatabase();
            //dataSeeder.SeedDatabaseFromJson();

            //dataSeeder.SaveDatabaseToJson();
        }
    }
}
