using UnityEngine;
using System;
using System.Collections;

public static partial class MZ {

    public class Time {

        public enum UpdateMode {
            DeltaTimeBase, 
            FrameBase,
        }

        public static float timeScale = 1;

        public static float stepTime { get { return instance._stepTimeFunc(); } }

        public static UpdateMode updateMode { 
            get { return instance._updateMode; } 
            set { instance.SetUpdateMode(value); } 
        }

        #region - private 

        static Time instance {
            get {
                if (_instance == null) {
                    _instance = new Time();
                    _instance.SetUpdateMode(UpdateMode.FrameBase);
                }

                return _instance;
            }
        }

		static MZ.Time _instance;
        
        UpdateMode _updateMode;
        
        Func<float> _stepTimeFunc;
        
        float _fixedDeltaTimeOfFrameBaseMode;

        void SetUpdateMode(UpdateMode updateMode) {
            _updateMode = updateMode;

            switch(_updateMode) {
            case UpdateMode.FrameBase:
                _fixedDeltaTimeOfFrameBaseMode = 1.0f/(float)Application.targetFrameRate;
                _stepTimeFunc = () => {
                    return _fixedDeltaTimeOfFrameBaseMode; };
                break;

            case UpdateMode.DeltaTimeBase:
                _stepTimeFunc = () => {
                    return UnityEngine.Time.deltaTime*timeScale; };
                break;
            }
        }

       #endregion
    }
}