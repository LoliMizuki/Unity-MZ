using UnityEngine;
using System.Collections;

static public partial class MZ {
	
	static public partial class Sprites {

	    public class FrameInfo {
	
	        static public FrameInfo NewWithTextureUVPivot(Texture2D texture, Rect uvRect, Vector2 pivot, string name) {
	            FrameInfo frameInfo = new FrameInfo();
	            frameInfo._name = ClearFrameNameFromOrigin(name);
	            frameInfo._texture = texture;
	            frameInfo._textureRect = uvRect;
				frameInfo._pivot = pivot;
	
	            frameInfo.CreateSprite();
	
	            return frameInfo;
	        }
	
	        public string name { 
	            get { return _name; } 
	        }
	
	        public Sprite sprite { 
	            get { return _sprite; } 
	        }
	
	        public Texture2D texture { 
	            get { return _texture; } 
	        }
	
	        public Rect textureRect { 
	            get { return _textureRect; } 
	        }
	
	        public Rect uvRect {
	            get {
	                return new Rect(
	                    ((float)sprite.textureRect.x)/((float)sprite.texture.width), 
	                    ((float)sprite.textureRect.y)/((float)sprite.texture.height),
	                    ((float)sprite.textureRect.width)/((float)sprite.texture.width),
	                    ((float)sprite.textureRect.height)/((float)sprite.texture.height));
	            }
	        }
	
	        public float frameWidth {
	            get {
	                return sprite.textureRect.width;
	            }
	        }
	
	        public float frameHeight {
	            get {
	                return sprite.textureRect.height;
	            }
	        }
	
			public void Clear() {
	//			GameObject.DestroyImmediate(_sprite, false);
			}
	
	        #region private
	        string _name;
	        Sprite _sprite;
	        Texture2D _texture;
	        Rect _textureRect;
			Vector2 _pivot;
	
	        private FrameInfo() {
	        }
	
	        static private string ClearFrameNameFromOrigin(string origin) {
	            int indexOfDot = origin.LastIndexOf(".");
	            return (indexOfDot != -1)? origin.Remove(indexOfDot) : origin;
	        }
	
	        private void CreateSprite() {
	            MZ.Debugs.AssertIfNull(_texture);
	            MZ.Debugs.AssertIfNull(_textureRect);
	            MZ.Debugs.AssertIfNull(_name);
	
				_sprite = Sprite.Create(_texture, _textureRect, _pivot, MZ.Sprites.PIXEL_TO_UNIT);
	            _sprite.name = _name;
	        }
	
	        #endregion
	    }
	}
}