using Dal;
using DalApi;
using DO;

namespace DalTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Initialization.Do(s_dalTask, s_dalLink, s_dalEngineer);
                mainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static ILink? s_dalLink = new LinkImplementation(); //stage 1


        private static void mainMenu()
        {
            string? choice;
            do
            {
                Console.WriteLine
                (@"enter your choice:
exit
task
engineer
link");
                choice = Console.ReadLine();
                if (choice == "exit")
                    return;

                secondMenu(choice);

            } while (choice != "exit");
        }

        private static void secondMenu(string? choice)
        {
            string? act;
            Console.WriteLine
                (@$"enter your choice:
exit
add {choice}
read {choice}
readAll {choice}
update {choice}
delete {choice}");
            act = Console.ReadLine();

            switch (act)
            {
                case "exit": return;
                case "add": add(choice); break;
                case "read": read(choice); break;
                case "readAll": readAll(choice); break;
                case "update": update(choice); break;
                case "delete": delete(choice); break;
            }
        }

        private static void add(string? choice)
        {
            if (choice == "task")
            {
                string? name, description, product, notes;
                DateTime? planToStart, deadLine;
                double? timeForTask;
                int id;
                TaskDifficulty level;

                Console.WriteLine("write task name");
                name = Console.ReadLine();
                Console.WriteLine("write description");
                description = Console.ReadLine();
                Console.WriteLine("enter plan to start");
                planToStart = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter deadline");
                deadLine = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("write prosuct");
                product = Console.ReadLine();
                Console.WriteLine("write notes");
                notes = Console.ReadLine();
                Console.WriteLine("enter engineer ID");
                id = int.Parse(Console.ReadLine());
                Console.WriteLine("enter time for task");
                timeForTask = int.Parse(Console.ReadLine());
                TaskDifficulty difficulty = (TaskDifficulty)int.Parse(Console.ReadLine());

                DO.Task task = new DO.Task(0, name, description, false, DateTime.Now,
                    planToStart, null, timeForTask, deadLine, null, product, notes, id, difficulty);

                Console.WriteLine(s_dalTask.Create(task));


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
                EngineerLevel level = (EngineerLevel)int.Parse(Console.ReadLine());
                Console.WriteLine("please enter cost per hour");
                double costPerHour = double.Parse(Console.ReadLine());

                DO.Engineer newEngineer = new DO.Engineer(id, name, email, level, costPerHour);

                Console.WriteLine(s_dalEngineer.Create(newEngineer));
            }

            if (choice == "link")
            {
                Console.WriteLine("enter previous task ID");
                int prevTask = int.Parse(Console.ReadLine());

                Console.WriteLine("enter the depended task ID");
                int newTask = int.Parse(Console.ReadLine());

                DO.Link newLink = new DO.Link(0, prevTask, newTask);

                Console.WriteLine(s_dalLink.Create(newLink));

            }
        }
        private static void read(string? choice)
        {
            Console.WriteLine("enter ID");
            int id = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case "task": Console.WriteLine(s_dalTask.Read(id));  break;
                case "engineer": Console.WriteLine(s_dalEngineer.Read(id)); break;
                case "link": Console.WriteLine(s_dalLink.Read(id)); break;
                default: Console.WriteLine("ERROR"); break;
            }


        }
        private static void readAll(string? choice)
        {
            switch (choice)
            {
                case "task":
                foreach(var item in s_dalTask.ReadAll())
                    Console.WriteLine(item);
                    break;
                case "engineer":
                    foreach(var item in s_dalEngineer.ReadAll())
                        Console.WriteLine(item);
                        break;
                case "link":
                    foreach (var item in s_dalTask.ReadAll())
                        Console.WriteLine(item);
                    break;
                default: Console.WriteLine("ERROR"); break;
            }
        }
        private static void update(string? choice)
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
                    double? timeForTask = null;
                    Console.WriteLine("describe the product");
                    string? product = Console.ReadLine();
                    Console.WriteLine("if you have any notes about the task, write them here");
                    string? notes = Console.ReadLine();
                    Console.WriteLine("enter the engineer id");
                    int engineerId = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter task difficulty");
                    TaskDifficulty difficulty = (TaskDifficulty)int.Parse(Console.ReadLine());

                    DO.Task newTask = new DO.Task(0, name, description, false, DateTime.Now,
                        planToStart, null, timeForTask, deadLine, null, product, notes, id, difficulty);

                    s_dalTask.Update(newTask);
                    break;
                case "engineer":
                    Console.WriteLine("enter ID");
                    int engId = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter full name");
                    string? engName = Console.ReadLine();
                    Console.WriteLine("enter email");
                    string? email = Console.ReadLine();
                    Console.WriteLine("enter engineer level");
                    EngineerLevel level = (EngineerLevel)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter engineer cost per hour");
                    double? costPerHour = double.Parse(Console.ReadLine());
                    DO.Engineer newEngineer = new Engineer(engId, engName, email, level, costPerHour);
                    s_dalEngineer.Update(newEngineer);
                    break;
                case "link":
                    Console.WriteLine("enter previous task ID");
                    int prevTask = int.Parse(Console.ReadLine());

                    Console.WriteLine("enter the depended task ID");
                    int dependedTask = int.Parse(Console.ReadLine());

                    DO.Link newLink = new DO.Link(0, prevTask, dependedTask);
                    s_dalLink.Create(newLink);
                    break;

            }
        }
        private static void delete(string? choice)
        {
            Console.WriteLine("enter id");
            int id = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case "task": s_dalTask.Delete(id); break;
                case "engineer": s_dalEngineer.Delete(id); break;
                case "link": s_dalLink.Delete(id); break;
                default: Console.WriteLine("ERROR"); break;
            }

        }



    }


}



