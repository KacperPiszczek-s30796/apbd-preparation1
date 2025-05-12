namespace WebApplication4.entities;

public class Order:BaseEntitie
{
    public int IdProduct { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FulfilledAt { get; set; }
    
}