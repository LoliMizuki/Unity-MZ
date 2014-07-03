using UnityEngine;
using System.Collections.Generic;

static public partial class MZ {
	
	static public partial class Sprites {

    public class FramesSet {

        public string textureName {
            get {
                return _texture.name;
            }
        }
        
        public Texture2D texture {
            get {
                return _texture;
            }
        }
        
        public int count {
            get {
                return _frameInfosByName.Count;
            }
        }
        
		public string[] frameInfoNames {
            get {
                if(_frameInfoNames == null) {
                    UpdateFrameInfoNames();
                }
                
                return _frameInfoNames.ToArray();
            }
        }
        
        static public FramesSet FramesSetWithDictionaryAndTexture(Dictionary<string, FrameInfo> frameInfosByName, Texture2D texture) {
            MZ.Debugs.Assert(frameInfosByName != null, "frameInfosByName is null");
            MZ.Debugs.Assert(texture != null, "texture is null");
            
            FramesSet framesSet = new FramesSet();
            framesSet._texture = texture;
            framesSet._frameInfosByName = frameInfosByName;
            framesSet.UpdateFrameInfoNames();
            
            return framesSet;
        }
        
        public FrameInfo this[string name] {
            get {
                return FrameInfoWithName(name);
            }
        }
        
        public FrameInfo FrameInfoWithName(string name) {
            if(_frameInfosByName == null) {
                return null;
            }

			if(!_frameInfosByName.ContainsKey(name)) {
				MZ.Debugs.Log("could not found frame '" + name + "' in " + textureName);
			}
            
            return _frameInfosByName[name];
        }
        
        public void Clear() {
            if(_frameInfosByName != null) {
				foreach(FrameInfo frameInfo in _frameInfosByName.Values) {
					frameInfo.Clear();
				}
                _frameInfosByName.Clear();
            }

            if(_frameInfoNames != null) {
                _frameInfoNames.Clear();
            }
            
//            GameObject.DestroyImmediate(_texture, false);
        }
        
        #region private
        Texture2D _texture;
        Dictionary<string, FrameInfo> _frameInfosByName;
        List<string> _frameInfoNames;
        
        private FramesSet() {
        }

        private void UpdateFrameInfoNames() {
            if(_frameInfosByName == null) {
                return;
            }

            if(_frameInfoNames == null) {
                _frameInfoNames = new List<string>();
            }

            _frameInfoNames.Clear();

            foreach(string frameName in _frameInfosByName.Keys) {
                _frameInfoNames.Add(frameName);
            }
        }
        #endregion
    }
}
}