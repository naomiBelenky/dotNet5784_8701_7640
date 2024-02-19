

using System.Reflection;

namespace BO;

static internal class Tools
{
    public static string ToStringProperty<T>(T t)
    {
        string str = "";
        PropertyInfo[] T_properties = t.GetType().GetProperties();

        foreach (var item in T_properties)
            str += item.Name + ": " + item.GetValue(t, null) + '\n';

        return str;
    }
}
