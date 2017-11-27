using System;
using Marreco.Expenses.DomainModel;
using GeoCoordinatePortable;

public class Transaction : IEntity
{
    public Money Value { get; private set; }
    public DateTimeOffset When { get; private set; }
    public string Description { get; set; }

    // public int Conciliation { get; set; }

    public void UpdateValue (Money newValue) => Value = newValue;
    public void UpdateWhen(DateTimeOffset newDate) => When = newDate;
    public void UpdateDescription (string newDescription) => this.Description = newDescription;
}

public class Expense : Transaction
{
    public Category Category { get; private set; }
    public GeoCoordinate Location { get; private set; }
    public Account  Account { get; private set; }
}

public class Income : Transaction 
{
    public Account Account { get; private set; }
}


public class Transfer : Transaction 
{
    public Account Origin { get; private set; }
    public Account Destination { get; private set; }
}
