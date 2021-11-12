using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ICastService
    {
        Task<CastResponseModel> GetCastDetails(int id);
    }
}