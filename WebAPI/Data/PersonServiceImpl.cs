using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FileData;
using Microsoft.EntityFrameworkCore;
using Models;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class PersonServiceImpl : IPersonService
    {
        private AssDbContext assDbContext;
        private IList<Adult> adults;

        public PersonServiceImpl()
        {
            assDbContext = new AssDbContext();
            adults = assDbContext.Adults.ToList();
            if (!adults.Any())
            {
                Seed();
                WriteAdultsToDb();
            }
        }

        

        public async Task<IList<Adult>> GetAllAsync()
        {
            List<Adult> tmp = new List<Adult>(adults);
            return tmp;
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            int max = adults.Max(adult => adult.PersonId);
            adult.PersonId = (++max);
            adult.JobTitle.JobId = adult.PersonId;
            adults.Add(adult);
            assDbContext.Adults.Add(adult);
            await assDbContext.SaveChangesAsync();
            return adult;
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            Adult toRemove = adults.FirstOrDefault(a => a.PersonId == adultId);
            adults.Remove(toRemove);
            if (toRemove != null)
            {
                assDbContext.Remove(toRemove);
                await assDbContext.SaveChangesAsync();
            }
            
        }

        public async Task<Adult> UpdateAsync(Adult adult)
        {
            Adult toUpdate = adults.FirstOrDefault(a => a.PersonId == adult.PersonId);
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
            assDbContext.Adults.Update(toUpdate);
            await assDbContext.SaveChangesAsync();
            return toUpdate;
        }
        private void Seed()
        {
            Adult[] adultArray =
            {
                new Adult
                {
                    PersonId = 1,
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
                    PersonId = 2,
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

        private void WriteAdultsToDb()
        {
            foreach (var adult in adults)
            {
                assDbContext.Adults.Add(adult);
            }

            assDbContext.SaveChanges();
        }
    }
}