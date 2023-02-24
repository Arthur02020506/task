using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace Task;

class Programm
{
    static void Main()
    {
        Console.WriteLine("Enter the path");

        //string filePath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent + "\\" + "Files\\file.txt";
        string filePath;
        do
        {
            filePath = Console.ReadLine();
        }while(filePath == null);

        string[] lines = File.ReadAllLines(filePath);
        
        List<Person> personsList = new List<Person>();
        List<string> wrongData = new List<string>();
        List<string> validations = new List<string>();

         Console.WriteLine("File structure\n");
        for (int i = 0; i < lines.Length; i++)
        {
           
            Console.WriteLine("Line {0}:"+lines[i],i+1);
            string[] item = lines[i].Split(" ");
            if (item.Length == 4 && (item[2] == ":" || item[2] == "-") && item[3].Length == 9)
            {
                Person person = new Person(item[0], item[1], item[2], item[3]);
                personsList.Add(person);
            }
            else
            {
                StringBuilder validationMessage = new StringBuilder();
                wrongData.Add(lines[i]);

                if (item.Length != 4)
                    validationMessage.Append($"In line {i+1}:  are less information\n");//TODO change message
                else
                {
                    if (item[2] != ":" || item[2] != "-")
                        validationMessage.Append($"In line {i+1}:  separator must be ':' or '-'\n");
                    if (item[3].Length != 9)
                        validationMessage.Append($"In line {i+1}: phone number should be with 9 digits\n");
                }

                validations.Add(validationMessage.ToString());
            }
        }

        bool parseResult = false;
        int order = 0;
        int criteria = 0;

        // Read sort ordering.
        while (parseResult == false || (order != 1 && order != 2))
        {
            Console.WriteLine("Please choose an ordering to sort\n 1.Ascending\n 2.Descending\n(choose number)");
            parseResult = int.TryParse(Console.ReadLine(), out order);
        };

        // Read criteria.
        parseResult = false;
        while (parseResult == false || (criteria != 1 && criteria != 2 && criteria != 3))
        {
            Console.WriteLine("Please choose a criteria\n 1.Name\n 2.Surname\n 3.Phone number\n (choose number)");
            parseResult = int.TryParse(Console.ReadLine(), out criteria);
        }

        List<Person> sorted = null;

        switch (criteria)
        {
            case 1:
                {
                    sorted = Sort(personsList, order, new Func<Person, string>(x => x.Name)).ToList();
                    break;
                }
            case 2:
                {
                    sorted = Sort(personsList, order, new Func<Person, string>(x => x.Surname)).ToList();
                    break;
                }
            case 3:
                {
                    sorted = Sort(personsList, order, new Func<Person, string>(x => x.PhoneNumber.Substring(0, 2))).ToList();
                    break;
                }
        }

        Print(sorted, wrongData, validations);
    }
    
    static IEnumerable<Person> Sort(List<Person> items, int order, Func<Person, string> selector)
    {
        if (order == 1)
            return items.OrderBy(selector);

        return items.OrderByDescending(selector);
    }
    
    static void Print(IEnumerable<Person> sorted, List<string> wrong_data, List<string> validations)
    {
        int lines = 0; // TODO remove
        Console.WriteLine("Sorted file's structure\n");

        foreach (Person person in sorted)
        {
            lines++;
            Console.WriteLine(lines + ":" + person.ToString());
        }

        int validationStart = lines;

        foreach (string item in wrong_data)
        {
            lines++;
            Console.WriteLine(lines + ":" + item);
        }

        Console.WriteLine("Validation:\n");

        foreach (string info in validations)
        {
            validationStart++;
            Console.WriteLine(/*validationStart + ":" +*/ info);
        }
    }
}