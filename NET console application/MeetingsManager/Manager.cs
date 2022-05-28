using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingManager.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MeetingManager
{
    public class Manager
    {
        private string ActiveUserName = "";
        private readonly string DataFile;
        public List<Meeting> Meetings;

        public Manager(string dataFile)
        {
            this.DataFile = dataFile;
            //Console.WriteLine(DataFile);
        }

        /// <summary>
        /// Starting method that displays all commands, gets data from json file and saves data to json file
        /// </summary>
        public void Start()
        {
            try
            {
                this.Meetings = JsonConvert.DeserializeObject<List<Meeting>>(File.ReadAllText(DataFile));
            }
            catch
            {
                Console.WriteLine("Change filepath in Program.cs to your own \n" +
                    "Otherwise meetings data won't be saved after program is turned off. \n" +
                    "Not sure why it's like that. But I noticed that if file path is given with @ and the filepath is absolute, it works. \n" +
                    "Otherwise program is trying to get file starting from debug folder.");

            }
            SetCurrentUserName();
            ShowCommandsNumber();
            DisplayAllCommands();
            WriteDataToFile(Meetings);
        }

        /// <summary>
        /// Displays command and number to console
        /// </summary>
        public void ShowCommandsNumber()
        {
            Console.WriteLine("------------------COMMANDS-----------------------");
            Console.WriteLine("Add meeting - 1");
            Console.WriteLine("Delete meeting - 2");
            Console.WriteLine("Add person to meeting - 3");
            Console.WriteLine("Delete person to meeting - 4");
            Console.WriteLine("Display all meetings - 5");
            Console.WriteLine("Filter meetings by description - 6");
            Console.WriteLine("Filter meetings by responsible person - 7");
            Console.WriteLine("Filter meetings by category - 8");
            Console.WriteLine("Filter meetings by type - 9");
            Console.WriteLine("Filter meetings by date - 10");
            Console.WriteLine("Filter meetings by attendees - 11");
            Console.WriteLine("Remind commands - 100");
            Console.WriteLine("--------------------------------------------------");
        }

        /// <summary>
        /// Displays all comands and based on given input calls them
        /// </summary>
        public void DisplayAllCommands()
        {
            if (this.Meetings == null)
            {
                WriteNoMeeting();
            }

            Console.WriteLine("Waiting for command.");
            string command = Console.ReadLine();

            if (command == "1")
            {
                AddMeeting();
                DisplayAllCommands();
            }
            else if (command == "2" && this.Meetings != null)
            {
                DeleteMeeting();
                DisplayAllCommands();
            }
            else if (command == "3" && this.Meetings != null)
            {
                AddPerson();
                DisplayAllCommands();
            }
            else if (command == "4" && this.Meetings != null)
            {
                DeletePersonFromMeeting();
                DisplayAllCommands();
            }
            else if (command == "5" && this.Meetings != null)
            {
                displayAllMeeting();
                DisplayAllCommands();
            }
            else if (command == "6" && this.Meetings != null)
            {
                FilterByDescription();
                DisplayAllCommands();
            }
            else if (command == "7" && this.Meetings != null)
            {
                FilterByResponsiblePerson();
                DisplayAllCommands();
            }
            else if (command == "8" && this.Meetings != null)
            {
                FilterByCategory();
                DisplayAllCommands();
            }
            else if (command == "9" && this.Meetings != null)
            {
                FilterByType();
                DisplayAllCommands();
            }
            else if (command == "10" && this.Meetings != null)
            {
                FilterByDate();
                DisplayAllCommands();
            }
            else if (command == "11" && this.Meetings != null)
            {
                FilterByAttendees();
                DisplayAllCommands();
            }
            else if (command == "100")
            {
                ShowCommandsNumber();
                DisplayAllCommands();
            }
            else if (command == "C")
            {
                return;
            }
            else
            {
                DisplayAllCommands();
            }
        }

        public void WriteNoMeeting()
        {
            Console.WriteLine("There are no meetings");
        }

        /// <summary>
        /// Sets current active user name
        /// </summary>
        public void SetCurrentUserName()
        {
            Console.WriteLine("Write your name.");
            this.ActiveUserName = Console.ReadLine();
            Console.WriteLine($"Welcome {ActiveUserName}!");
        }

        /// <summary>
        /// Gets meetings data form json file
        /// </summary>
        /// <param name="filePath"></param>
        public void GetDataFromFile(string filePath)
        {
            this.Meetings = JsonConvert.DeserializeObject<List<Meeting>>(File.ReadAllText(filePath));
        }

        /// <summary>
        /// Writes meetings data to json file
        /// </summary>
        /// <param name="meetings"></param>
        public void WriteDataToFile(List<Meeting> meetings)
        {
            string json = JsonConvert.SerializeObject(meetings, Formatting.Indented);

            File.WriteAllText(DataFile, json);
        }

        /// <summary>
        /// Creates new meeting with given values and adds it to all meetings list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="responsiblePerson"></param>
        /// <param name="description"></param>
        /// <param name="meetingCatefory"></param>
        /// <param name="tyoe"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void CreateMeeting(string name, string responsiblePerson, string description, MeetingCategory meetingCatefory, MeetingType tyoe, DateTime startDate, DateTime endDate)
        {
            Meeting meeting = new Meeting
            {
                Name = name,
                ResponsiblePerson = responsiblePerson,
                Description = description,
                Category = meetingCatefory,
                Type = tyoe,
                StartDate = startDate,
                EndDate = endDate,
                Persons = new List<Person>()
            };

            if (this.Meetings == null)
            {
                this.Meetings = new List<Meeting>();
                this.Meetings.Add(meeting);
                Console.WriteLine("New meeting added");
            }
            else
            {
                this.Meetings.Add(meeting);
                Console.WriteLine("New meeting added");
            }
        }
        /// <summary>
        /// Gets all values needed to create new meeting from console input and calls method that is responsible for creating new meeting
        /// </summary>
        public void AddMeeting()
        {
            Console.WriteLine("Write name");
            string name = Console.ReadLine();
            Console.WriteLine("Write responsible person");
            string responsiblePerson = Console.ReadLine();
            Console.WriteLine("Write description");
            string description = Console.ReadLine();

            MeetingCategory category = SelectCategory();
            MeetingType type = SelectType();

            Console.WriteLine("Write start date format dd/MM/yyyy h:mm tt");
            string startDateString = Console.ReadLine();
            DateTime startDate = GetDate(startDateString);

            Console.WriteLine("Write end date format dd/MM/yyyy h:mm tt");
            string endDateString = Console.ReadLine();
            DateTime endDate = GetDate(endDateString);

            CreateMeeting(name, responsiblePerson, description, category, type, startDate, endDate);

        }
        /// <summary>
        /// Displays for user categories of Meeting and returns type by given input of user
        /// </summary>
        /// <returns></returns>
        public MeetingCategory SelectCategory()
        {
            Console.WriteLine("Select category");
            Console.WriteLine($"{MeetingCategory.CodeMonkey} - 0");
            Console.WriteLine($"{MeetingCategory.Hub} - 1");
            Console.WriteLine($"{MeetingCategory.Short} - 2");
            Console.WriteLine($"{MeetingCategory.TeamBuilding} - 3");

            string categoryString = Console.ReadLine();
            MeetingCategory category = MeetingCategory.CodeMonkey;

            if (categoryString == "0")
            {
                category = MeetingCategory.CodeMonkey;
            }
            else if (categoryString == "1")
            {
                category = MeetingCategory.Hub;
            }
            else if (categoryString == "2")
            {
                category = MeetingCategory.Short;
            }
            else if (categoryString == "3")
            {
                category = MeetingCategory.TeamBuilding;
            }
            else
            {
                Console.WriteLine("This category does not exist.");
                DisplayAllCommands();
            }

            return category;
        }

        /// <summary>
        /// Displays for user types of Meeting and returns type by given input of user
        /// </summary>
        /// <returns></returns>
        public MeetingType SelectType()
        {
            Console.WriteLine("Select type");
            Console.WriteLine($"{MeetingType.Live} - 0");
            Console.WriteLine($"{MeetingType.InPerson} - 1");

            string typeString = Console.ReadLine();
            MeetingType type = MeetingType.Live;

            if (typeString == "0")
            {
                type = MeetingType.Live;
            }
            else if (typeString == "1")
            {
                type = MeetingType.InPerson;
            }
            else
            {
                Console.WriteLine("This type does not exist.");
                DisplayAllCommands();
            }

            return type;
        }

        /// <summary>
        /// Deletes meeting if the current active user is responsible for meeting
        /// </summary>
        public void DeleteMeeting()
        {
            if (this.Meetings != null)
            {
                Console.WriteLine("Write meeting name that you want to delete");
                string meetingName = Console.ReadLine();
                bool exist = false;
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.Name == meetingName)
                    {
                        if (meeting.ResponsiblePerson == this.ActiveUserName)
                        {
                            Meetings.Remove(meeting);
                            exist = true;
                            Console.WriteLine($"Meeting {meeting.Name} deleted");
                            DisplayAllCommands();
                            break;
                        }
                        else
                        {
                            exist = true;
                            Console.WriteLine("You are not the responsible person for this meeting.");
                            DisplayAllCommands();
                            break;
                        }
                    }
                }

                if (exist == false)
                {
                    Console.WriteLine($"Meeting {meetingName} does not exist");
                    DisplayAllCommands();
                }
            }
            else
            {
                Console.WriteLine("There are no meetings.");
                DisplayAllCommands();
            }
        }

        /// <summary>
        /// Creates new person and calls method that is responsible for adding it
        /// </summary>
        public void AddPerson()
        {
            Console.WriteLine("Write person's name");
            string personName = Console.ReadLine();
            DateTime addTime = SetPersonDate();
            Person newPerson = new Person(personName, addTime);
            AddPersonToMeeting(newPerson);
        }

        /// <summary>
        /// Adds person to the meeting if he is not arealidy added and if meeting exists.
        /// </summary>
        /// <param name="person"></param>
        public void AddPersonToMeeting(Person person)
        {
            Console.WriteLine("Write meeting's name");
            string meetingsName = Console.ReadLine();
            bool contains = false;

            contains = MeetingExist(meetingsName);


            if (contains == false)
            {
                Console.WriteLine("Meeting with this name does not exist.");
                DisplayAllCommands();
            }
            else
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.Name == meetingsName)
                    {
                        if (PersonExistInMeeting(meeting, person.Name) == true)
                        {
                            Console.WriteLine("This person is arealidy added!");
                            DisplayAllCommands();
                            break;
                        }
                        else
                        {
                            meeting.Persons.Add(person);
                            Console.WriteLine($"{person.Name} added to {meeting.Name} meeting.");
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Checks if meeting with given name exists and returns bool
        /// </summary>
        /// <param name="meetingName"></param>
        /// <returns></returns>
        public bool MeetingExist(string meetingName)
        {
            bool exist = false;

            foreach (Meeting meeting in this.Meetings)
            {
                if (meeting.Name == meetingName)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        /// <summary>
        /// Creates person and calls method that is responsible for deleting that person.
        /// </summary>
        public void DeletePersonFromMeeting()
        {
            Console.WriteLine("Write person's name");
            string personName = Console.ReadLine();
            DateTime addTime = DateTime.Now;
            Person newPerson = new Person(personName, addTime);
            DeletePerson(newPerson);

        }
        /// <summary>
        /// Deletes given person from meeting if he exist in meeting. Does not delete person if he is responsible person in that meeting.
        /// </summary>
        /// <param name="person"></param>
        public void DeletePerson(Person person)
        {
            Console.WriteLine("Write meeting's name");
            string meetingsName = Console.ReadLine();
            bool contains = false;

            contains = MeetingExist(meetingsName);

            if (contains == false)
            {
                Console.WriteLine("Meeting with this name does not exist.");
                DisplayAllCommands();
            }
            else
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.Name == meetingsName)
                    {
                        if (PersonExistInMeeting(meeting, person.Name) == true)
                        {
                            if (meeting.ResponsiblePerson == person.Name)
                            {
                                Console.WriteLine("You cant remove responsible person");
                                break;
                            }
                            else
                            {
                                RemovePersonFromMeeeting(meeting, person.Name);
                                Console.WriteLine("Person removed");
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("This person does not exist in this meeting.");
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if person with given name exist in given meeting and based on that returns bool
        /// </summary>
        /// <param name="meeting"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool PersonExistInMeeting(Meeting meeting, string name)
        {
            bool exist = false;

            foreach (Person person in meeting.Persons)
            {
                if (person.Name == name)
                {
                    exist = true;
                    break;
                }
            }

            return exist;
        }
        /// <summary>
        /// Deletes person of given name from person list which of given meeting
        /// </summary>
        /// <param name="meeting"></param>
        /// <param name="name"></param>
        public void RemovePersonFromMeeeting(Meeting meeting, string name)
        {
            foreach (Person person in meeting.Persons)
            {
                if (person.Name == name)
                {
                    meeting.Persons.Remove(person);
                    break;
                }
            }
        }

        /// <summary>
        /// Creates date from given value of console by format.
        /// </summary>
        /// <returns></returns>
        private DateTime SetPersonDate()
        {
            Console.WriteLine("Write when to add. Format is dd/MM/yyyy h:mm tt");

            string date = Console.ReadLine(), format = "dd/MM/yyyy h:mm tt";

            DateTime newDate = DateTime.Now;
            try
            {
                if (date.Contains("/"))
                {
                    format = "dd/MM/yyyy h:mm tt";
                }
                else
                {
                    Console.WriteLine("Wrong date. Write addDate dd/MM/yyyy h:mm tt");
                    DisplayAllCommands();
                }

                newDate = DateTime.ParseExact(date, format, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong date. Write addDate dd/MM/yyyy h:mm tt");
                DisplayAllCommands();
            }
            return newDate;
        }

        /// <summary>
        /// Displays all meetings in console
        /// </summary>
        public void displayAllMeeting()
        {
            Console.WriteLine($"{"Name",-20} {"ResponsiblePerson",20} {"Description",20} {"Category",20} {"Type",10} {"StartDate",15} {"EndDate",15}");

            if (Meetings != null)
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    Console.WriteLine(meeting.ToString());
                }
            }
        }
        /// <summary>
        /// Filters meetings by description and displays them in console
        /// </summary>
        public void FilterByDescription()
        {
            Console.WriteLine("Write discription");
            string description = Console.ReadLine();

            Console.WriteLine($"Filtered by description : {description}");
            Console.WriteLine($"{"Name",-20} {"ResponsiblePerson",20} {"Description",20} {"Category",20} {"Type",10} {"StartDate",15} {"EndDate",15}");

            if (Meetings != null)
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.Description.Contains(description))
                    {
                        Console.WriteLine(meeting.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Filters meetings by responsible person and displays them in console
        /// </summary>
        public void FilterByResponsiblePerson()
        {
            Console.WriteLine("Write responsiblePerson");
            string responsiblePerson = Console.ReadLine();

            Console.WriteLine($"Filtered by responsiblePerson : {responsiblePerson}");
            Console.WriteLine($"{"Name",-20} {"ResponsiblePerson",20} {"Description",20} {"Category",20} {"Type",10} {"StartDate",15} {"EndDate",15}");

            if (Meetings != null)
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.ResponsiblePerson == responsiblePerson)
                    {
                        Console.WriteLine(meeting.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Filters meetings by category and displays them in console
        /// </summary>
        public void FilterByCategory()
        {
            Console.WriteLine("Write category");
            string category = Console.ReadLine();

            Console.WriteLine($"Filtered by category : {category}");
            Console.WriteLine($"{"Name",-20} {"ResponsiblePerson",20} {"Description",20} {"Category",20} {"Type",10} {"StartDate",15} {"EndDate",15}");

            if (Meetings != null)
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.Category.ToString() == category)
                    {
                        Console.WriteLine(meeting.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Filters meetings by type and displays them in console
        /// </summary>
        public void FilterByType()
        {
            Console.WriteLine("Write type");
            string type = Console.ReadLine();

            Console.WriteLine($"Filtered by type : {type}");
            Console.WriteLine($"{"Name",-20} {"ResponsiblePerson",20} {"Description",20} {"Category",20} {"Type",10} {"StartDate",15} {"EndDate",15}");

            if (Meetings != null)
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.Type.ToString() == type)
                    {
                        Console.WriteLine(meeting.ToString());
                    }
                }
            }
        }
        /// <summary>
        /// Based on filter by 1 or 2 dates calls method that filters by date or dates
        /// </summary>
        public void FilterByDate()
        {
            Console.WriteLine("Do you want to filter from date or between dates? \n \t Write 1 to filter from  \n \t  Write 2 to filter between");
            string filterBy = Console.ReadLine();

            if (filterBy == "1")
            {
                FilterDateFrom();
            }
            else if (filterBy == "2")
            {
                FilterDateBetween();
            }
            else
            {
                Console.WriteLine("Wrong number");
                DisplayAllCommands();
            }
        }
        /// <summary>
        /// Filters meetings from date
        /// </summary>
        public void FilterDateFrom()
        {
            DateTime from;

            Console.WriteLine("Write date with format dd/MM/yyyy h:mm tt");
            string date = Console.ReadLine();
            from = GetDate(date);

            Console.WriteLine($"Filtered by date from : {from}");
            Console.WriteLine($"{"Name",-20} {"ResponsiblePerson",20} {"Description",20} {"Category",20} {"Type",10} {"StartDate",15} {"EndDate",15}");

            if (Meetings != null)
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.StartDate >= from)
                    {
                        Console.WriteLine(meeting.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Filters meetings between 2 dates : from, to and displays in console
        /// </summary>
        public void FilterDateBetween()
        {
            DateTime from;
            DateTime to;

            Console.WriteLine("Write date FROM with format dd/MM/yyyy h:mm tt");
            string date = Console.ReadLine();
            from = GetDate(date);

            Console.WriteLine("Write date TO with format dd/MM/yyyy h:mm tt");
            string dateTo = Console.ReadLine();
            to = GetDate(dateTo);

            Console.WriteLine($"Filtered by dates bettwe : {from} - {to}");
            Console.WriteLine($"{"Name",-20} {"ResponsiblePerson",20} {"Description",20} {"Category",20} {"Type",10} {"StartDate",15} {"EndDate",15}");
            if (Meetings != null)
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.StartDate >= from && meeting.StartDate <= to)
                    {
                        Console.WriteLine(meeting.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Creates date from given input
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetDate(string date)
        {
            string format = "dd/MM/yyyy h:mm tt";

            DateTime newDate = DateTime.Now;
            try
            {
                newDate = DateTime.ParseExact(date, format, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong date. Write with format dd/MM/yyyy h:mm tt");
                DisplayAllCommands();
            }
            return newDate;
        }

        /// <summary>
        /// Filters and displays in console meetings by attendees number
        /// </summary>
        public void FilterByAttendees()
        {
            Console.WriteLine("Write minimum attendees number");
            string countString = Console.ReadLine();
            int count = int.Parse(countString);

            Console.WriteLine($"Filtered by minimum attendees : {countString}");
            Console.WriteLine($"{"Name",-20} {"ResponsiblePerson",20} {"Description",20} {"Category",20} {"Type",10} {"StartDate",15} {"EndDate",15}");

            if (Meetings != null)
            {
                foreach (Meeting meeting in this.Meetings)
                {
                    if (meeting.Persons.Count >= count)
                    {
                        Console.WriteLine(meeting.ToString());
                    }
                }
            }
        }
    }
}
