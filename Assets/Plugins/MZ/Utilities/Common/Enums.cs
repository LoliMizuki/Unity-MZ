using System;

public static partial class MZ {

    public class Enums {

        public static E EnumFromString<E>(string str) {
            foreach (E e in (E[])Enum.GetValues(typeof(E))) {
                if (str.ToLower() == e.ToString().ToLower()) {
                    return e;
                }
            }
        
            MZ.Debugs.Log("can not parse from string '" + str + "' to type " + typeof(E).ToString());
            return default(E);
        }

        public static E[] AllEnums<E>() {
            return (E[])Enum.GetValues(typeof(E));
        }
    }
}