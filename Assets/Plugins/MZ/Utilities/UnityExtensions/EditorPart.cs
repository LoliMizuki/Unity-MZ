// editor on script that can splite into many small part, depended on their features.
// this is a generic base class for part component
// override this and write gui draw code on InspectorGUI()

#if UNITY_EDITOR

using UnityEngine;
using System.Collections.Generic;

public static partial class MZ {

    public interface IEditorPart<T> {
        T editor { get; }
        
        Dictionary<string, object> dataDict { get; }
        
        void Reset();
        
        void InspectorGUI();
        
        void SetWithDict(Dictionary<string, object> dict);
        
        Dictionary<string, object> GetOutputDict();
    }
    
    public class EditorPart<T> : IEditorPart<T> {
        
		public T editor { get; private set; }
        
		public Dictionary<string, object> dataDict { get; private set; }
        
        public EditorPart(T editor) {
			this.editor = editor;
			dataDict = new Dictionary<string, object>();
        }
        
        public virtual void Reset() {
            dataDict.Clear();
        }
        
        public virtual void InspectorGUI() {
            MZ.Debugs.AssertAlwaysFalse("override me");
        }
        
        public virtual void SetWithDict(Dictionary<string, object> dict) {
            MZ.Debugs.AssertAlwaysFalse("override me");
        }
        
        public virtual Dictionary<string, object> GetOutputDict() {
            MZ.Debugs.AssertAlwaysFalse("override me");
            return null;
        }
    }
}

#endif
