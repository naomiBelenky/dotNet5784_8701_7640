namespace DalTest;

using Dal;
using DalApi;
using DO;
using System.Data.SqlTypes;


internal class Program
{
    //private static readonly IDal s_dal = new DalList(); //stage 2
    //static readonly IDal s_dal = new DalXml(); //stage 3
    static readonly IDal s_dal = Factory.Get; //stage 4

    static void Main(string[] args)
    {
        try
        {
            //Initialization.Do(s_dal); //stage 2
            mainMenu();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    //private static ITask? s_dalTask = new TaskImplementation(); //stage 1
    //private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
    //private static ILink? s_dalLink = new LinkImplementation(); //stage 1


    private static void mainMenu()  //the main menu, asking what entity the user wants to do action on
    {
        string? choice;
        do
        {
            Console.WriteLine
            ("enter your choice: \nData initialization\nexit\ntask\nengineer\nlink");
            choice = Console.ReadLine();
            if (choice == "exit")
                return;
            if (choice == "Data initialization")
            {
                Console.Write("Would you like to create Initial data? (Y/N)\n"); //stage 3
                string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3
                if (ans == "Y") //stage 3
                                //Initialization.Do(s_dal);
                    Initialization.Do(); //stage 4
                continue;
            }

            secondMenu(choice);

        } while (choice != "exit");
    }

    private static void secondMenu(string? choice)  //the second menu, asking what action does the user want to do on the entity he chose
    {
        string? act;
        Console.WriteLine
            ($"enter your choice:\nexit\nadd {choice}\nread {choice}\nread all {choice}\nupdate {choice}\ndelete {choice}");
        act = Console.ReadLine();

        switch (act)
        {
            case "exit": return;
            case "add": add(choice); break;
            case "read": read(choice); break;
            case "read all": readAll(choice); break;
            case "update": update(choice); break;
            case "delete": delete(choice); break;
        }
    }

    #region methods for second menu
    private static void add(string? choice) //adding a new entity the the list
    {
        if (choice == "task")
        {
            string? name, description, product, notes;
            DateTime? planToStart, deadLine;
            TimeSpan? timeForTask;
            int id;

            Console.WriteLine("write task name");
            name = Console.ReadLine();
            Console.WriteLine("write description");
            description = Console.ReadLine();
            Console.WriteLine("enter plan to start");
            planToStart = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("enter deadline");
            deadLine = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("write product");
            product = Console.ReadLine();
            Console.WriteLine("write notes");
            notes = Console.ReadLine();
            Console.WriteLine("enter engineer ID");
            id = int.Parse(Console.ReadLine());
            Console.WriteLine("enter time for task");
            timeForTask = TimeSpan.Parse(Console.ReadLine());
            Level difficulty = (Level)int.Parse(Console.ReadLine());

            DO.Task task = new DO.Task(0, name, description, difficulty, false, DateTime.Now,
                planToStart, null, timeForTask, deadLine, null, product, notes, id);

            Console.WriteLine(s_dal.Task.Create(task));
        }

        if (choice == "engineer")
        {
            Console.WriteLine("enter ID");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("enter full name");
            string name = Console.ReadLine();
            Console.WriteLine("enter email");
            string email = Console.ReadLine();
            Console.WriteLine("enter engineer level");
            Level level = (Level)int.Parse(Console.ReadLine());
            Console.WriteLine("please enter cost per hour");
            double costPerHour = double.Parse(Console.ReadLine());

            DO.Engineer newEngineer = new DO.Engineer(id, name, email, level, costPerHour);

            Console.WriteLine(s_dal.Engineer.Create(newEngineer));
        }

        if (choice == "link")
        {
            Console.WriteLine("enter previous task ID");
            int prevTask = int.Parse(Console.ReadLine());

            Console.WriteLine("enter the depended task ID");
            int newTask = int.Parse(Console.ReadLine());

            DO.Link newLink = new DO.Link(0, prevTask, newTask);

            Console.WriteLine(s_dal.Link.Create(newLink));

        }
    }
    private static void read(string? choice)    //printing an object by its id
    {
        Console.WriteLine("enter ID");
        int id = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "task": Console.WriteLine(s_dal.Task.Read(id)); break;
            case "engineer": Console.WriteLine(s_dal.Engineer.Read(id)); break;
            case "link": Console.WriteLine(s_dal.Link.Read(id)); break;
            default: Console.WriteLine("ERROR"); break;
        }


    }
    private static void readAll(string? choice) //printing the whole list of the entity the user chose
    {
        switch (choice)
        {
            case "task":
                foreach (var item in s_dal.Task.ReadAll())
                    Console.WriteLine(item);
                break;
            case "engineer":
                foreach (var item in s_dal.Engineer.ReadAll())
                    Console.WriteLine(item);
                break;
            case "link":
                foreach (var item in s_dal.Link.ReadAll())
                    Console.WriteLine(item);
                break;
            default: Console.WriteLine("ERROR"); break;
        }
    }
    private static void update(string? choice)  //updating one object by its id
    {
        Console.WriteLine($"enter {choice} ID");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("enter fields to update");

        switch (choice)
        {
            case "task":
                Console.WriteLine("write task name");
                string? name = Console.ReadLine();
                Console.WriteLine("describe the task");
                string? description = Console.ReadLine();
                DateTime? creation = DateTime.Today;
                Console.WriteLine("on what date do you plan to start?");
                DateTime? planToStart = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("what is the deadline of the task?");
                DateTime? deadLine = Convert.ToDateTime(Console.ReadLine());
                TimeSpan? timeForTask = null;
                Console.WriteLine("describe the product");
                string? product = Console.ReadLine();
                Console.WriteLine("if you have any notes about the task, write them here");
                string? notes = Console.ReadLine();
                Console.WriteLine("enter the engineer id");
                int engineerId = int.Parse(Console.ReadLine());
                Console.WriteLine("enter task difficulty");
                Level difficulty = (Level)int.Parse(Console.ReadLine());

                DO.Task newTask = new DO.Task(id, name, description, difficulty, false, DateTime.Now,
                    planToStart, null, timeForTask, deadLine, null, product, notes, id);

                s_dal.Task.Update(newTask);
                break;
            case "engineer":
                Console.WriteLine("enter full name");
                string? engName = Console.ReadLine();
                Console.WriteLine("enter email");
                string? email = Console.ReadLine();
                Console.WriteLine("enter engineer level");
                Level level = (Level)int.Parse(Console.ReadLine());
                Console.WriteLine("enter engineer cost per hour");
                double costPerHour = double.Parse(Console.ReadLine());
                DO.Engineer newEngineer = new Engineer(id, engName, email, level, costPerHour);
                s_dal.Engineer.Update(newEngineer);
                break;
            case "link":
                Console.WriteLine("enter previous task ID");
                int prevTask = int.Parse(Console.ReadLine());

                Console.WriteLine("enter the depended task ID");
                int dependedTask = int.Parse(Console.ReadLine());

                DO.Link newLink = new DO.Link(id, prevTask, dependedTask);
                s_dal.Link.Update(newLink);
                break;

        }
    }
    private static void delete(string? choice)  //deleting one obgect by its id
    {
        Console.WriteLine("enter id");
        int id = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case "task": s_dal.Task.Delete(id); break;
            case "engineer": s_dal.Engineer.Delete(id); break;
            case "link": s_dal.Link.Delete(id); break;
            default: Console.WriteLine("ERROR"); break;
        }
    }
}
#endregion
