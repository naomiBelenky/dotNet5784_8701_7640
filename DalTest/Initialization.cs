namespace DalTest;

using DalApi;
using DO;
using System.Xml.Linq;
using Dal;
using System.Data.Common;

public static class Initialization
{
    private static IDal? s_dal;
    //private static ITask? s_dalTask;
    //private static ILink? s_dalLink;
    //private static IEngineer? s_dalEngineer;

    private static readonly Random s_rand = new();

    private static void createTasks()
    {
        DateTime[] dates = new DateTime[20];    //an array with random dates
        TimeSpan[] duration = new TimeSpan[20];    //an array with random numbers of days
        Random rand = new Random(DateTime.Now.Millisecond);
        for (int i = 0; i < 20; i++)
        {
            int day = rand.Next(1, 30);
            int month = rand.Next(8, 11);
            dates[i] = new DateTime(2023, month, day);

            int days = rand.Next(1, 7);
            duration[i] = new TimeSpan(days, 0, 0, 0);
        }
        Task[] tasks = new Task[20];
        tasks[0] = new Task(0, "Optimization", "Improve, reduce costs", (Level)rand.Next(0, 5), false, dates[0]) with { TimeForTask = duration[0] };
        tasks[1] = new Task(0, "Automation", "Design automated solutions", (Level)rand.Next(0, 5), false, dates[1]) with { TimeForTask = duration[1] };
        tasks[2] = new Task(0, "Simulation", "Virtual models for testing", (Level)rand.Next(0, 5), false, dates[2]) with { TimeForTask = duration[2] };
        tasks[3] = new Task(0, "Algorithm Development", "Craft complex algorithms", (Level)rand.Next(0, 5), false, dates[3]) with { TimeForTask = duration[3] };
        tasks[4] = new Task(0, "Prototyping", "Build early-stage models", (Level)rand.Next(0, 5), false, new DateTime(2023, 07, 01)) with { TimeForTask = duration[4] };
        tasks[5] = new Task(0, "Network Infrastructure", "Manage data communication networks", (Level)rand.Next(0,5), false, dates[5]) with { TimeForTask = duration[5] };
        tasks[6] = new Task(0, "Data Analysis", "Extract insights from datasets", (Level)rand.Next(0, 5), false, dates[6]) with { TimeForTask = duration[6] };
        tasks[7] = new Task(0, "System Integration", "Integrate for improved functionality", (Level)rand.Next(0, 5), false, dates[7]) with { TimeForTask = duration[7] };
        tasks[8] = new Task(0, "Robotics", "Program robotic systems", (Level)rand.Next(0, 5), false, dates[8]) with { TimeForTask = duration[8] };
        tasks[9] = new Task(0, "Quality Assurance", "Ensure product reliability", (Level)rand.Next(0, 5), false, dates[9]) with { TimeForTask = duration[9] };
        tasks[10] = new Task(0, "Security Engineering", "Protect systems, data", (Level)rand.Next(0, 5), false, dates[10]) with { TimeForTask = duration[10] };
        tasks[11] = new Task(0, "Energy Management", "Design efficient energy solutions", (Level)rand.Next(0, 5), false, dates[11]) with { TimeForTask = duration[11] };
        tasks[12] = new Task(0, "Embedded Systems", "Develop dedicated systems", (Level)rand.Next(0, 5), false, dates[12]) with { TimeForTask = duration[12] };
        tasks[13] = new Task(0, "AR Development", "Create AR applications", (Level)rand.Next(0, 5), false, new DateTime(2023, 12, 04)) with { TimeForTask = duration[13] };
        tasks[14] = new Task(0, "UI Design", "Design visual elements", (Level)rand.Next(0, 5), false, dates[14]) with { TimeForTask = duration[14] };
        tasks[15] = new Task(0, "Machine Learning Integration", "Integrate machine learning", (Level)rand.Next(0, 5), false, dates[15]) with { TimeForTask = duration[15] };
        tasks[16] = new Task(0, "Structural Analysis", "Analyze structural integrity", (Level)rand.Next(0, 5), false, dates[16]) with { TimeForTask = duration[16] };
        tasks[17] = new Task(0, "Control Systems Engineering", "Design dynamic control", (Level)rand.Next(0, 5), false, dates[17]) with { TimeForTask = duration[17] };
        tasks[18] = new Task(0, "CAD Modeling", "Create 3D models", (Level)rand.Next(0, 5), false, new DateTime(2023, 07, 25)) with { TimeForTask = duration[18] };
        tasks[19] = new Task(0, "Sustainable Design", "Eco-friendly project design", (Level)rand.Next(0, 5), false, new DateTime(2023, 12, 18)) with { TimeForTask = duration[19] };
 
        for (int i = 0; i < 20; i++)
        {
            s_dal!.Task.Create(tasks[i]);
        }
    }

