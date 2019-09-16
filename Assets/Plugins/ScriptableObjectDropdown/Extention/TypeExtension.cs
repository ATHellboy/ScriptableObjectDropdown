using UnityEngine;

namespace ScriptableObjectDropdown.Extension
{
    public static class TypeExtension
    {
        public static System.Reflection.FieldInfo GetFieldViaPath(this System.Type type, string path)
        {
            System.Type parentType = type;
            System.Reflection.FieldInfo fi = type.GetField(path);
            string[] perDot = path.Split('.');
            foreach (string fieldName in perDot)
            {
                fi = parentType.GetField(fieldName);
                if (fi != null)
                    parentType = fi.FieldType;
                else
                    return null;
            }
            if (fi != null)
                return fi;
            else return null;
        }
    }
}