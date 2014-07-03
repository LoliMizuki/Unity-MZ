#if UNITY_EDITOR

using UnityEngine;
using System.Collections.Generic;

public class MZTestAnimationNodeInfo : MonoBehaviour {

    public List<string> AnimationSetNames = new List<string>();

    public void SetInfoWithAnimation(MZAnimation animation) {
        if(animation == null) {
            return;
        }

        AnimationSetNames = new List<string>();
        foreach(string animationName in  animation.animationsSetsByName.Keys) {
            AnimationSetNames.Add(animationName);
        }
    }
}

#endif