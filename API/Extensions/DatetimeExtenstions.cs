namespace API.Extensions;

public static class DatetimeExtenstions
{
    public static int CalculateAge(this DateOnly dateOnly)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        var age = today.Year - dateOnly.Year;

        if (dateOnly > today.AddYears(-age)) age--;

        return age;
    }
}
