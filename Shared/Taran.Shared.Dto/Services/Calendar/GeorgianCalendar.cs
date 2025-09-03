namespace Taran.Shared.Dtos.Services.Calendar;

public class GeorgianCalendar : ICalendar
{
    private int dayDayTypeRetrieved;
    private List<string> MonthNames = new List<string>()
        {
            "فروردین",
            "اردیبهشت",
            "خرداد",
            "تیر",
            "مرداد",
            "شهریور",
            "مهر",
            "آبان",
            "آذر",
            "دی",
            "بهمن",
            "اسفند"
        };

    public bool TryParse(string stringDate, out DateTime date)
    {
        return DateTime.TryParse(stringDate, out date);

        //date = DateTime.MinValue;
        //if (string.IsNullOrEmpty(stringDate))
        //    return false;

        //try
        //{
        //    int Year = int.Parse(stringDate.Substring(0, 4));
        //    int Month = int.Parse(stringDate.Substring(5, stringDate.IndexOf("/", 5) - 5));
        //    int Day = int.Parse(stringDate.Substring(stringDate.LastIndexOf("/") + 1));
        //    date = new DateTime(Year, Month, Day);
        //    return true;
        //}
        //catch
        //{
        //    date = DateTime.MinValue;
        //    return false;
        //}
    }

    public bool TryParse(string stringDate, out DateOnly date)
    {
        return DateOnly.TryParse(stringDate, out date);

        //if (string.IsNullOrEmpty(stringDate))
        //{
        //    var _date = DateTime.MinValue;
        //    date = DateOnly.FromDateTime(_date);
        //    return false;
        //}

        //try
        //{
        //    int Year = int.Parse(stringDate.Substring(0, 4));
        //    int Month = int.Parse(stringDate.Substring(5, stringDate.IndexOf("/", 5) - 5));
        //    int Day = int.Parse(stringDate.Substring(stringDate.LastIndexOf("/") + 1));
        //    var _date = new DateTime(Year, Month, Day);
        //    date = DateOnly.FromDateTime(_date);
        //    return true;
        //}
        //catch
        //{
        //    var _date = DateTime.MinValue;
        //    date = DateOnly.FromDateTime(_date);
        //    return false;
        //}
    }

    public string ConvertDate(DateTime date, bool appendTime = false)
    {
        string stringDate = "";

        stringDate += date.Year + "/";
        stringDate += date.Month.ToString("0#") + "/";
        stringDate += date.Day.ToString("0#") + " ";

        if (appendTime)
        {
            stringDate += date.Hour.ToString("0#") + ":";
            stringDate += date.Minute.ToString("0#");
        }

        return stringDate;
    }

    public string ConvertDate(DateOnly date)
    {
        string stringDate = "";

        var _dateTime = new DateTime(date, new TimeOnly());
        stringDate += date.Year + "/";
        stringDate += date.Month.ToString("0#") + "/";
        stringDate += date.Day.ToString("0#") + " ";

        return stringDate;
    }

    public DateOnly ConvertToDateOnly(string date) 
    {
        var dateTime = Convert(date);
        return DateOnly.FromDateTime(dateTime);
    }

    public DateTime Convert(string date)
    {
        DateTime dateTime;
        DateTime.TryParse(date, out dateTime);
        return dateTime;
        //if (string.IsNullOrWhiteSpace(date))
        //    throw new ArgumentNullException("date cant be empty");

        //if (date.Contains(' '))
        //    date = date.Substring(0, date.IndexOf(' '));

        //if (date.Contains('-'))
        //    date = date.Replace('-', '/');

        //string[] dateParts = date.Split('/');
        //if (dateParts.Length != 3)
        //    throw new Exception("date has invalid parts");

        //int year = int.Parse(dateParts[0]);
        //int month = int.Parse(dateParts[1]);
        //int day = int.Parse(dateParts[2]);

        //return new DateTime(year, month, day, 0, 0, 0, 0);
    }

    public string ConvertToHumanReadable(DateTime date)
    {
        string stringDate = "";

        stringDate += date.Day + " ";
        stringDate += MonthNames[date.Month - 1] + " ";
        stringDate += date.Year;

        return stringDate;
    }
}