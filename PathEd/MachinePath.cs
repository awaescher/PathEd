using System;

namespace PathEd
{
	public class MachinePath : IPath
	{
		public string Get()
		{
			return Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
		}

		public void Set(string value)
		{
			Environment.SetEnvironmentVariable("PATH", value, EnvironmentVariableTarget.Machine);
		}
	}
}