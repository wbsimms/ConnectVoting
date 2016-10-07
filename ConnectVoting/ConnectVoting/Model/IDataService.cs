using System.Threading.Tasks;

namespace ConnectVoting.Model
{
	public interface IDataService
	{
		Task<DataItem> GetData();
	}
}