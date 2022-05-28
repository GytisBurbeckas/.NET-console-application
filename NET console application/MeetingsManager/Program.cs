// See https://aka.ms/new-console-template for more information
using MeetingManager;

/*
"Change filepath in Program.cs to your own.
Otherwise meetings data won't be saved after program is turned off.
Not sure why it's like that. But I noticed that if file path is given with @ and the filepath is absolute, it works.
Otherwise program is trying to find file starting from debug folder.
 */
string filepath = @"C:\Users\LEGION\Desktop\NET console application\MeetingsManager\Data\MeetingsData.json";
var manager = new Manager(filepath);

manager.Start();
