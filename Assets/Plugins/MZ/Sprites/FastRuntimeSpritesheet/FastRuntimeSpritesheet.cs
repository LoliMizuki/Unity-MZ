using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

static public partial class MZ {

	static public partial class Sprites {

		public class FastRuntimeSpritesheet {
		
			static public FastRuntimeSpritesheet NewWithSize(int size) {
				FastRuntimeSpritesheet s = new FastRuntimeSpritesheet();
				s.SetSpritesheetTextureWithSizeFormat(size, size);
				return s;
			}
			
			static public FastRuntimeSpritesheet NewWithSizeAndTextures(int size, Texture2D[] textures) {
				FastRuntimeSpritesheet s = NewWithSize(size);
				s.EmbedTexturesToSpritesheet(textures);
				return s;
			}
			
			public Texture2D spritesheetTexture { get; internal set; }
			
			public List<FrameInfo> frameInfos { get; internal set; }
			
			public void ExportToPngFile(string path) {
				if (spritesheetTexture == null) {
					MZ.Debugs.Log("spritesheetTexture is not exist");
					return;
				}
				
				byte[] bytes = spritesheetTexture.EncodeToPNG();
				File.WriteAllBytes(path, bytes);
			}
			
			public void ClearSpritesheetTexture() {
				frameInfos.Clear();
				if (spritesheetTexture != null) Texture.DestroyImmediate(spritesheetTexture);
			}
			
			Vector2i _spritesheetTextureMaxPosition { get { return new Vector2i(spritesheetTexture.width - 1, spritesheetTexture.height - 1); } }
			
			private FastRuntimeSpritesheet() {
				frameInfos = new List<FrameInfo>();
			}
			
			void SetSpritesheetTextureWithSizeFormat(int width, int height) {
				if (spritesheetTexture != null) {
					Texture.DestroyImmediate(spritesheetTexture);
				}
				
				spritesheetTexture = new Texture2D(width, height);
				for (int x = 0; x < spritesheetTexture.width; x++) {
					for (int y = 0; y < spritesheetTexture.height; y++) {
						spritesheetTexture.SetPixel(x, y, new Color(1, 1, 1, 0));
					}
				}
			}
			
			void EmbedTexturesToSpritesheet(Texture2D[] textures) {
				if (spritesheetTexture == null) {
					MZ.Debugs.Log("no spritesheet texture exist, please create it first");
					return;
				}
				
				int x = 1;
				int y = 1;
				int nextY = 0;
				
				List<FramePamaters> framePamaters = new List<FramePamaters>();
				
				foreach (Texture2D t in textures) {
					if (x + t.width >= _spritesheetTextureMaxPosition.x) {
						nextY++;
						y = nextY + 1;
						x = 0;
					}
					
					FramePamaters fp = null;
					EmbedTextureToSpritesheetWithPosition(t, new Vector2i(x, y), out fp);
					if (fp != null) framePamaters.Add(fp);
					
					x += t.width + 1;
					nextY = (y + t.height > nextY)? y + t.height : nextY;
					
					if (nextY > _spritesheetTextureMaxPosition.y) {
						break;
					}
				}
				
				spritesheetTexture.Apply();
				SetNewFrameInfosWithParametersList(framePamaters);
			}
		
			void EmbedTextureToSpritesheetWithPosition(Texture2D texture, Vector2i pos, out FramePamaters fp) {
				fp = null;
			
				if (spritesheetTexture == null) return;
				if (pos.x + texture.width >= _spritesheetTextureMaxPosition.x || pos.y + texture.height >= _spritesheetTextureMaxPosition.y) return;
				
				int maxX = pos.x + texture.width;
				int maxY = pos.y + texture.height;
				
				int x, y, xInTex, yInTex;
				for (x = pos.x, xInTex = 0; x < maxX; x++, xInTex++) {
					for (y = pos.y, yInTex = 0; y < maxY; y++, yInTex++) {
						spritesheetTexture.SetPixel(x, y, texture.GetPixel(xInTex, yInTex));
					}	
				}
				
				fp = new FramePamaters();
				fp.name = texture.name;
				fp.uvRect = new Rect(pos.x, pos.y, texture.width, texture.height);
				fp.pivot = new Vector2(.5f, .5f);
			}
			
			void SetNewFrameInfosWithParametersList(List<FramePamaters> list) {
				if (spritesheetTexture == null) return;
				if (frameInfos == null) frameInfos = new List<FrameInfo>();
			
				frameInfos.Clear();
				
				foreach (FramePamaters fp in list)  {
					FrameInfo fi = FrameInfo.NewWithTextureUVPivot(spritesheetTexture, fp.uvRect, fp.pivot, fp.name);
					frameInfos.Add(fi);
				}
			}
			
			class FramePamaters {
				public string name = string.Empty;
				public Rect uvRect = new Rect();
				public Vector2 pivot = Vector2.zero;
			}
		}
	}
}