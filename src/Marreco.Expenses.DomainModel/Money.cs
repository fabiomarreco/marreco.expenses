using Marreco.Expenses.DomainModel;

public class Money : ValueObject<Money>
{
    public readonly decimal Value;

    public Money(decimal value)
    {
        this.Value = value;
    }
}