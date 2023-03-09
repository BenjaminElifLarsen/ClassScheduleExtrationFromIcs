namespace ClassScheduleExtrationFromIcs.ICS;

internal class LectureICS
{
    private string _description;
    private DateTime _stamp;
    private DateTime _start;
    private string _location;
    private string _lecture;
    private DateTime _end;
    private List<string> _classes;

    public DateTime Start => _start;
    public DateTime End => _end;
    public IEnumerable<string> Classses => _classes;
    public string Location => _location;
    public string Lecture => _lecture;

    private LectureICS(string description, string distamp, string distart, string location, string lectureAndClass, string dtend)
    {
        _description = description;
        _stamp = ConvertEventToDateTime(distamp);
        _start = ConvertEventToDateTime(distart);
        _location = location;
        _lecture = lectureAndClass.Split(PermittedClassValues.CLASS_INDICATOR, 10, StringSplitOptions.RemoveEmptyEntries).First();//.Split(' ').First();
        _end = ConvertEventToDateTime(dtend);
        _classes = new List<string>();
        foreach (string classValue in PermittedClassValues.CLASS_INDICATOR)
        {
            if (lectureAndClass.Contains(classValue)) _classes.Add(classValue);
        }
    }

    public static LectureICS CreateLecture(IEnumerable<string> eventParts)
    {
        var descriptionParts = eventParts.First(x => x.Contains("DESCRIPTION")).Split(':').Where(x => !x.Contains("DESCRIPTION"));
        string description = "";
        foreach (var part in descriptionParts)
        {
            description += part;
        }

        string distamp = eventParts.First(x => x.Contains("DTSTAMP")).Split(':').Last();
        string distart = eventParts.First(x => x.Contains("DTSTART")).Split(':').Last();
        string diend = eventParts.First(x => x.Contains("DTEND")).Split(':').Last();
        string location = eventParts.First(x => x.Contains("LOCATION")).Split(':').Last();
        string lectureAndClass = eventParts.First(x => x.Contains("SUMMARY;LANGUAGE")).Split(':').Last();
        return new LectureICS(description, distamp, distart, location, lectureAndClass, diend);
    }

    private DateTime ConvertEventToDateTime(string toConvert)
    {
        string[] parts = toConvert.Split(new char[] { 'T', 'Z' });
        var year = int.Parse(parts[0][0..4]);
        var month = int.Parse(parts[0][4..6]);
        var day = int.Parse(parts[0][6..8]);
        var hour = int.Parse(parts[1][0..2]);
        var min = int.Parse(parts[1][2..4]);
        var sec = int.Parse(parts[1][4..6]);
        return new(year, month, day, hour, min, sec);
    }
}


//BEGIN:VEVENT
//CATEGORIES:FLEX-skema //this on seems unique
//CLASS:PUBLIC
//CREATED:20220517T042219Z
//DESCRIPTION:Lektion 2\n\n[FLEX - ID 851401:3469194]
//DTEND:20220810T083000Z
//DTSTAMP:20221206T134953Z
//DTSTART:20220810T072000Z
//LAST-MODIFIED:20220517T042420Z
//LOCATION:C117S
//PRIORITY:5
//SEQUENCE:0
//SUMMARY;LANGUAGE=da-dk:grndl program.h1pd081122 //meetings only have da. not da-dk
//TRANSP:OPAQUE
//UID:040000008200E00074C5B7101A82E0080000000082C89CB2A569D801000000000000000
//	01000000073290ADE7185DA4E8DEA21D6A6C8B074
//X-MICROSOFT-CDO-BUSYSTATUS:BUSY
//X-MICROSOFT-CDO-IMPORTANCE:1
//END:VEVENT
