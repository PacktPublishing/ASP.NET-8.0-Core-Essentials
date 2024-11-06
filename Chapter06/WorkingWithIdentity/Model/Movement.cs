namespace WorkingWithIdentity.Model;

public class Movement
{
    public int Id { get; set; }
    
    public DateTime? Date { get; set; }
    
    public string Description { get; set; }
    
    public decimal Value { get; set; }
    
    public int AccountId { get; set; }
    
    public Account Account { get; set; }
}