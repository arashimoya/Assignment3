using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace WebAPI.Data
{
    public interface IPersonService
    {
        void AddPerson(Adult adult);
        void EditPerson(Adult adult);
        void RemovePerson(int AdultId);
        
        Adult Get(int id);
        Task<IList<Adult>> GetAllAsync();
    }
}