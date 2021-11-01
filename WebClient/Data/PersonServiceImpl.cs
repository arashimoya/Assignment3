using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileData;
using Models;

namespace Assignment1.Data
{
    public class PersonServiceImpl : IPersonService
    {
        private FileContext FileContext;

        public PersonServiceImpl()
        {
            FileContext = new FileContext();
        }

        public async Task<IList<Adult>> GetAllAsync()
        {
            return FileContext.Adults;
        }
        
        public async Task AddPersonAsync(Adult adult)
        {
            int max = FileContext.Adults.Max(adult => adult.Id);
            adult.Id = (++max);
            FileContext.Adults.Add(adult);
            FileContext.SaveChanges();
        }

        public async Task UpdatePersonAsync(Adult adult)
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

        public async Task RemovePersonAsync(int AdultId)
        {
            Adult toRemove = FileContext.Adults.First(a => a.Id == AdultId);
            FileContext.Adults.Remove(toRemove);
            FileContext.SaveChanges();
        }

        

        public async Task<Adult> GetAsync(int id)
        {
            return FileContext.Adults.FirstOrDefault(a => a.Id == id);
        }
    }
}