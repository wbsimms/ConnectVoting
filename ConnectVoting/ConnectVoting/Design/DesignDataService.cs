﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectVoting.Model;

namespace ConnectVoting.Design
{
	public class DesignDataService : IDataService
	{
		public Task<DataItem> GetData()
		{
			// Use this to create design time data

			var item = new DataItem("Welcome to MVVM Light [design]");
			return Task.FromResult(item);
		}

		public List<string> AvailableElections { get; set; } = new List<string>() {"One","Two", "Three" };
	}
}