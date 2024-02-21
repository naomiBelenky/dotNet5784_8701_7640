using System.Reflection;

namespace BO;

static internal class Tools
{
    public static string ToStringProperty<T>(T t)
    {
        string str = "";
        PropertyInfo[] T_properties = t.GetType().GetProperties();


        foreach (var item in T_properties)
        {
            object value = item.GetValue(t, null);
            if (value is IList<BO.TaskInList> list)
            {
                str += item.Name + ":\n";
                foreach (var listItem in list)
                {
                    str += "- " + listItem + "\n";
                }
            }
            else
            {
                str += item.Name + ": " + value + '\n';
            }
        }
        return str;

        //foreach (var item in T_properties)
        //    str += item.Name + ": " + item.GetValue(t, null) + '\n';

        //return str;
    }
}
