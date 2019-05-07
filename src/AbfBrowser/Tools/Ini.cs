using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public static class Ini
    {
        public static Dictionary<string, string> IniFromObject(object obj)
        {
            Dictionary<string, string> valuesByObjectName = new Dictionary<string, string>();
            foreach (System.Reflection.FieldInfo fieldInfo in obj.GetType().GetFields())
            {
                System.Object fieldValue = fieldInfo.GetValue(obj);
                if (fieldValue == null)
                    valuesByObjectName.Add(fieldInfo.Name, "");
                else
                    valuesByObjectName.Add(fieldInfo.Name, fieldValue.ToString());
            }
            return valuesByObjectName;
        }
    }
}
