using System.Collections.Generic;

public static partial class MZ {

    public class Collections {
    
        public static string ElemnetsStringInList<T>(List<T> list) { 
            string s = "";
            foreach (T e in list) s += e.ToString() + ",";

            return ForamtListString(s, "[", "]");
        }
        
		public static void LogElemnetsInList<T>(List<T> list) {
			MZ.Debugs.Log(ElemnetsStringInList<T>(list));
		}
        
        public static string ForamtListString(string listString, string leftCloseSymbol, string rightCloseSymbol) {
            int lastIndexOfComma = (listString.LastIndexOf(",") >= 0)? listString.LastIndexOf(",") : 0;
            return leftCloseSymbol + listString.Substring(0, lastIndexOfComma) + rightCloseSymbol;
        }
    }
}