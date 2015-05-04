#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MZEventsExcutor))]
public class MZEventsExcutorInspector : Editor {

	public override void OnInspectorGUI() {
        MZ.EditorGUI.LayoutLabelWithDeep("Number: " + _eventsExcutor.events.Count.ToString());
    
        EditorGUILayout.Space();
    
        foreach (MZ.Event e in _eventsExcutor.events) {
            MZ.EditorGUI.LayoutLabelWithDeep("Type: " + e.GetType());
            MZ.EditorGUI.LayoutLabelWithDeep("Passed: " + e.passedTime.ToString(), 1);
        }
    }
    
    
    
    MZEventsExcutor _eventsExcutor = null;
    
    void OnEnable() {
        _eventsExcutor = target as MZEventsExcutor;
    }
}

#endif