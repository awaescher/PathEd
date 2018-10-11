using System;
using System.IO;
using System.Windows.Forms;

namespace PathEd
{
	class Program
	{
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern bool SetProcessDPIAware();

		static void Main(string[] args)
		{
			var hasValidArguments = args?.Length == 2 && (args[0] == "add" || args[0] == "remove");

			if (!hasValidArguments)
			{
				ShowHowToUse();
				return;
			}

			var updater = new PathUpdater(new MachinePath());

			try
			{
				if (args[0] == "add")
					updater.Add(args[1]);
				else
					updater.Remove(args[1]);
			}
			catch
			{
				System.Environment.Exit(1); // NO SUCCESS
			}

			
		}

		private static void ShowHowToUse()
		{
			InitWinForms();

			var appName = Path.GetFileName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			var q = "\"";
			MessageBox.Show($@"{appName} allows you to safely add or remove values from the system's PATH variable.

Call this executable with two arguments:
  1. Action {q}add{q} or {q}remove{q} to add or remove a value
  2. The value to add or remove

Example:
  {appName} add {q}C:\Program Files\RepoZ{q}

Note:
  You should put your value in quotes ({q}{q}) if it contains spaces.
  This app is case insensitive, it will ignore value casing.
", appName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private static void InitWinForms()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (Environment.OSVersion.Version.Major >= 6)
				SetProcessDPIAware();
		}
	}
}