using System.Threading.Tasks;

namespace WhoIsUtility.Core
{
    public interface IWhoIs
    {
        Task<string> GetHostInfo(string hostName);
    }
}