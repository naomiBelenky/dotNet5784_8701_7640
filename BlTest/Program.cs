using BlApi;
using BO;
using DalApi;

namespace BlTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        static void Main(string[] args)
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();

            string? choice;
            do
            {
                Console.WriteLine("enter your choice: \nexit\ntask\nengineer\nautomaticSchedule\n");
                choice = Console.ReadLine();
                if (choice == "exit")
                    return;
                try
                {
                    switch (choice)
                    {
                        case "task": taskMenu(); break;
                        case "engineer": engineerMenu(); break;
                        case "automaticSchedule": s_bl.automaticSchedule(); break;
                    }
                }
                catch (BO.BlAlreadyExistsException messege)
                {
                    printEx(messege);
                }
                catch (BO.BlDoesNotExistException messege)
                {
                    printEx(messege);
                }
                catch (BO.BlForbiddenInThisStage messege)
                {
                    printEx(messege);
                }
                catch (BO.BlInformationIsntValid messege)
                {
                    printEx(messege);
                }
                catch (BO.BlXMLFileLoadCreateException messege)
                {
                    printEx(messege);
                }
               

            } while (choice != "exit");

            DateTime startOfProject = DateTime.Parse(getString("enter date of start of the project"));
            DalApi.Factory.Get.saveStartandFinishDatestoFile("data-config.xml", "startDate", startOfProject);
            s_bl.automaticSchedule();
        }

        private static void taskMenu()
        {

            string? choice;


            Console.WriteLine("enter your choice: \nadd\ndelete\nread\nread all\nupdate\nupdate date\nmain menu");
            choice = Console.ReadLine();
            switch (choice)
            {
                //?
                case "add":

                    int id = int.Parse(getString("enter id"));
                    string? name = (getString("enter Name")), descreption = getString("enter descreption"),
                        product = getString("enter ptodict"), notes = getString("enter notes");
                    TimeSpan? duration = TimeSpan.Parse(getString("enter duration"));
                    //duratuin
                    Level difficulty = (Level)int.Parse(getString("enter level"));

                    List<BO.TaskInList> prevTasks = new List<BO.TaskInList>();

                    int tempID = int.Parse(getString("enter id of previous task or endl"));
                    while (tempID != null)
                    {
                        BO.TaskInList temp = new BO.TaskInList()
                        {
                            Id = tempID,
                            Description = s_bl.Task.Read(tempID).Description,
                            Name = s_bl.Task.Read(tempID).Name,
                            Status = (BO.Status)s_bl.Task.Read(tempID).Status
                        };
                        prevTasks.Add(temp);
                        tempID = int.Parse(getString("enter id of previous task or endl"));
                    }

                    BO.Task task = new BO.Task()
                    {
                        Id = id,
                        Name = name,
                        Description = descreption,
                        Creation = DateTime.Now,
                        Status = BO.Status.Unscheduled,
                        Links = prevTasks,

                        Duration = duration,
                        Product = product,
                        Notes = notes,
                        Difficulty = difficulty
                    };


                    int newID = s_bl.Task.Add(task);
                    Console.WriteLine(newID);
                    break;
                case "delete":
                    int idToDelete = int.Parse(getString("enter the id of the task to delete"));
                    s_bl.Task.Delete(idToDelete);
                    s_bl.Task.Read(idToDelete); //התוצאה צריכה להיות שגיאה שנתפסת
                    break;
                case "read":
                    int idToRead = int.Parse(getString("enter the id of the task to read"));
                    Console.WriteLine(s_bl.Task.Read(idToRead));
                    break;
                ///??
                case "read all":
                    foreach (var item in s_bl.Task.ReadAll())
                        Console.WriteLine(item);

                    break;
                case "update":
                    id = int.Parse(getString("enter id"));
                    name = (getString("enter Name")); descreption = getString("enter descreption");
                    product = getString("enter ptodict"); notes = getString("enter notes");
                    duration = TimeSpan.Parse(getString("enter duration"));
                    //duratuin
                    difficulty = (Level)int.Parse(getString("enter level"));

                    prevTasks = new List<BO.TaskInList>();

                    tempID = int.Parse(getString("enter id of previous task or endl"));
                    while (tempID != null)
                    {
                        BO.TaskInList temp = new BO.TaskInList()
                        {
                            Id = tempID,
                            Description = s_bl.Task.Read(tempID).Description,
                            Name = s_bl.Task.Read(tempID).Name,
                            Status = (BO.Status)s_bl.Task.Read(tempID).Status
                        };
                        prevTasks.Add(temp);
                        tempID = int.Parse(getString("enter id of previous task or endl"));
                    }

                    BO.Task taskToUpdate = new BO.Task()
                    {
                        Id = id,
                        Name = name,
                        Description = descreption,
                        Creation = DateTime.Now,
                        Status = BO.Status.Unscheduled,
                        Links = prevTasks,

                        Duration = duration,
                        Product = product,
                        Notes = notes,
                        Difficulty = difficulty
                    };

                    s_bl.Task.Update(taskToUpdate);
                    //check:
                    Console.WriteLine(s_bl.Task.Read(taskToUpdate.Id));
                    break;
                case "update date":
                    id = int.Parse(getString("enter id of task to update date"));
                    DateTime date = DateTime.Parse(getString("enter the new date"));
                    s_bl.Task.UpdateDate(id, date);
                    //check:
                    Console.WriteLine(s_bl.Task.Read(id));
                    break;
                case "main menu": return;
                default:
                    {
                        Console.WriteLine("doesnt valid input, please enter again");
                        choice = Console.ReadLine();
                        break;
                    }
            }

            return;

        }
        private static void engineerMenu()
        {
            string? choice;

            Console.WriteLine("enter your choice: \nadd\ndelete\nread\nread all\nupdate\nmain menu");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "add":
                    BO.Engineer engineer = new BO.Engineer()
                    {
                        Id = int.Parse(getString("enter id\n")),
                        Name = getString("enter name"),
                        Email = getString("enter email"),
                        Level = (Level)int.Parse(getString("enter level")),
                        Cost = double.Parse(getString("enter cost"))
                        //TaskInEngineer?
                    };
                    Console.WriteLine(s_bl.Engineer.Add(engineer));
                    break;
                case "delete":
                    int id = int.Parse(getString("enter id to delete"));
                    s_bl.Engineer.Delete(id);
                    //check
                    s_bl.Engineer.Read(id);
                    break;
                case "read":
                    id = int.Parse(getString("enter id to read"));
                    Console.WriteLine(s_bl.Engineer.Read(id));
                    break;
                case "read all":
                    foreach (var item in s_bl.Engineer.ReadAll())
                        Console.WriteLine(item);
                    break;
                case "update":
                    BO.Engineer eng = new BO.Engineer()
                    {
                        Id = int.Parse(getString("enter id\n")),
                        Name = getString("enter name"),
                        Email = getString("enter email"),
                        Level = (Level)int.Parse(getString("enter level")),
                        Cost = double.Parse(getString("enter cost"))
                        //TaskInEngineer?
                    };
                    s_bl.Engineer.Update(eng);
                    //check:
                    s_bl.Engineer.Read(eng.Id);
                    break;
                case "main menu": return;
                default:
                    {
                        Console.WriteLine("doesnt valid input, please enter again");
                        choice = Console.ReadLine();
                        return;
                    }
            }

            return;
        }


        private static string getString(string s)
        {
            Console.WriteLine(s);
            return Console.ReadLine();
        }

        public static void printEx(Exception messege)
        {
            if (messege.InnerException != null)
                Console.WriteLine("Dal Exeption:\n");
            Console.WriteLine(messege.GetType() + "\n");
            Console.WriteLine(messege + "\n");
        }


    }


}





/*Would you like to create Initial data? (Y/N)Y
enter your choice:
exit
task
engineer
task
enter your choice:
add
delete
read
read all
update
update date
main menu
read
enter the id of the task to read
1
BO.Task
enter your choice:
exit
task
engineer
task
enter your choice:
add
delete
read
read all
update
update date
main menu
delete
enter the id of the task to delete
1
Stack overflow.
Repeat 19250 times:
--------------------------------
   at BlImplementation.Bl.get_StageOfProject()
--------------------------------
   at BlImplementation.TaskImplementation.Delete(Int32)
   at BlTest.Program.taskMenu()
   at BlTest.Program.Main(System.String[])
*/
