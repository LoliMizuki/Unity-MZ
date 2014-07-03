using UnityEngine;
using System;
using System.Collections.Generic;
using MiniJSON;

static public partial class MZ {
	
	static public partial class Sprites {

	    public class FrameImporter {
	
			static public MZ.Sprites.FramesSet FramesSetFromShoeBoxPathInResources(string pathInResources) {
				TextAsset jsonFromShoebox = Resources.Load<TextAsset>(pathInResources);
				MZ.Debugs.AssertIfNullWithMessage(jsonFromShoebox, "can not found spritesheet file in resources path: " + pathInResources);
				
				Texture2D texture = Resources.Load<Texture2D>(pathInResources);
				MZ.Debugs.AssertIfNullWithMessage(texture, "can not found texture file in resources path: " + pathInResources);
	
	            string jsonStringFromShoebox = jsonFromShoebox.ToString();
	
				Dictionary<string, MZ.Sprites.FrameInfo> frameInfosDictionary = FrameInfosDictionaryFromShoeBoxJsonAndTexture(jsonStringFromShoebox, texture);
				MZ.Sprites.FramesSet framesSet = MZ.Sprites.FramesSet.FramesSetWithDictionaryAndTexture(frameInfosDictionary, texture);
	
	            return framesSet;
			}
	
	        static public Dictionary<string, MZ.Sprites.FrameInfo> FrameInfosDictionaryFromShoeBoxJsonAndTexture(string shoeboxJson, Texture2D texture) {
	            Dictionary<string, object> root = Json.Deserialize(shoeboxJson) as Dictionary<string, object>;
	            Dictionary<string, object> frames = root["frames"] as Dictionary<string, object>;
	
	            Dictionary<string, MZ.Sprites.FrameInfo> frameInfosByName = new Dictionary<string, MZ.Sprites.FrameInfo>();
	            foreach(string frameName in frames.Keys) {
	                Dictionary<string, object> frame = frames[frameName] as Dictionary<string, object>;
	
					Rect frameRect = RectFromDict(frame, "frame");
					Rect sourceSizeRect = RectFromDict(frame, "spriteSourceSize");
	
					MZ.Sprites.FrameInfo fraInfo = FrameInfoFromFrameRectSourceSizeRect(frameName, texture, frameRect, sourceSizeRect);
	                frameInfosByName.Add(fraInfo.name, fraInfo);
	            }
	            
	            return frameInfosByName;
	        }
	
			static Rect RectFromDict(Dictionary<string, object> dict, string rectKey) {
				if(!dict.ContainsKey(rectKey)) {
					MZ.Debugs.AssertAlwaysFalse("can not found rect info in dict with key = " + rectKey);
					return new Rect(0, 0, 0, 0);
				}
	
				Dictionary<string, object> rectInfo = (Dictionary<string, object>)dict[rectKey];
				int x = int.Parse(rectInfo["x"].ToString());
				int y = int.Parse(rectInfo["y"].ToString());
				int w = int.Parse(rectInfo["w"].ToString());
				int h = int.Parse(rectInfo["h"].ToString());
	
				return new Rect(x, y, w, h);
			}
	
			static MZ.Sprites.FrameInfo FrameInfoFromFrameRectSourceSizeRect(string name, Texture2D texture, Rect originFrameRect, Rect sourceSizeRect) {
				int yInvertForUnity = (int)(texture.height - originFrameRect.y - originFrameRect.height);
				Rect frameRect = new Rect(originFrameRect.x, yInvertForUnity, originFrameRect.width, originFrameRect.height);
	
				Vector2 pivot = PivotFromFrameAndSourceSizeRects(originFrameRect, sourceSizeRect);
				
				MZ.Sprites.FrameInfo frameInfo = MZ.Sprites.FrameInfo.NewWithTextureUVPivot(texture, frameRect, pivot, name);
	        
	        	return frameInfo;
	        }
	
			static Vector2 PivotFromFrameAndSourceSizeRects(Rect frameRect, Rect sourceSizeRect) {
				float maxFrameX = frameRect.width - 1;
				float maxFrameY = frameRect.height - 1;
	
				float maxSourceRectX = sourceSizeRect.width - 1;
				float maxSourceRectY = sourceSizeRect.height - 1;
	
				if(maxFrameX == 0 || maxFrameY == 0) {
					return new Vector2(0, 0);
				}
	
				Vector2 shoeboxTrimmedSpriteCenter = new Vector2(sourceSizeRect.x + maxFrameX/2.0f, sourceSizeRect.y + maxFrameY/2.0f);
	
				Vector2 shoeboxFullSpriteCenter = new Vector2(maxSourceRectX/2.0f, maxSourceRectY/2.0f);
	
				Vector2 shoeboxOffset = shoeboxFullSpriteCenter - shoeboxTrimmedSpriteCenter;
	
				Vector2 shoeboxResultCenter = new Vector2(maxFrameX/2.0f + shoeboxOffset.x, maxFrameY/2.0f + shoeboxOffset.y);
	
				Vector2 unitySpriteResultCenter = new Vector2(shoeboxResultCenter.x, maxFrameY - shoeboxResultCenter.y);
	
				Vector2 pivot = new Vector2(unitySpriteResultCenter.x/maxFrameX, unitySpriteResultCenter.y/maxFrameY);
	
				return pivot;
			}
	    }
	}
}