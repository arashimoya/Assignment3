using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FileData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            return await assDbContext.Adults.ToListAsync();
        }

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            EntityEntry<Adult> newlyAdded = await assDbContext.Adults.AddAsync(adult);
            await assDbContext.SaveChangesAsync();
            return newlyAdded.Entity;
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
            try
            {
                Adult toUpdate = adults.FirstOrDefault(a => a.PersonId == adult.PersonId);
                if (toUpdate == null) 
                    throw new Exception($"Did not find an adult with this ID");
               
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
            catch (Exception e)
            {
                throw new Exception($"Did not find todo with id{adult.PersonId}");
            }
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
                    FirstName = "Dan",
                    LastName = "Pintea",
                    Age = 23,
                    EyeColor = "Hazel",
                    HairColor = "Brown",
                    Height = 180,
                    Sex = "M",
                    Weight = 66,
                    JobTitle = new Job()
                    {
                        JobTitle = "Junior Web Developer",
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