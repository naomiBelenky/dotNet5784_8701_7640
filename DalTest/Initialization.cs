﻿namespace DalTest;

using DalApi;
using DO;

public static class Initialization
{
    private static ITask? s_dalTask;
    private static ILink? s_dalLink;
    private static IEngineer? s_dalEngineer;

    private static readonly Random s_rand = new();

    private static void createTasks()
    {
        DateTime[] dates = new DateTime[20];
        for (int i = 0; i < 20; i++)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int day = rand.Next(30);
            int month = rand.Next(8, 11);
            dates[i] = new DateTime(2023, month, day);
        }
        Task[] tasks = new Task[20];
        tasks[0] = new Task(0, "Optimization", "Improve, reduce costs", false, dates[0]);
        tasks[1] = new Task(0, "Automation", "Design automated solutions", false, dates[1]);
        tasks[2] = new Task(0, "Simulation", "Virtual models for testing", false, dates[2]);
        tasks[3] = new Task(0, "Algorithm Development", "Craft complex algorithms", false, dates[3]);
        tasks[4] = new Task(0, "Prototyping", "Build early-stage models", false, new DateTime(2023, 07, 01));
        tasks[5] = new Task(0, "Network Infrastructure", "Manage data communication networks", false, dates[5]);
        tasks[6] = new Task(0, "Data Analysis", "Extract insights from datasets", false, dates[6]);
        tasks[7] = new Task(0, "System Integration", "Integrate for improved functionality", false, dates[7]);
        tasks[8] = new Task(0, "Robotics", "Program robotic systems", false, dates[8]);
        tasks[9] = new Task(0, "Quality Assurance", "Ensure product reliability", false, dates[9]);
        tasks[10] = new Task(0, "Security Engineering", "Protect systems, data", false, dates[10]);
        tasks[11] = new Task(0, "Energy Management", "Design efficient energy solutions", false, dates[11]);
        tasks[12] = new Task(0, "Embedded Systems", "Develop dedicated systems", false, dates[12]);
        tasks[13] = new Task(0, "AR Development", "Create AR applications", false, new DateTime(2023, 12, 04));
        tasks[14] = new Task(0, "UI Design", "Design visual elements", false, dates[14]);
        tasks[15] = new Task(0, "Machine Learning Integration", "Integrate machine learning", false, dates[15]);
        tasks[16] = new Task(0, "Structural Analysis", "Analyze structural integrity", false, dates[16]);
        tasks[17] = new Task(0, "Control Systems Engineering", "Design dynamic control", false, dates[17]);
        tasks[18] = new Task(0, "CAD Modeling", "Create 3D models", false, new DateTime(2023, 07, 25));
        tasks[19] = new Task(0, "Sustainable Design", "Eco-friendly project design", false, new DateTime(2023, 12, 18));

        for (int i = 0; i < 20; i++)
        {
            s_dalTask.Create(tasks[i]);
        }
    }

    private static void createLinks()
    {
        for (int i = 1; i <= 20 && i != 5; i++)  //All the tasks depended in the first task: Prototyping
        {
            Link temp = new Link(0, 5, i);
            s_dalLink.Create(temp);
        }
        for (int i = 1; i <= 20 && i!=5 && i!=19; i++)   //tasks 5-20 are depended on task #19
        {
            Link temp = new Link(0, 19, i);
            s_dalLink.Create(temp);
        }

        Link temp1 = new(0, 10, 14);
        s_dalLink.Create(temp1);
        Link temp2 = new(0, 10, 20);
        s_dalLink.Create(temp2);
        Link temp3 = new(0, 17, 14);
        s_dalLink.Create(temp3);
        Link temp4 = new(0, 17, 20);
        s_dalLink.Create(temp4);
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
            EngineerLevel tempLevel = (EngineerLevel)random.Next(1, 5);
            Engineer temp = new Engineer(tempID, names[i], emails[i], tempLevel);
            s_dalEngineer.Create(temp);
        }
    }

    public static void Do(ITask? dalTask, ILink? dalLink, IEngineer? dalEngineer)
    {
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalLink = dalLink ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");

        createTasks();
        createLinks();
        createEngineers();
    }
}






