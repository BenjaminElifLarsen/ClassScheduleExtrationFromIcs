
using ClassScheduleExtrationFromIcs.ISC;
using System.Text.RegularExpressions;

namespace ClassScheduleExtrationFromIcs.FileHandling;

internal class ReadIcsFile
{
    public static void AnalyseFolder(out IEnumerable<LectureICS> lectures)
    {
        var path = Startup.GetFolderPath;
        var filesInFolder = Directory.EnumerateFiles(path);
        List<LectureICS> foundLectures = new();
        foreach(var file in filesInFolder)
        {
            string[] parts = file.Split('.');
            if(parts.Last().ToLower() == "ics")
            {
                //List<string> lectures = new();
                var icsData = File.ReadAllLines(file);
                for(int i = 0; i < icsData.Length; i++)
                {
                    if (icsData[i] == "BEGIN:VEVENT")
                    {
                        List<string> eventParts = new();
                        while (icsData[i] != "END:VEVENT")
                        {
                            i++;
                            eventParts.Add(icsData[i]);
                        }
                        bool isFlex = IsFlexSchedule(eventParts);
                        if (isFlex)
                        {
                            bool containsClass = ContainsAClass(eventParts);
                            if (containsClass)
                            {
                                LectureICS lecture = LectureICS.CreateLecture(eventParts);
                                foundLectures.Add(lecture);
                            }
                        }
                    }
                }
            }
        }
        lectures = foundLectures;
    }

    private static bool IsFlexSchedule(IEnumerable<string> eventData)
    {
        return eventData.Any(x => x.Contains(VeVentType.LECTURE_INDICATOR));
    }

    private static bool ContainsAClass(IEnumerable<string> eventData)
    {
        var summary = eventData.SingleOrDefault(x => x.Contains("SUMMARY;"));
        if (summary is null) return false;
        return PermittedClassValues.CLASS_INDICATOR.Any(summary.Contains);
    }
}
