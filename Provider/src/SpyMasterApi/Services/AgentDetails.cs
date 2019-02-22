using System;

namespace SpyMasterApi.Services
{
    public class AgentDetails
    {
        public string Name { get; }
        public string Surname { get; }
        public DateTime DateOfBirth { get; }
        public int Age { get; }

        public AgentDetails(string name, string surname, DateTime dateOfBirth, int age)
        {
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Age = age;
        }
    }
}