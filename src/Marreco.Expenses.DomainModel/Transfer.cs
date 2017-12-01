public class Transfer : Transaction 
{
    public Account Origin { get; private set; }
    public Account Destination { get; private set; }

    public void ChangeOrigin(Account origin) => Origin = origin;
    public void ChangeDestination(Account destination) => Destination = destination;
}
