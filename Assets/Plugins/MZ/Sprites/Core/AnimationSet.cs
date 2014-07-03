using UnityEngine;
using System.Collections.Generic;
using MiniJSON;

static public partial class MZ {
    
    static public partial class Sprites {

    public class AnimationSet {

        public string name;
        public string textureName;
        public float oneLoopTime;
		public MZ.Sprites.ShaderMode shaderMode;

		// TODO: maybe go to spriteRender Control
		static public Shader ShaderFromMode(MZ.Sprites.ShaderMode shaderMode) {
			switch(shaderMode) {
			case MZ.Sprites.ShaderMode.Additive:
				return Shader.Find("Particles/Additive");

			case MZ.Sprites.ShaderMode.Default:
			default:
				return Shader.Find("Sprites/Default");
			}
		}

		public override string ToString() {
			return name;
		}

        public List<FrameInfo> frameInfos {
            get {
                if(_frameInfos == null) {
                    _frameInfos = new List<FrameInfo>();
                }
                
                return _frameInfos;
            }
        }

        public float interval {
            get {
                return _interval;
            }
        }

        public Texture2D texture {
            get {
                if(textureName == null || FramesCollection.instance.framesSetByTextureName.ContainsKey(textureName) == false) {
                    return null;
                }

                return FramesCollection.instance.framesSetByTextureName[textureName].texture;
            }
        }

        static public AnimationSet New() {
            AnimationSet newSet = new AnimationSet();
            newSet.name = "";
            newSet.textureName = "";
            newSet.oneLoopTime = 0;

            return newSet;
        }

        static public AnimationSet AnimationSetWithParameters(string name, float oneLoopTime, string textureName, params string[] frameNames) {
            AnimationSet animationSet = AnimationSet.New();
            animationSet.name = name;
            animationSet.textureName = textureName;
            animationSet.oneLoopTime = oneLoopTime;
            animationSet.AddFrameNames(frameNames);
        
            animationSet.Apply();

            return animationSet;
        }

        static public AnimationSet AnimationSetFromDictionary(Dictionary<string,object> dictionary) {
            string name = dictionary["name"].ToString();

            float oneLoopTime = float.Parse(dictionary["one_loop_time"].ToString());

            string textureName = dictionary["texture"].ToString();

            List<object> frameNamesList = dictionary["frames"] as List<object>;
            string[] frameNames = new string[frameNamesList.Count];
            for(int i = 0; i < frameNamesList.Count; i++) {
                frameNames[i] = frameNamesList[i].ToString();
            }

            AnimationSet animationSet = AnimationSet.AnimationSetWithParameters(
            	name, 
            	oneLoopTime, 
            	textureName, 
            	frameNames
    		);

			animationSet.shaderMode = (MZ.Sprites.ShaderMode)System.Enum.Parse(typeof(MZ.Sprites.ShaderMode),
			                                                                  dictionary["shader_mode"].ToString());

            return animationSet;
        }

        static public AnimationSet AnimationSetFromJson(string json) { 
            Dictionary<string, object> dict = Json.Deserialize(json) as Dictionary<string, object>;
            return AnimationSetFromDictionary(dict);
        }

        static public Dictionary<string, object> DictionaryFromAnimationSet(AnimationSet animationSet) {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("name", animationSet.name);
            dict.Add("texture", animationSet.textureName);
            
            List<string> frames = new List<string>();
            foreach(FrameInfo frameInfo in animationSet.frameInfos) {
                frames.Add(frameInfo.name);
            }
            dict.Add("frames", frames.ToArray());
            
            dict.Add("one_loop_time", animationSet.oneLoopTime);

			dict.Add("shader_mode", animationSet.shaderMode.ToString());
            
            return dict;
        }

		public void AddFrameName(string frameName) {
			frameInfos.Add(FramesCollection.instance.framesSetByTextureName[textureName][frameName]);
		}

        public void AddFrameNames(string[] frameNames) {
            for(int i = 0; i < frameNames.Length; i++) {
				AddFrameName(frameNames[i]);
            }
        }

        public void Apply() {
			MZ.Debugs.AssertIfNullWithMessage(textureName, "please texture name before apply");
			MZ.Debugs.Assert(FramesCollection.instance.framesSetByTextureName.ContainsKey(textureName), 
			               "can not found texture: " + textureName);

			MZ.Debugs.AssertIfNullWithMessage(name, "please set name before apply");
			MZ.Debugs.AssertIfNullWithMessage(frameInfos, "please frames before apply");
			MZ.Debugs.AssertIfNullWithMessage(oneLoopTime > 0, "oneLoopTime must more than zero");

            if(frameInfos.Count > 0) {
                _interval = oneLoopTime/frameInfos.Count;
            }
        }
    
    #region private
    
        List<FrameInfo> _frameInfos;
        float _interval;

        private AnimationSet() {
			shaderMode = MZ.Sprites.ShaderMode.Default;
        }
        
        #endregion
    }
}
}