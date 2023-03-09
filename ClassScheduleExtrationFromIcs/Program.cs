using ClassScheduleExtrationFromIcs;
using ClassScheduleExtrationFromIcs.FileHandling;
using ClassScheduleExtrationFromIcs.ISC;

var filePath = Startup.GetFolderPath;


Console.WriteLine("Mappe sti: " + filePath);
ReadIcsFile.AnalyseFolder(out IEnumerable<LectureICS> lectures);
Console.WriteLine(lectures.Count());

var groups = lectures.GroupBy(x => x.Lecture);
Console.WriteLine(groups.Count());