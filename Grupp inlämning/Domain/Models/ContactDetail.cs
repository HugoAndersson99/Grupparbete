namespace Domain.Models
{
    public class ContactDetail
    {
        public Guid Id { get; set; }
        public Guid CVId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public CV CV { get; set; }

        public ContactDetail()
        {
        }

        public ContactDetail(Guid id, Guid cvId, string name, string email, string phone, string address, int zipCode, CV Cv)
        {
            Id = id;
            CVId = cvId;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            ZipCode = zipCode;
            CV = Cv;
        }
    }
}
