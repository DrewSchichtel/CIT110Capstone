using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIT110Capstone
{
    // **************************************************
    // Title: CIT-110 Capstone Project
    // Description: Allows user to save a list of employees along with their rate of pay. Also, punch in and out employees while the program keeps a sum of weekly hours.
    // Author: Drew Schichtel
    // Dated Created: 12/08/2019
    // Last Modified: 12/09/2019
    // **************************************************
    class Program
    {
        static void Main(string[] args)
        {
            //
            // initialize employee list from data file and send to main menu
            //
            string dataPath = @"Data\EmployeeList.txt";
            List<Employee> employees = InitializeEmployeeList(dataPath);
            DisplayWelcomeScreen("Payroll Application");
            DisplayMainMenu(employees, dataPath);

        }


        /// <summary>
        /// Main Menu
        /// </summary>
        /// <param name="employees"></param>
        static void DisplayMainMenu(List<Employee> employees, string dataPath)
        {
            bool quitApplication = false;
            string menuChoice;
            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("a) Punch In");
                Console.WriteLine("b) Punch Out");
                Console.WriteLine("c) View Empoloyee Info");
                Console.WriteLine("d) Add Employee");
                Console.WriteLine("e) Remove Employee");
                Console.WriteLine("f) View Weekly Hours");
                Console.WriteLine("g) Reset Week");
                Console.WriteLine("q) Quit");
                Console.Write("Enter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        PunchIn(employees);
                        break;

                    case "b":
                        PunchOut(employees);
                        break;

                    case "c":
                        DisplayEmployeeInfo(employees);
                        break;

                    case "d":
                        AddEmployee(employees);
                        break;

                    case "e":
                        RemoveEmployee(employees);
                        break;

                    case "f":
                        DisplayWeeklyHours(employees);
                        break;

                    case "g":
                        ResetWeek(employees);
                        break;

                    case "q":
                        quitApplication = true;
                        SaveEmployeeList(employees, dataPath);
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitApplication);
        }

        #region Methods

        /// <summary>
        /// allows user to choose an employee to display info on
        /// </summary>
        static void DisplayEmployeeInfo(List<Employee> employees)
        {
            string[] names = new string[employees.Count];
            DisplayScreenHeader("Employee Detail");

            //
            // list employees
            //
            Console.WriteLine("\tEmployee Names");
            Console.WriteLine("\t-------------");
            foreach (Employee employee in employees)
            {
                Console.Write(employee.Name + ",");
            }

            Console.WriteLine();
            Console.WriteLine("Which employee would you like info on?");
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string employeeName = Console.ReadLine(); 

            //
            // get and validate employee name then display info
            //

            for (int i = 0; i < employees.Count; i++)
            {
                names[i] = employees[i].Name;
            }

            Employee selectedEmployee = null;

            if (names.Contains(employeeName))
            {
                foreach (Employee employee in employees)
                {
                    if (employee.Name == employeeName)
                    {
                        selectedEmployee = employee;
                        break;
                    }
                }

                Console.WriteLine($"\tName: {selectedEmployee.Name}");
                Console.WriteLine($"\tAge: {selectedEmployee.Rate}");
                Console.WriteLine($"\tHour Total: {selectedEmployee.Hours}");
                Console.WriteLine($"\tLast Punch In: {selectedEmployee.TimeIn}");

            }
            else
            {
                Console.WriteLine("Employee not found. Try Again");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Allows user to remove an employee from save file
        /// </summary>
        static void RemoveEmployee(List<Employee> employees)
        {
            string[] names = new string[employees.Count];
            DisplayScreenHeader("Remove Employee");

            Console.WriteLine("\tEmployees");
            foreach (Employee employee in employees)
            {
                Console.Write(employee.Name + ",");
            }

            Console.WriteLine();
            Console.WriteLine("Which employee would you like to remove?");
            Console.WriteLine();
            Console.Write("Enter name:");
            string employeeName = Console.ReadLine();

            for (int i = 0; i < employees.Count; i++)
            {
                names[i] = employees[i].Name;
            }

            Employee selectedEmployee = null;

            if (names.Contains(employeeName))
            {
                foreach (Employee employee in employees)
                {
                    if (employee.Name == employeeName)
                    {
                        selectedEmployee = employee;
                        break;
                    }
                }
                employees.Remove(selectedEmployee);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedEmployee.Name} has been removed");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Employee not found. Try Again");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Allows user to add employee to the save file
        /// </summary>
        static void AddEmployee(List<Employee> employees)
        {
            Employee newEmployee = new Employee();
            DisplayScreenHeader("Add Employee");

            //
            // get new employee properties and give default hours and last punch times
            // 

            Console.Write("Name: ");
            newEmployee.Name = Console.ReadLine();
            Console.Write("Rate: ");
            double.TryParse(Console.ReadLine(), out double rate);
            newEmployee.Rate = rate;
            newEmployee.Hours = 0;
            newEmployee.TimeIn = DateTime.Now;

            employees.Add(newEmployee);
            Console.WriteLine(newEmployee.Name + " has been added");
            Console.WriteLine();
            DisplayContinuePrompt();
        }

        /// <summary>
        /// allows user to display weekly hour totals for all employees
        /// </summary>
        static void DisplayWeeklyHours(List<Employee> employees)
        {
            DisplayScreenHeader("Weekly Totals");
            Console.WriteLine();
            foreach (Employee employee in employees)
            {
                Console.WriteLine(employee.Name + ":" + employee.Hours.ToString("f") + ", " + (employee.Hours * employee.Rate).ToString("c"));
                Console.WriteLine("-----------------");
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// allows user to reset weekly hour totals of all employees
        /// </summary>
        static void ResetWeek(List<Employee> employees)
        {

            DisplayScreenHeader("Reset Weekly Hours");
            Console.WriteLine();
            Console.WriteLine("Are you sure you would like to reset weekly hour totals? yes/no");
            string input = Console.ReadLine().ToLower();

            if (input == "yes")
            {
                foreach (Employee employee in employees)
                {
                    employee.Hours = 0;
                }
                Console.WriteLine("Weekly hour totals have been reset");
            }
            else
            {
                Console.WriteLine("Weekly hour totals have not been reset");
            }

            DisplayContinuePrompt();
            SaveEmployeeList(employees, @"Data\EmployeeList.txt");
        }

        /// <summary>
        /// allows user to punch out employee
        /// </summary>
        static void PunchOut(List<Employee> employees)
        {
            double dailyHours = 0;
            string[] names = new string[employees.Count];
            DisplayScreenHeader("Punch Out");

            Console.WriteLine("\tEmployees");
            Console.WriteLine("\t-------------");
            foreach (Employee employee in employees)
            {
                Console.Write(employee.Name + ",");
            }

            Console.WriteLine();
            Console.WriteLine("Which employee would you like to punch out?");
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string employeeName = Console.ReadLine();

            for (int i = 0; i < employees.Count; i++)
            {
                names[i] = employees[i].Name;
            }

            Employee selectedEmployee = null;

            if (names.Contains(employeeName))
            {
                foreach (Employee employee in employees)
                {
                    if (employee.Name == employeeName)
                    {
                        selectedEmployee = employee;
                        break;
                    }
                }

                dailyHours = (DateTime.Now - selectedEmployee.TimeIn).TotalHours;
                selectedEmployee.Hours = selectedEmployee.Hours + dailyHours;

            }
            else
            {
                Console.WriteLine("Employee not found. Try Again");
            }

            DisplayContinuePrompt();
            SaveEmployeeList(employees, @"Data\EmployeeList.txt");
        }

        /// <summary>
        /// allows user to punch in employee
        /// </summary>
        static void PunchIn(List<Employee> employees)
        {
            string[] names = new string[employees.Count];
            DisplayScreenHeader("Punch In");

            Console.WriteLine("\tEmployees");
            Console.WriteLine("\t-------------");
            foreach (Employee employee in employees)
            {
                Console.Write(employee.Name + ",");
            }

            Console.WriteLine();
            Console.WriteLine("Which employee would you like to punch in?");
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string employeeName = Console.ReadLine();

            for (int i = 0; i < employees.Count; i++)
            {
                names[i] = employees[i].Name;
            }

            Employee selectedEmployee = null;

            if (names.Contains(employeeName))
            {
                foreach (Employee employee in employees)
                {
                    if (employee.Name == employeeName)
                    {
                        selectedEmployee = employee;
                        break;
                    }
                }
                selectedEmployee.TimeIn = DateTime.Now;
            }
            else
            {
                Console.WriteLine("Employee not found. Try Again");
            }

            DisplayContinuePrompt();
            SaveEmployeeList(employees, @"Data\EmployeeList.txt");
        }
        #endregion


        #region Helpers

        /// <summary>
        /// gets employee list from data file on startup
        /// </summary>
        static List<Employee> InitializeEmployeeList(string dataPath)
        {           
            List<Employee> employees = new List<Employee>();

            string[] EmployeeStrings = File.ReadAllLines(dataPath);
            foreach (string employee in EmployeeStrings)
            {
                //
                // create and add employee to list from file
                //
                string[] employeeProperties = employee.Split(',');

                Employee employee1 = new Employee();

                employee1.Name = employeeProperties[0];

                double.TryParse(employeeProperties[1], out double rate);
                employee1.Rate = rate;

                double.TryParse(employeeProperties[2], out double hours);
                employee1.Hours = hours;

                DateTime.TryParse(employeeProperties[3], out DateTime timeIn);

                employees.Add(employee1);

            }

            return employees;
        }

        /// <summary>
        /// Saves the list of employees after big changes and on exit
        /// </summary>
        static void SaveEmployeeList(List<Employee> employees, string dataPath)
        {
            string[] employeeStrings = new string[employees.Count];

            for (int i = 0; i < employees.Count; i++)
            {
                string employeeString = employees[i].Name + "," + employees[i].Rate + "," + employees[i].Hours + "," + employees[i].TimeIn;

                employeeStrings[i] = employeeString;
            }

            File.WriteAllLines(dataPath, employeeStrings);
            DisplayScreenHeader("Saving Employee List");
            Console.WriteLine();
            Console.WriteLine("Employee list has been saved");
            Console.WriteLine();
            DisplayContinuePrompt();
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen(string Header)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"\t\t{Header}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\tBy: Drew Schichtel");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the application");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }
        #endregion
    }
}
