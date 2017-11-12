using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Scrap_file_from_a_website
{
	class Program
	{
		static List<string> key_lists = new List<string> { }; // Keyword list
		static void Main(string[] args)
		{
			#region Input
			Console.Write("Please type your direction to text file: ");
			string link_dir = Console.ReadLine();
			string tmp;
			do
			{
				Console.Write("Please insert your key word (To stop type 'Done'): ");
				tmp = Console.ReadLine();
				if (tmp != "Done")
					key_lists.Add(tmp);
			} while (tmp != "Done");
			#endregion
				
			// Solution
			FirefoxProfileManager profile_manager = new FirefoxProfileManager();
			FirefoxProfile profile = profile_manager.GetProfile("Selenium"); 
			// "Selenium" is my profile name in Firefox, you need to create something similar otherwise Firefox will use Default profile 
			IWebDriver driver = new FirefoxDriver(profile);
			string line;
			System.IO.StreamReader file = new System.IO.StreamReader(link_dir); //Link to text file
			while ((line = file.ReadLine()) != null) 
				Process(line, driver);
			file.Close();
			driver.Close();
			Console.ReadLine();
		}

		static void Process(string url, IWebDriver driver) {
			driver.Navigate().GoToUrl(url);
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			foreach(string key in key_lists)
				foreach(IWebElement query in driver.FindElements(By.LinkText(key))) 
					query.Click();
			//driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMinutes(2);
		}
	}
}
