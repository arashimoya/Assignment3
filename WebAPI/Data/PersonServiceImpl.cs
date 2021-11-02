using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FileData;
using Models;

namespace WebAPI.Data
{
    public class PersonServiceImpl : IPersonService
    {
        private FileContext FileContext;
        private string adultsFile = "adults.json";
        private IList<Adult> adults;

        public PersonServiceImpl()
        {
            if (!File.Exists(adultsFile))
            {
                Seed();
                WriteAdultsToFile();
            }
            string content = File.ReadAllText(adultsFile);
            adults = JsonSerializer.Deserialize<IList<Adult>>(content);
        }

        private void Seed()
        {
            Adult[] adultArray =
            {
                new Adult
                {
                    Id = 1,
                    FirstName = "Adam",
                    LastName = "Ara",
                    Age = 20,
                    EyeColor = "Blue",
                    HairColor = "Blonde",
                    Height = 188,
                    Sex = "M",
                    Weight = 70,
                    JobTitle = new Job
                    {
                        JobTitle = "Junior Developer",
                        Salary = 20000
                    },
                },
                new Adult
                {
                    Id = 2,
                    FirstName = "Tomek",
                    LastName = "Maj",
                    Age = 21,
                    EyeColor = "Bronze",
                    HairColor = "Black",
                    Height = 175,
                    Sex = "M",
                    Weight = 70,
                    JobTitle = new Job()
                    {
                        JobTitle = "IT support",
                        Salary = 20000,
                    },
                },

            };
            adults = adultArray.ToList();

        }

        private void WriteAdultsToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(adults, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(adultsFile, productsAsJson);
        }

        public async Task<IList<Adult>> GetAllAsync()
        {
            List<Adult> tmp = new List<Adult>(adults);
            return tmp;
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            int max = adults.Max(adult => adult.Id);
            adult.Id = (++max);
            adults.Add(adult);
            WriteAdultsToFile();
            return adult;
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            Adult toRemove = adults.FirstOrDefault(a => a.Id == adultId);
            adults.Remove(toRemove);
            WriteAdultsToFile();
        }

        public async Task<Adult> UpdateAsync(Adult adult)
        {
            Adult toUpdate = adults.FirstOrDefault(a => a.Id == adult.Id);
            if (toUpdate == null) throw new Exception($"Did not find an adult with this ID");
            toUpdate.FirstName = adult.FirstName;
            toUpdate.LastName = adult.LastName;
            toUpdate.HairColor = adult.HairColor;
            toUpdate.EyeColor = adult.EyeColor;
            toUpdate.Age = adult.Age;
            toUpdate.Weight = adult.Weight;
            toUpdate.Height = adult.Height;
            toUpdate.Sex = adult.Sex;
            toUpdate.JobTitle.Salary = adult.JobTitle.Salary;
            toUpdate.JobTitle.JobTitle = adult.JobTitle.JobTitle;
            WriteAdultsToFile();
            return toUpdate;
        }
    }
}