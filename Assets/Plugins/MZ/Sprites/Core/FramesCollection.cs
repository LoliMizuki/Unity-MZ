#if !MZ_Sprites_FramesCollection_H
#define MZ_Sprites_FramesCollection_H
#endif

using UnityEngine;
using System.Collections.Generic;

static public partial class MZ {
	
	static public partial class Sprites {
	
	public class FramesCollection {
		
		static public FramesCollection instance {
			get {
				if(_instance == null) {
					_instance = new FramesCollection();
				}
				
				return _instance;
			}
		}
		static FramesCollection _instance = null;
		
		public Dictionary<string, FramesSet> framesSetByTextureName {
			get {
				if(_framesSetByTextureNameCache == null) {
					_framesSetByTextureNameCache = new Dictionary<string, FramesSet>();
				}
				return _framesSetByTextureNameCache;
			}
		}
		Dictionary<string, FramesSet> _framesSetByTextureNameCache;
		
		public void AddFramesSetWithTextureName(FramesSet framesSet, string textureName) {
			if(framesSetByTextureName.ContainsKey(textureName)) {
				MZ.Debugs.Log("duplicate texture = " + textureName);
				return;
			}
			
			framesSetByTextureName.Add(textureName, framesSet);
		}
		
		public FramesSet FramesSetWithTextureName(string textureName) {
			return framesSetByTextureName[textureName];
		}
		
		public FrameInfo FrameInfoWithTextureName(string frameName, string textureName) {
			FramesSet framesSet = framesSetByTextureName[textureName];
			if(framesSet == null) {
				return null;
			}
			
			return framesSet[frameName];
		}
		
		public Sprite SpriteFromTextureName(string spriteName, string textureName) {
			FrameInfo frameInfo = FrameInfoWithTextureName(spriteName, textureName);
			if(frameInfo == null) {
				return null;
			}
			
			return frameInfo.sprite;
		}
		
		public FramesSet AddFramesWithShoeBoxName(string name) {
			string path = MZ.Sprites.SPRITESHEETS_DIRECTORY_IN_RESOURCES + "/" + name;
			return AddFramesWithShoeBoxPathInResources(path);
		}
		
		public FramesSet AddFramesWithShoeBoxPathInResources(string path) {
			FramesSet framesSet = FrameImporter.FramesSetFromShoeBoxPathInResources(path);
			AddFramesSetWithTextureName(framesSet, framesSet.textureName);
		
			return framesSet;
		}
		
		public void Clear() {
			foreach(FramesSet framesSet in framesSetByTextureName.Values) {
				framesSet.Clear();
			}
			
			framesSetByTextureName.Clear();
		}
	}
}
}