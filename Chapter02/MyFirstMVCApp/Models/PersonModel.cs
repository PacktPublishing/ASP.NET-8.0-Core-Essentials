namespace MyFirstMVCApp.Models
{
    public class PersonModel
    {  
        public Guid Id { get; set; } 
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public PersonModel()
        {
            Id = Guid.NewGuid();
            
        }

        public PersonModel(string name, DateTime dateOfBirth) : this()
        {
            name = Name;
            dateOfBirth = DateOfBirth;
        }
    }
}