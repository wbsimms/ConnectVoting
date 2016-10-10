using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectVoting.Model
{
	public interface IDataService
	{
		Task<DataItem> GetData();
		List<string> AvailableElections { get; set; }
	}
}