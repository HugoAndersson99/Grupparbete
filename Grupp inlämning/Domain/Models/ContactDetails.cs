﻿namespace Domain.Models
{
    public class ContactDetails
    {
        public Guid Id { get; set; }
        public Guid CVId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }

        public ContactDetails()
        {
        }

        public ContactDetails(Guid id, Guid cvId, string name, string email, string phone, string address, int zipCode)
        {
            Id = id;
            CVId = cvId;
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            ZipCode = zipCode;
        }
    }
}
