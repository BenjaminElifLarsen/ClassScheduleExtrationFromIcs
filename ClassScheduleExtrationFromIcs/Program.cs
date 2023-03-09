using ClassScheduleExtrationFromIcs;
using ClassScheduleExtrationFromIcs.FileHandling;
using ClassScheduleExtrationFromIcs.ICS;

var filePath = Startup.GetFolderPath;


Console.WriteLine("Folder path: " + filePath);
ReadIcsFile.AnalyseFolder(out IEnumerable<LectureICS> lectures);
Console.WriteLine("Lecture amount: " + lectures.Count());

var groupByLecture = lectures.GroupBy(x => x.Lecture);
Console.WriteLine("Lecture groupping amount: " + groupByLecture.Count());

/*
 * schedule for a class
 * A class have 5 days each week
 * A day can consist of one or more courses
 * A course can be taught to a single class or multiple classes at ones
 * There is no overlap between course times on a day, it is not possible to have multiple courses at ones for a class
 * 
 * current design contains a 'single' shedule that holds all days. Each day know of the courses on that day for all classes and when the individual start and end time of each course part. 
 */