using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace WebAPI.Data
{
    public interface IPersonService
    {
        Task<IList<Adult>> GetAllAsync();
        Task<Adult> AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int adultId);
        Task<Adult> UpdateAsync(Adult adult);
    }
}