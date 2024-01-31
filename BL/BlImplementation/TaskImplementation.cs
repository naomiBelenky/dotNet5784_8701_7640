using BlApi;
using System.Security.Cryptography;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public void Add(BO.Task task)
    {
        DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description, (DO.Level)task.Difficulty);
        try
        {
            if (int.IsNegative(task.Id)) throw new BO.BlInformationIsntValid("id is not valid");
            if (task.Name == "") throw new BO.BlInformationIsntValid("name is not valid");
            if (task.Links != null) //if there are dependent tasks, adding them to the list of links
            {
                foreach (BO.TaskInList item in task.Links)
                {
                    DO.Link link = new DO.Link(0, item.Id, task.Id);
                    _dal.Link.Create(link);
                }
            }

            int id = _dal.Task.Create(doTask);  //if the data is valid, creating the task in the data layer
        }
        catch (DO.DalAlreadyExistsException message)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={task.Id} already exists", message);
        }
    }


    public void Delete(int id)
    {

        //checking if there is another task that depended in this task
        DO.Link? tempLink = _dal.Link.Read(item => item.PrevTask == id);
        if (tempLink != null)
            throw new BO.BlDeletionImpossible("Deleting this task is forbidden beacuse there is another task that depended on it");

        try
        {
            //check if 
            DO.Task? tempTask = _dal.Task.Read(id);
            if (tempTask != null)
                if (tempTask.PlanToStart != null)
                    throw new BO.BlForbiddenAfterCreatingSchedule("Deleting is prohibited after the project schedule is created");
                else;
            else
                throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");


            _dal.Task.Delete(id);
        }
        catch (DO.DalDoesNotExistException messege)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist", messege);
        }
    }


    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id) ?? throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        BO.Task task = new BO.Task()
        {
            Id = id,
            Name = doTask.Name,
            Description = doTask.Description,
            Difficulty = (BO.Level)doTask.Difficulty,
            Creation = (DateTime)doTask.Creation!,
            //Status calculated later
            //Links (need to calculate)
            //Milestone (idk)
            PlanToStart = doTask.PlanToStart,
            StartWork = doTask.StartWork,
            //PlanToFinish calculated later
            Deadline = doTask.Deadline,
            FinishDate = doTask.FinishDate,
            Duration = doTask.TimeForTask,
            Product = doTask.Product,
            Notes = doTask.Notes,
            Engineer = new BO.EngineerInTask    //filling the info about the engineer working on the task
            {
                Id = (int)doTask.EngineerID!,
                Name = (_dal.Engineer.Read(id) ??
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist")).FullName
                //if the Read returns an engineer, assigning his name to the EngineerInTask
            }
        };

        task.Status = getStatus(task);
        task.PlanToFinish = getPlanToFinish(task);

        return task;
    }



    public IEnumerable<BO.Task> ReadAll(Func<bool>? filter = null)
    {
        throw new NotImplementedException();
        //if (filter == null)
        //    IEnumerable<BO.Task> tasks = (from DO.Task doTask in _dal.Task.DeleteAll()
        //                                  select new BO.Task()
        //                                  {
        //                                  });
                                          

    } 

    public void Update(BO.Task task)
    {
        //נבדוק שהנתונים תקינים
        if (int.IsNegative(task.Id)) throw new BO.BlInformationIsntValid("id is not valid");
        if (task.Name == "") throw new BO.BlInformationIsntValid("name is not valid");
        
        try
        {
            //בשביל ההמשך, נבדוק כמה תלויות יש לי
            int counterDoLinks = (_dal.Link.ReadAll(link => link.NextTask == task.Id).ToList().Count);
            
            //נבדוק האם הלו"ז כבר הוחלט
            DO.Task? tempTask = _dal.Task.Read(task.Id);
            if (tempTask != null) 
                if (tempTask.PlanToStart != null) 
                {
                    //אם הלוז כבר נקבע נבדוק שעודכנו רק השדות המותרים לעדכון
                    if (task.Id != tempTask.TaskID || (int)task.Difficulty != (int)tempTask.Difficulty ||/*task.Milestone!=tempTask.Milestone||*/
                        task.Creation != tempTask.Creation || task.PlanToStart != tempTask.PlanToStart
                        || task.StartWork != tempTask.StartWork || task.Deadline != tempTask.Deadline
                        || task.FinishDate != tempTask.FinishDate || task.Links != null && task.Links.Count != counterDoLinks) 
                    { throw new BO.BlForbiddenAfterCreatingSchedule("Updating this parameters is prohibited after the project schedule is created"); }

                    //צריך לראות אם רוצים לעדכן משהו בתלויות
                    //שזה אומר: או להוסיף תלות
                    //או למחוק תלות 
                    //אם הוסיפו תלויות אז מספר האיברים ברשימה של המשימה יהיה גדול יותר ממספר התלויות שהמשימה הבאה שלהן היא המשימה שלי
                    //במקרה הזה אני אבין שצריך להוסיף
                    //ואז אצטרך לבדוק מה המשימה שצריך להוסיף 
                    //ואז להוסיף אותה לתלויות,אם מותר
                    //במקרה שצריך למחוק אז מספר האיברים ברשימה יהיה קטן ממספר התלויות עם הת.ז המתאים
                    //ואז אצטרך לחפש מה צריך למחוק 
                    //ואז אמחק אם מותר.
                    //עוד דרך לבדוק אם צריך להוסיף או למחוק: 
                    
                    
                   


                    //if(task.Links != null)
                    //{
                    //    //(from BO.TaskInList link in task.Links
                    //    // select  )
                    //    foreach (var link in task.Links)
                    //    {
                    //        from DO.Link item in _dal.Link.ReadAll(link => link.NextTask == task.Id)
                    //        where item.PrevTask != link.Id
                    //        select item
                    //    }
                    //IEnumerable<DO.Link> temp = (from DO.Link item in _dal.Link.ReadAll(link => link.NextTask == task.Id)
                    //                             where item.PrevTask != task)
                    //}
                     
                    //נבדוק שאין שינוי ברשימת תלויות
                    //לעבור על הליסט
                    //עבור כל אחד מהמשימות שיש שם לחפש אותה בלינקס
                    //אם אין אותה סימן שרצינו לשנות 


                }

            //אם אנחנו פה סימן שכל הבדיקות עברו בהצלחה:)
            //לכן נעדכן 
            //נבדוק האם רוצים להוסיף תלויות
            if (task.Links != null) //צריך לטפל במקרה שבו זה כן שווה לנל זתומרת שאין לי שום תלויות
            {
                if (task.Links.Count > counterDoLinks) //אם אנחנו רוצים להוסיף תלויות 
                {
                    IEnumerable<int> newTasksID = (from BO.TaskInList item in task.Links
                                                   where (_dal.Link.Read(link => link.PrevTask == item.Id && link.NextTask == task.Id) == null)
                                                   select item.Id) ; //רשימת תלויות חדשות

                    foreach (int taskID in newTasksID) //נוסיף את כולן 
                    {
                        DO.Link newLink = new DO.Link(0, taskID, task.Id);
                        _dal.Link.Create(newLink);
                    }
                }

                if (task.Links.Count < counterDoLinks)  //אם צריך למחוק תלויות
                {
                    //נחפש את כל התלויות בנתונים שכבר לא קיימות ברשימה
                    IEnumerable<int> oldLinksID = (from DO.Link item in _dal.Link.ReadAll(link => link.NextTask == task.Id)
                                                   where task.Links.Any(link => link.Id == item.PrevTask) == false
                                                   select item.LinkID);
                    foreach(int linkID in oldLinksID)
                    {
                        _dal.Link.Delete(linkID);
                        //try??? 
                    }
                }
            }

            DO.Task doTask = new DO.Task(task.Id, task.Name, task.Description, (DO.Level)task.Difficulty);
            _dal.Task.Update(doTask);
        }

        catch(DO.DalDoesNotExistException messege)
        {
            throw new BO.BlDoesNotExistException($"Task with ID={task.Id} does Not exist", messege);
        }
    }

    public void UpdateDate(int id, DateTime date)
    {
        throw new NotImplementedException();
    }

    #region private methods for help

    /// <summary>
    /// determines the status of the task
    /// </summary>
    /// <param name="task"> The task we are trying to get the status of </param>
    /// <returns></returns>
    private BO.Status getStatus(BO.Task task)
    {
        BO.Status status = BO.Status.Unscheduled;
        if (task.PlanToStart == null) status = BO.Status.Unscheduled;
        else if (task.StartWork == null) status = BO.Status.Scheduled;
        else if (task.FinishDate == null) status = BO.Status.OnTrack;
        else if (task.FinishDate <= DateTime.Now) status = BO.Status.Done;
        return status;
    }
    /// <summary>
    /// calculates the date that is planned to finish the task
    /// </summary>
    /// <param name="task"> the task we are calculating the date for </param>
    /// <returns></returns>
    private DateTime? getPlanToFinish(BO.Task task)
    {
        return (DateTime?)(task.StartWork + task.Duration);
    }

    private List<BO.TaskInList> getLinks(BO.Task task)
    {
        List<DO.Link?> links = new List<DO.Link?>(_dal.Link.ReadAll());

    }
    #endregion
}
