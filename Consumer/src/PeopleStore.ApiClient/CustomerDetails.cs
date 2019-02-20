using System;

namespace PeopleStore.ApiClient
{
    public class CustomerDetails
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
    }
}