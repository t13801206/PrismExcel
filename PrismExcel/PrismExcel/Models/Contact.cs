using System;
using System.Collections.Generic;
using System.Text;

namespace PrismExcel.Models
{
    public class Contact
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public string this[int i] => i switch
        {
            0 => Name,
            1 => Age.ToString(),
            2 => City,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public static List<Contact> GenerateInitList()
        {
            return new List<Contact>
            {
                new Contact() { Name = "Yamada", Age = 10, City = "Tokyo"},
                new Contact() { Name = "Suzuki", Age = 12, City = "Tokyo" },
                new Contact() { Name = "Sato", Age = 28, City = "Osaka" },
                new Contact() { Name = "Yamada", Age = 15, City = "Tokyo" },
                new Contact() { Name = "Suzuki", Age = 10, City = "Yokohama" },
            };
        }
    }
}
