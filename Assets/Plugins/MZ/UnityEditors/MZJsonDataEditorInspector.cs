#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;

// add below
// [CustomEditor(typeof(xxx))]
public class MZJsonDataEditorInspector<EDITOR_TYPE, DATA_TYPE> : Editor 
	where DATA_TYPE : MZ.JsonData, new()
	where EDITOR_TYPE : MZJsonDataEditor<DATA_TYPE> {
	
    public EDITOR_TYPE editor { get; internal set; }
    
    protected virtual bool needLayoutFileMange { get { return true; } }
    
    public override void OnInspectorGUI() {
    	if (Application.isPlaying) return;
    
        if (needLayoutFileMange) LayoutFileMange();
        
		LayoutInspectorGUIInEditorMode();
    }
    
    protected virtual void LayoutFileMange() {
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("New")) editor.New();
        
        if (GUILayout.Button("Open")) editor.Open();
        
        if (GUILayout.Button("Save")) editor.Save();
        
        EditorGUILayout.EndHorizontal();
        
		EditorGUILayout.Space();
    }
    
	protected virtual void LayoutInspectorGUIInEditorMode() {}
    
    
	
	void OnEnable() { editor = target as EDITOR_TYPE; }
}

#endif