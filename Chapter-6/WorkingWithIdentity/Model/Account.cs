namespace WorkingWithIdentity.Model;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual ICollection<Movement>? Movements { get; set; }
    
}