// TODO: 
//      * SetFrameButNotPlay() has issue dynamic setting

using UnityEngine;
using System;
using System.Collections.Generic;
    
[RequireComponent(typeof(SpriteRenderer))]
public class MZAnimation : MonoBehaviour {
    
    public Action<MZAnimation> loopDoneAction;
    public float timeScale = 1.0f;
    public Func<float> stepTimeFunc = () => MZ.Time.stepTime;

    public SpriteRenderer spriteRenderer {
        get {
            if (_spriteRendererCache == null) {
                _spriteRendererCache = GetComponent<SpriteRenderer>();
            }
            return _spriteRendererCache;
        }
    }

    SpriteRenderer _spriteRendererCache;

    public MZ.Sprites.ShaderMode shaderMode {
        set {
            _shaderMode = value;
            switch(_shaderMode) {
            case MZ.Sprites.ShaderMode.Additive:
                gameObject.renderer.material = MZ.Sprites.SharedResources.instance.additiveMaterial;
                break;
                    
            case MZ.Sprites.ShaderMode.Default:
            default:
                gameObject.renderer.material = gameObject.renderer.sharedMaterial;
                break;
            }
        }
        get {
            return _shaderMode;
        }
    }

    MZ.Sprites.ShaderMode _shaderMode;

    public Dictionary<string, MZ.Sprites.AnimationSet> animationsSetsByName {
        get {
            if (_animationsSetsByNameCache == null) {
                _animationsSetsByNameCache = new Dictionary<string, MZ.Sprites.AnimationSet>();
            }
            return _animationsSetsByNameCache;
        }
    }

    Dictionary<string, MZ.Sprites.AnimationSet> _animationsSetsByNameCache;

    public bool playing {
        get {
            return _currentUpdateAction != null;
        }
    }

    public void AddAnimationSet(string framesSetName, MZ.Sprites.AnimationSet animationSet) {
		MZ.Debugs.AssertIfNullWithMessage(animationSet, "animation set is null, framse-set = {0}", framesSetName);
        MZ.Debugs.Assert(animationsSetsByName.ContainsKey(framesSetName) == false, "duplicate frames set name({0})", framesSetName);
        animationsSetsByName.Add(framesSetName, animationSet);
    }

    public void SetFrameButNotPlay() {
        MZ.Debugs.AssertIfNullWithMessage(animationsSetsByName.Count > 0, "no any animationsSets");


        // ignore c# method ... is sucks ...
//          string[] keys = new string[1];
//          animationsSetsByName.Keys.CopyTo(keys, 0);
//
//          _currentAnimationSet = animationsSetsByName[keys[0]];

        // try my method
        List<string> animKeyNames = MZ.Collections.KeysListFromDict(animationsSetsByName);
        _currentAnimationSet = animationsSetsByName[animKeyNames[0]];

        SetFrameWithIndex(0);
    }

    public bool Play() {
        MZ.Debugs.AssertIfNullWithMessage(animationsSetsByName != null, "no any _animationsSets");

        string[] keys = new string[1];
        animationsSetsByName.Keys.CopyTo(keys, 0);

        return PlayWithName(keys[0]);
    }

    public bool PlayWithName(string animationName) {
        MZ.Debugs.AssertIfNullWithMessage(animationsSetsByName, "no any _animationsSets");

        ResetStateToPlay();

        if (!animationsSetsByName.ContainsKey(animationName)) {
            MZ.Debugs.Log("can not found animation with name = " + animationName + " in " + 
                MZ.Collections.KeysStringInDict(animationsSetsByName));
            return false;
        }

        _currentAnimationSet = animationsSetsByName[animationName];

        SetFrameWithIndex(0);
        _currentUpdateAction = UpdateAniamtion;

        return true;
    }

    public void ResetStateToPlay() {
        _timeCount = 0;
        _frameIndex = 0;
    }

    public void Stop() {
        _currentUpdateAction = null;
    }

    public void Clear() {
        Stop();
        loopDoneAction = null;
        spriteRenderer.sprite = null;
        animationsSetsByName.Clear();
    }

    #region private

    delegate void UpdateAction();
        
    // update state
    int _frameIndex;
    float _timeCount;
    MZ.Sprites.AnimationSet _currentAnimationSet;
    UpdateAction _currentUpdateAction;
            
    void Update() {
        if (_currentUpdateAction != null) {
            _currentUpdateAction();
        }
    }

    void UpdateAniamtion() {
        float stepTime = stepTimeFunc()*timeScale;
        _timeCount += stepTime;

        if (_timeCount >= _currentAnimationSet.interval) {
            _timeCount -= _currentAnimationSet.interval;
            _frameIndex++;

            bool oneLoopDone = CheckAndCallLoopDoneAction();
            if (!oneLoopDone) {
                SetFrameWithIndex(_frameIndex%_currentAnimationSet.frameInfos.Count);
            }
        }
    }

    bool CheckAndCallLoopDoneAction() {
        if (loopDoneAction == null) {
            return false;
        }

        if (_frameIndex < _currentAnimationSet.frameInfos.Count) {
            return false;
        }

        loopDoneAction(this);
        _currentUpdateAction = null;
        return true;
    }

    void SetFrameWithIndex(int index) {
        MZ.Debugs.AssertIfNullWithMessage(_currentAnimationSet, "_currentAnimationSet is null");
        MZ.Debugs.AssertIfNullWithMessage(spriteRenderer, "_spriteRenderer is null");
        MZ.Debugs.Assert((0 <= index && index < _currentAnimationSet.frameInfos.Count),
                           "index out of range, index=" + index + ", range=" + _currentAnimationSet.frameInfos.Count);

        spriteRenderer.sprite = _currentAnimationSet.frameInfos[index].sprite;
    }

    #endregion
}