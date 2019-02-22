using System;

namespace SpyMasterApi.Services
{
    public interface ICustomerService
    {
        Customer Get(string id);
    }

    public class Customer
    {
        public string Name { get; }
        public string Surname { get; }
        public DateTime DateOfBirth { get; }
        public int Age { get; }

        public Customer(string name, string surname, DateTime dateOfBirth, int age)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Age = age;
        }
    }
}