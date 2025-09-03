namespace Taran.Shared.Dtos.Services.Calendar;

public interface ICalendar
{
    string ConvertDate(DateTime date, bool appendTime = false);
    string ConvertDate(DateOnly date);
    string ConvertToHumanReadable(DateTime date);
    DateTime Convert(string date);
    DateOnly ConvertToDateOnly(string date);
    bool TryParse(string stringDate, out DateTime date);
    bool TryParse(string stringDate, out DateOnly date);
}