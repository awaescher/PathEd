using System;
using System.Text.RegularExpressions;

namespace PathEd
{
	public class PathUpdater
	{
		public PathUpdater(IPath path)
		{
			Path = path ?? throw new ArgumentNullException(nameof(path));
		}

		public void Add(string value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var pathVariables = Path.Get();

			if (string.IsNullOrEmpty(pathVariables))
			{
				pathVariables = value;
			}
			else
			{
				if (pathVariables.IndexOf(value, StringComparison.OrdinalIgnoreCase) > -1)
					return;

				pathVariables = pathVariables.TrimEnd(';');
				pathVariables = pathVariables + ";" + value;
			}

			Path.Set(pathVariables);
		}

		public void Remove(string value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			var pathVariables = Path.Get();

			if (string.IsNullOrEmpty(pathVariables))
				return;

			if (pathVariables.IndexOf(value, StringComparison.OrdinalIgnoreCase) == -1)
				return;

			pathVariables = Regex.Replace(pathVariables, $"\"?{Regex.Escape(value)}\"?;?", "", RegexOptions.IgnoreCase);

			if (pathVariables.EndsWith(";"))
				pathVariables = pathVariables.Substring(0, pathVariables.Length - 1);

			Path.Set(pathVariables);
		}

		public IPath Path { get; }
	}
}