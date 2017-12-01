using System;

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
