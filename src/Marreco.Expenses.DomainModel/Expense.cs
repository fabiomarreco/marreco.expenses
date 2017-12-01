using Marreco.Expenses.DomainModel;
using GeoCoordinatePortable;
public class Expense : Transaction
{
    public Category Category { get; private set; }
    public GeoCoordinate Location { get; private set; }
    public Account  Account { get; private set; }
}
