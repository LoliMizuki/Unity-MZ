// editor on script that can splite into many small part, depended on their features.
// this is a generic base class for part component
// override this and write gui draw code on InspectorGUI()

#if UNITY_EDITOR

using UnityEngine;
using System.Collections.Generic;

static public partial class MZ {

    public interface IMZEditorPart<T> {
        T editor { get; }
        Dictionary<string, object> dataDict { get; }
        void Reset();
        void InspectorGUI();
        void SetWithDict(Dictionary<string, object> dict);
        Dictionary<string, object> GetOutputDict();
    }
    
    public class MZEditorPart<T> : IMZEditorPart<T> {
        
        public T editor {
            get {
                return _editor;
            }
        }
        
        public Dictionary<string, object> dataDict { 
            get {
                if(_dataDictCache == null) {
                    _dataDictCache = new Dictionary<string, object>();
                }
                
                return _dataDictCache;
            }
        }
        Dictionary<string, object> _dataDictCache;
        
        public MZEditorPart(T editor) {
            _editor = editor;
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
        
        T _editor;
    }
}

#endif
