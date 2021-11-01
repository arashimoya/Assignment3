using System.Collections.Generic;
using System.Threading.Tasks;
using FileData;
using Models;

namespace Assignment1.Data
{
    public interface IPersonService
    {
        Task AddPersonAsync(Adult adult);
        Task UpdatePersonAsync(Adult adult);
        Task RemovePersonAsync(int AdultId);
        
        Task<Adult> GetAsync(int id);
        Task<IList<Adult>> GetAllAsync();
    }
}