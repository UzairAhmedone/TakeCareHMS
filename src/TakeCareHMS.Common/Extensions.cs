using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TakeCareHMS;

public static class Extensions
{
    public static string GetEnumDescription(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        if (fi != null)
        {
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
        }

        return value.ToString(); // fallback if no description is found
    }
}
