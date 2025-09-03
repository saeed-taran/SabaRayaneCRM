namespace Taran.Shared.Dtos.Services.Calendar;

public class PersianCalendar : ICalendar
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

    public bool TryParse(string jalaliDate, out DateTime date)
    {
        date = DateTime.MinValue;
        if (string.IsNullOrEmpty(jalaliDate))
            return false;

        try
        {
            int Year = int.Parse(jalaliDate.Substring(0, 4));
            int Month = int.Parse(jalaliDate.Substring(5, jalaliDate.IndexOf("/", 5) - 5));
            int Day = int.Parse(jalaliDate.Substring(jalaliDate.LastIndexOf("/") + 1));
            date = new DateTime(Year, Month, Day, new System.Globalization.PersianCalendar());
            return true;
        }
        catch
        {
            date = DateTime.MinValue;
            return false;
        }
    }

    public bool TryParse(string jalaliDate, out DateOnly date)
    {
        if (string.IsNullOrEmpty(jalaliDate))
        {
            var _date = DateTime.MinValue;
            date = DateOnly.FromDateTime(_date);
            return false;
        }

        try
        {
            int Year = int.Parse(jalaliDate.Substring(0, 4));
            int Month = int.Parse(jalaliDate.Substring(5, jalaliDate.IndexOf("/", 5) - 5));
            int Day = int.Parse(jalaliDate.Substring(jalaliDate.LastIndexOf("/") + 1));
            var _date = new DateTime(Year, Month, Day, new System.Globalization.PersianCalendar());
            date = DateOnly.FromDateTime(_date);
            return true;
        }
        catch
        {
            var _date = DateTime.MinValue;
            date = DateOnly.FromDateTime(_date);
            return false;
        }
    }

    public string ConvertDate(DateTime date, bool appendTime = false)
    {
        string persianDate = "";
        System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
        persianDate += calendar.GetYear(date) + "/";
        persianDate += calendar.GetMonth(date).ToString("0#") + "/";
        persianDate += calendar.GetDayOfMonth(date).ToString("0#") + " ";

        if (appendTime)
        {
            persianDate += date.Hour + ":";
            persianDate += date.Minute;
        }

        return persianDate;
    }

    public string ConvertDate(DateOnly date)
    {
        string persianDate = "";
        System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();

        var _dateTime = new DateTime(date, new TimeOnly());
        persianDate += calendar.GetYear(_dateTime) + "/";
        persianDate += calendar.GetMonth(_dateTime).ToString("0#") + "/";
        persianDate += calendar.GetDayOfMonth(_dateTime).ToString("0#") + " ";

        return persianDate;
    }

    public DateOnly ConvertToDateOnly(string date) 
    {
        var dateTime = Convert(date);
        return DateOnly.FromDateTime(dateTime);
    }

    public DateTime Convert(string date)
    {
        if (string.IsNullOrWhiteSpace(date))
            throw new ArgumentNullException("date cant be empty");

        if (date.Contains(' '))
            date = date.Substring(0, date.IndexOf(' '));

        if (date.Contains('-'))
            date = date.Replace('-', '/');

        string[] dateParts = date.Split('/');
        if (dateParts.Length != 3)
            throw new Exception("date has invalid parts");

        int year = int.Parse(dateParts[0]);
        int month = int.Parse(dateParts[1]);
        int day = int.Parse(dateParts[2]);

        System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();
        return calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
    }

    public string ConvertToHumanReadable(DateTime date)
    {
        string persianDate = "";
        System.Globalization.PersianCalendar calendar = new System.Globalization.PersianCalendar();

        persianDate += calendar.GetDayOfMonth(date) + " ";
        persianDate += MonthNames[calendar.GetMonth(date) - 1] + " ";
        persianDate += calendar.GetYear(date);

        return persianDate;
    }
}