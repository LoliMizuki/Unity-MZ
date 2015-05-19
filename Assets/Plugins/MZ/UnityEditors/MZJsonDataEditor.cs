#if UNITY_EDITOR

using UnityEngine;
using System;
using System.IO;
using System.Collections;

[ExecuteInEditMode]
public class MZJsonDataEditor<DATA_TYPE> : MonoBehaviour where DATA_TYPE : MZ.Datas.JsonDatas.Data, new() {

	public DATA_TYPE data = null;
	
	public virtual void SetData(DATA_TYPE newData = null) {
		data = (newData != null)? newData : new DATA_TYPE();
	}
	
	public virtual void New() { data = new DATA_TYPE(); }
	
	public virtual void Open() {}
	
	public virtual void OpenWithPath(string path) { New(); }
	
	public virtual void Save() {}
	
	protected virtual void AwakeInEditorMode() {}
	
	protected virtual void StartInEditorMode() {}
	
	protected virtual void UpdateInEditorMode() {}
	
	
	
	void Awake() { InvokeOnlyOnEditMode(AwakeInEditorMode); }
	
	void Start() { InvokeOnlyOnEditMode(StartInEditorMode); }
	
	void Update() { InvokeOnlyOnEditMode(UpdateInEditorMode); }
	
	void InvokeOnlyOnEditMode(Action action) {
		if (Application.isPlaying) return;
		if (action != null) action();
	}
}

#endif