#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MZEventsExcutor))]
public class MZEventsExcutorInspector : Editor {

	public override void OnInspectorGUI() {
        MZ.EditorGUIs.LayoutLabelWithIndentDepth("Number: " + _eventsExcutor.events.Count.ToString());
    
        EditorGUILayout.Space();
    
        foreach (MZ.Actions.ActionBase e in _eventsExcutor.events) {
            MZ.EditorGUIs.LayoutLabelWithIndentDepth("Type: " + e.GetType());
            MZ.EditorGUIs.LayoutLabelWithIndentDepth("Passed: " + e.passedTime.ToString(), 1);
        }
    }
    
    
    
    MZEventsExcutor _eventsExcutor = null;
    
    void OnEnable() {
        _eventsExcutor = target as MZEventsExcutor;
    }
}

#endif