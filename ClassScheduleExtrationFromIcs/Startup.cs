namespace ClassScheduleExtrationFromIcs;

internal static class Startup
{
    private static string _foldername = "ClassSchedule";
	private static string _path;
	public static string GetFolderPath => _path + "\\" + _foldername;

	static Startup()
	{
		_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		if (!DoesFolderExist()) CreateFolder();
	}

	private static bool DoesFolderExist()
	{
		return Directory.Exists(GetFolderPath);
	}

	private static void CreateFolder()
	{
		Directory.CreateDirectory(GetFolderPath);
	}


}
