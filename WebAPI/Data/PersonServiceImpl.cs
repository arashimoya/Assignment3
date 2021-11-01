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
        
        public void AddPerson(Adult adult)
        {
            int max = FileContext.Adults.Max(adult => adult.Id);
            adult.Id = (++max);
            FileContext.Adults.Add(adult);
            FileContext.SaveChanges();
        }

        public void EditPerson(Adult adult)
        {
            Adult toUpdate = FileContext.Adults.First(a => a.Id == adult.Id);
            toUpdate.FirstName = adult.FirstName;
            toUpdate.LastName = adult.LastName;
            toUpdate.HairColor = adult.HairColor;
            toUpdate.EyeColor = adult.EyeColor;
            toUpdate.Age = adult.Age;
            toUpdate.Weight = adult.Weight;
            toUpdate.Height = adult.Height;
            toUpdate.Sex = adult.Sex;
            FileContext.SaveChanges();
        }

        public void RemovePerson(int AdultId)
        {
            Adult toRemove = FileContext.Adults.First(a => a.Id == AdultId);
            FileContext.Adults.Remove(toRemove);
            FileContext.SaveChanges();
        }

        

        public Adult Get(int id)
        {
            return FileContext.Adults.FirstOrDefault(a => a.Id == id);
        }
    }
}