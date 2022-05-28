using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Models
{
    public class Person
    {
        public string Name { get; set; }

        public DateTime AddDate { get; set; }

        public Person(string name, DateTime addDate)
        {
            this.Name = name;
            this.AddDate = addDate;
        }

        public Person()
        {
        }


    }
}
