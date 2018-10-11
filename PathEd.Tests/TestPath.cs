namespace PathEd.Tests
{
	public class TestPath : IPath
	{
		private string _path = "";

		public string Get() => _path;

		public void Set(string value) => _path = value;
	}
}