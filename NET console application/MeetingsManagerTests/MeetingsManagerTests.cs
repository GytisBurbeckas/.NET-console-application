using NUnit.Framework;
using MeetingManager;
using MeetingManager.Models;
using System.Collections.Generic;
using System;

namespace MeetingsManagerTests
{
    public class Tests
    {    
        Manager manager;

        //Change filepath to your own absolute.
        string filePath = @"C:\Users\LEGION\Desktop\NET task\MeetingsManagerTests\Data\MeetingsData.json";
        [SetUp]
        public void CreateManager()
        {
            manager = new Manager(filePath);
        }

        [Test]
        public void Getting_New_Date()
        {
            //ARRANGE
            DateTime dateTest = new DateTime(2022, 12, 12, 17, 0, 0);

            //ACT
            var date = manager.GetDate("12/12/2022 5:00 PM");

            //ASSET
            Assert.AreEqual(dateTest, date);
        }

        [Test]
        public void Getting_Data_From_File()
        {
            //ARRANGE        
            manager.GetDataFromFile(filePath);

            //ASSET            
            Assert.IsNotNull(manager.Meetings);
        }

        [Test]
        public void Getting_Meetings_Data()
        {
            //ARRANGE   

            //ACT
            manager.GetDataFromFile(filePath);

            //ASSET
            Assert.IsNotNull(manager.Meetings);
        }
        //Time timit reached

    }
}