    private static void createLinks()
    {
        for (int i = 1; i <= 20; i++)  //All the tasks depended in the first task: Prototyping
        {
            if (i == 5)
                continue;
            Link temp = new Link(0, 5, i);
            s_dal!.Link.Create(temp);
        }
        for (int i = 1; i <= 20; i++)   //tasks 5-20 are depended on task #19
        {
            if (i == 5 || i == 19)
                continue;
            Link temp = new Link(0, 19, i);
            s_dal!.Link.Create(temp);
        }

        Link temp1 = new(0, 10, 14);
        //s_dalLink.Create(temp1); //stage 1
        s_dal!.Link.Create(temp1); //stage 2
        Link temp2 = new(0, 10, 20);
        //s_dalLink.Create(temp2);
        s_dal.Link.Create(temp2); //stage 2
        Link temp3 = new(0, 17, 14);
        s_dal.Link.Create(temp3);
        Link temp4 = new(0, 17, 20);
        s_dal.Link.Create(temp4);
        //14th and 20th tasks depended in 10th and 17th tasks
    }

    private static void createEngineers()
    {
        string[] emails = {"aviBuchbut@gmail.com","shiramarket@gmail.com",
            "taloriel@gmail.com","eytanbenisty@gmail.com","danmaz@gmail.com"};
        string[] names = {"Avi Buchbut","Shira Market", "Tal Oriel",
            "Eytan Benisty", "Dan Maztal"};
        Random random = new Random(DateTime.Now.Millisecond);


        for (int i = 0; i < 5; i++)
        {
            int tempID = random.Next(200000000, 400000000);
            Level tempLevel = (Level)random.Next(0, 5);
            double costPerHour = Math.Round((random.NextDouble() * (500 - 30) + 30), 2); //random double number between 30 to 500 with two numbers after the decimal point
            Engineer temp = new Engineer(tempID, names[i], emails[i], tempLevel, costPerHour);
            //s_dalEngineer.Create(temp); //stage 1
            s_dal!.Engineer.Create(temp); //stage 2
        }
    }

    //public static void Do(IDal? dal   /*ITask? dalTask, ILink? dalLink, IEngineer? dalEngineer*/) //stage 3
    public static void Do() //stage 4
    {
        //s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalLink = dalLink ?? throw new NullReferenceException("DAL can not be null!");
        //s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");

        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stage 2
        s_dal = DalApi.Factory.Get; //stage 4

        //resetting the serial numbers to 1 and delete all the antity objects
        Reset(); 
        
        createTasks();       
        createLinks();       
        createEngineers();

        //s_dal.
    }

    public static void Reset()
    {
        //if (XMLTools.LoadListFromXMLElement("dal-config").Element("dal")!.Value == "xml")
        //{
        //    XElement config = XMLTools.LoadListFromXMLElement("data-config");
        //    config.Element("startDate")?.SetValue("");
        //    config.Element("finishDate")?.SetValue("");
        //    XMLTools.SaveListToXMLElement(config, "data-config");
        //}
        DalApi.Factory.Get.StartDate = null;

        DalApi.Factory.Get.Task.DeleteAll();
        Factory.Get.Engineer.DeleteAll();
        DalApi.Factory.Get.Link.DeleteAll();
        //(The check for existing initial data is performed within the function "DeleteAll")
        DateTime? a = DalApi.Factory.Get.StartDate;
    }
}
