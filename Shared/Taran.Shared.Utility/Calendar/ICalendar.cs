namespace Taran.Shared.Utility.Services.Calendar;

public interface ICalendar
{
    string ConvertDate(DateTime date, bool appendTime = false);
    string ConvertDate(DateOnly date);
    string ConvertToHumanReadable(DateTime date);
    DateTime Convert(string date);
    DateOnly ConvertToDateOnly(string date);
    bool TryParse(string jalaliDate, out DateTime date);
    bool TryParse(string jalaliDate, out DateOnly date);
}