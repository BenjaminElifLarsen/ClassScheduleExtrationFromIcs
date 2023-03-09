namespace ClassScheduleExtrationFromIcs;

internal static class Startup
{
    private static string _folderName = "ClassSchedule";
	private static string _path;
	public static string GetFolderPath => _path + "\\" + _folderName;

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
