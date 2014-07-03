using System;

static public partial class MZ {

    public class Enums {

        static public E EnumFromString<E>(string str) {
            foreach (E e in (E[])Enum.GetValues(typeof(E))) {
                if (str.ToLower() == e.ToString().ToLower()) {
                    return e;
                }
            }
        
            MZ.Debugs.Log("can not parse from string '" + str + "' to type " + typeof(E).ToString());
            return default(E);
        }

        static public E[] AllEnums<E>() {
            return (E[])Enum.GetValues(typeof(E));
        }
    }
}