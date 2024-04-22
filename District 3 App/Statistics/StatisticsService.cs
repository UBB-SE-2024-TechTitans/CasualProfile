using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;
using District_3_App.ProfileSocialNetworkInfoStuff.Entities;
using District_3_App.Repository;

namespace District_3_App.Statistics
{
    public class StatisticsService
    {
        private int timeSpentOnAppToday = 0;
        private int timeSpentOnAppMonthly = 0;
        private int timeSpentOnAppWeekly = 0;

        private Dictionary<User, int> friends = new Dictionary<User, int>();
        private Window mainWindow;
        private string filePath;

        public StatisticsService(string filePath)
        {
            this.mainWindow = Application.Current.MainWindow;

            this.filePath = GenerateDefaultFilePath();
            Console.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                CreateXml(filePath);
            }
            Load(filePath);

            var sortedSequence = friends.OrderByDescending(pair => pair.Value);
            Dictionary<User, int> sortedDictionary = sortedSequence.ToDictionary(pair => pair.Key, pair => pair.Value);

            friends = sortedDictionary;

            SaveStreaksToXML();
            ReadXML();
        }

        private string GenerateDefaultFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Stats.xml");
        }

        public void CreateXml(string filePath)
        {
            XDocument xDocument = new XDocument(new XElement("Friends"));
            xDocument.Save(filePath);
        }
        private void Load(string filePath)
        {
            Console.WriteLine("Reading from file: " + filePath);

            XDocument xDocument = XDocument.Load(filePath);
            XElement root = xDocument.Element("Friends");
            if (!root.HasElements)
            {
                throw new Exception("No friend elements found in the XML file.");
            }

            foreach (var friendElem in root.Elements("Friend"))
            {
                string username = friendElem.Element("Username")?.Value;
                int streak = int.Parse(friendElem.Element("Streak")?.Value ?? "0");

                if (!string.IsNullOrEmpty(username))
                {
                    User user = new User(Guid.NewGuid(), username, string.Empty, string.Empty, DateTime.Now.ToString()); // Fill in appropriate values for other properties
                    friends.Add(user, streak);
                }
            }
        }

        public void SaveStreaksToXML()
        {
            XDocument xDocument = XDocument.Load(filePath);
            XElement root = xDocument.Element("Friends");

            root.Elements("Friend").Remove();

            foreach (var kvp in friends)
            {
                XElement friendElem = new XElement("Friend",
                    new XElement("Username", kvp.Key.Username),
                    new XElement("Streak", kvp.Value));
                root.Add(friendElem);
            }

            xDocument.Save(filePath);
        }

        private static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        private void ReadXML()
        {
            // Read from the XML file
            try
            {
                DateTime dateValue;

                // Load the XML document from the file
                string filePath = "TimeData.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                // Get the root element
                XmlElement root = xmlDoc.DocumentElement;

                // Get all entry elements
                XmlNodeList entryNodes = root.SelectNodes("Entry");

                // Iterate over each entry element
                foreach (XmlNode entryNode in entryNodes)
                {
                    // Get the time and date elements for this entry
                    XmlNode timeNode = entryNode.SelectSingleNode("Time");
                    XmlNode dateNode = entryNode.SelectSingleNode("Date");

                    if (timeNode != null && dateNode != null)
                    {
                        // Parse the time value and add it to the timeSpentOnAppToday variable
                        int timeValue;
                        if (int.TryParse(timeNode.InnerText, out timeValue) && DateTime.TryParse(dateNode.InnerText, out dateValue))
                        {
                            int day = dateValue.Day;
                            int week = GetIso8601WeekOfYear(dateValue);
                            int month = dateValue.Month;

                            if (day == DateTime.Now.Day)
                            {
                                timeSpentOnAppToday += timeValue;
                                timeSpentOnAppWeekly += timeValue;
                                timeSpentOnAppMonthly += timeValue;
                            }
                            else if (week == GetIso8601WeekOfYear(DateTime.Now))
                            {
                                timeSpentOnAppWeekly += timeValue;
                                timeSpentOnAppMonthly += timeValue;
                            }
                            else if (month == DateTime.Now.Month)
                            {
                                timeSpentOnAppMonthly += timeValue;
                            }
                        }

                        // Get the date value
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show($"Error reading time data from XML file: {ex.Message}");
            }
        }

        public List<string> GetFriendNames()
        {
            List<string> friendNames = new List<string>();
            int contor = 5;
            foreach (User user in friends.Keys)
            {
                contor--;
                friendNames.Add(user.Username);
                if (contor == 0)
                {
                    return friendNames;
                }
            }

            return friendNames;
        }
        public List<int> GetUserStreaks()
        {
            List<int> friendStreaks = new List<int>();
            int contor = 5;
            foreach (User user in friends.Keys)
            {
                contor--;
                friendStreaks.Add(friends[user]);
                if (contor == 0)
                {
                    return friendStreaks;
                }
            }

            return friendStreaks;
        }

        public int GetToday()
        {
            return timeSpentOnAppToday;
        }

        public int GetWeek()
        {
            return timeSpentOnAppWeekly;
        }
        public int GetMonth()
        {
            return timeSpentOnAppMonthly;
        }

        public string ConvertSecondsToHMS(int timeSpentOnApp)
        {
            // Calculate hours
            int hours = timeSpentOnApp / 3600;

            // Calculate remaining seconds after removing hours
            int remainingSeconds = timeSpentOnApp % 3600;

            // Calculate minutes
            int minutes = remainingSeconds / 60;

            // Calculate remaining seconds after removing minutes
            int seconds = remainingSeconds % 60;

            // Format the result as hours:minutes:seconds
            return $"{hours:D2}h{minutes:D2}min";
        }
    }
}
