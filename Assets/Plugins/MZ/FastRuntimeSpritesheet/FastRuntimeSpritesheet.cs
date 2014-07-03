using UnityEngine;
using System.Collections;

using System;
using System.IO;

static public partial class MZ {

	public class FastRuntimeSpritesheet {
	
		static public FastRuntimeSpritesheet TestWithTextures(Texture2D[] textures) {
			FastRuntimeSpritesheet s = new FastRuntimeSpritesheet();
			s.SetSpritesheetTextureWithSizeFormat(512, 512);
			s.EmbedTexturesToSpritesheet(textures);
			
			return s;
		}
		
		static public FastRuntimeSpritesheet NewWithSize(int width, int height) {
			FastRuntimeSpritesheet s = new FastRuntimeSpritesheet();
			s.SetSpritesheetTextureWithSizeFormat(width, height);
			return s;
		}
		
		static public FastRuntimeSpritesheet NewWithSizeAndTextures(int width, int height, Texture2D[] textures) {
			FastRuntimeSpritesheet s = NewWithSize(width, height);
			s.EmbedTexturesToSpritesheet(textures);
			return s;
		}
		
		public void ExportToPngFile(string path) {
			if (_spritesheetTexture == null) {
				MZ.Debugs.Log("spritesheetTexture is not exist");
				return;
			}
			
			byte[] bytes = _spritesheetTexture.EncodeToPNG();
			File.WriteAllBytes(path, bytes);
		}
	 
		public Texture2D spritesheetTexture { get { return _spritesheetTexture; } }
		
		Texture2D _spritesheetTexture = null;
		
		Vector2i _spritesheetTextureMaxPosition { get { return new Vector2i(_spritesheetTexture.width - 1, _spritesheetTexture.height - 1); } }
		
		private FastRuntimeSpritesheet() {
		}
		
		void SetSpritesheetTextureWithSizeFormat(int width, int height) {
			if (_spritesheetTexture != null) {
				Texture.DestroyImmediate(_spritesheetTexture);
			}
			
			_spritesheetTexture = new Texture2D(width, height);
			for (int x = 0; x < _spritesheetTexture.width; x++) {
				for (int y = 0; y < _spritesheetTexture.height; y++) {
					_spritesheetTexture.SetPixel(x, y, new Color(1, 1, 1, 0));
				}
			}
		}
		
		void EmbedTexturesToSpritesheet(Texture2D[] textures) {
			if (_spritesheetTexture == null) {
				MZ.Debugs.Log("no spritesheet texture exist, please create it first");
				return;
			}
			
			int x = 1;
			int y = 1;
			int nextY = 0;
			
			foreach (Texture2D t in textures) {
				if (x + t.width >= _spritesheetTextureMaxPosition.x) {
					nextY++;
					y = nextY + 1;
					x = 0;
				}
				
				EmbedTextureToSpritesheetWithPosition(t, new Vector2i(x, y));
				x += t.width + 1;
				nextY = (y + t.height > nextY)? y + t.height : nextY;
				
				if (nextY > _spritesheetTextureMaxPosition.y) {
					break;
				}
			}
			
			_spritesheetTexture.Apply();
		}
	
		void EmbedTextureToSpritesheetWithPosition(Texture2D texture, Vector2i pos) {
			if (_spritesheetTexture == null) return;
			if (pos.x + texture.width >= _spritesheetTextureMaxPosition.x || pos.y + texture.height >= _spritesheetTextureMaxPosition.y) return;
			
			int maxX = pos.x + texture.width;
			int maxY = pos.y + texture.height;
			
			int x, y, xInTex, yInTex;
			for (x = pos.x, xInTex = 0; x < maxX; x++, xInTex++) {
				for (y = pos.y, yInTex = 0; y < maxY; y++, yInTex++) {
					_spritesheetTexture.SetPixel(x, y, texture.GetPixel(xInTex, yInTex));
				}	
			}
		}
	}
	
	public struct Vector2i {
		public int x;
		public int y;
		
		public Vector2i(int x, int y) {
			this.x = x;
			this.y = y;
		}
		
		override public string ToString() {
			return string.Format("(x: {0}, y: {1})", x, y);
		}
	}
}
