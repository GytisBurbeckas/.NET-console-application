using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingManager.Models
{
    public class Meeting
    {

        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public MeetingCategory Category { get; set; }
        public MeetingType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Person> Persons { get; set; }
        public Meeting()
        {
        }

        public Meeting(string name, string responsiblePerson, string description, MeetingCategory category, MeetingType type, DateTime startDate, DateTime endDate, List<Person> persons)
        {
            Name = name;
            ResponsiblePerson = responsiblePerson;
            Description = description;
            Category = category;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Persons = persons;
        }

        public override string ToString()
        {
            return ($"{Name,-20} {ResponsiblePerson,20} {Description,20} {Category,20} {Type,10} {StartDate,10} {EndDate,10}");
        }
    }
}
