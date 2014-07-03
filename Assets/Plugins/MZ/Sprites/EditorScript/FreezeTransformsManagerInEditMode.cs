#if UNITY_EDITOR

using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class FreezeTransformsManagerInEditMode : MonoBehaviour {
    
    struct FreezeTransform {
        public string Name;
        public Transform TargetTransform;
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion LocalRotation;
    }
    
    List<FreezeTransform> _freezeTransforms;
    
    void Awake() {
//        RegisterFreezeTransformWithName("_Main Camera");
//        RegisterFreezeTransformWithName("__Animations");
//        RegisterFreezeTransformWithName("Animations Editor");
//        RegisterFreezeTransformWithName("Sprites");
//        RegisterFreezeTransformWithName("Spritesheets Viewer");
    }
    
    void RegisterFreezeTransformWithName(string name) {
        RegisterFreezeTransformWithTransform(GameObject.Find(name).transform, name);
    }
    
    void RegisterFreezeTransformWithTransform(Transform freezeTransform, string name) {
        if(freezeTransform == null) {
            Debug.Log("Can not found transform with name '" + name + "' to freeze" );
            return;
        }
        
        FreezeTransform ft;
        ft.Name = name;
        ft.TargetTransform = freezeTransform;
        ft.Position = freezeTransform.position;
        ft.Scale = freezeTransform.localScale;
        ft.LocalRotation = freezeTransform.localRotation;
        
        if(_freezeTransforms == null) {
            _freezeTransforms = new List<FreezeTransform>();
        }
        
        _freezeTransforms.Add(ft);
    }
    
    void Update() {
        if(_freezeTransforms == null) {
            return;
        }

        foreach(FreezeTransform ft in _freezeTransforms) {
            if(ft.TargetTransform.position != ft.Position) {
                ft.TargetTransform.position = ft.Position;
            }
            
            if(ft.TargetTransform.localScale != ft.Scale) {
                ft.TargetTransform.localScale = ft.Scale;
            }
            
            if(ft.TargetTransform.localRotation != ft.LocalRotation) {
                ft.TargetTransform.localRotation = ft.LocalRotation;
            }
        }
    }
}

#endif