#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

public static partial class MZ {
	
	public static partial class Sprites {

		public partial class GUI {
			
			public static Rect DrawFrameToRect(MZ.Sprites.FrameInfo frame, Rect rect, float offsetInnerBox, string title) {
				UnityEngine.GUI.Box(rect, title);
				Rect lastRect = rect;
				
				Rect frameRect = new Rect(
					lastRect.x + offsetInnerBox, lastRect.y + offsetInnerBox, 
					lastRect.width - offsetInnerBox*2, lastRect.height - offsetInnerBox*2
				);
				UnityEngine.GUI.DrawTextureWithTexCoords(frameRect, frame.texture, frame.uvRect);
				
				return lastRect;
			}
			
			public static Rect DrawFrameToRect(MZ.Sprites.FrameInfo frame, Rect rect, float offsetInnerBox, string title, Texture2D outTexture) {
				UnityEngine.GUI.Box(rect, title);
				Rect lastRect = rect;
				
				Rect frameRect = new Rect(lastRect.x + offsetInnerBox, lastRect.y + offsetInnerBox, 
				                          lastRect.width - offsetInnerBox*2, lastRect.height - offsetInnerBox*2);
				UnityEngine.GUI.DrawTextureWithTexCoords(frameRect, frame.texture, frame.uvRect);
				
				if(outTexture != null) {
					UnityEngine.GUI.DrawTexture(lastRect, outTexture);
				}
				
				return lastRect;
			}
			
			public static Rect DrawFrameOnCurrentLayout(MZ.Sprites.FrameInfo frame, float width, float height, float offsetInnerBox, string title) {
				GUILayout.Box(title, GUILayout.Width(width), GUILayout.Height(height));
				Rect lastRect = GUILayoutUtility.GetLastRect();
				
		        if(frame != null) {
				    Rect frameRect = new Rect(lastRect.x + offsetInnerBox, lastRect.y + offsetInnerBox, width - offsetInnerBox*2, height - offsetInnerBox*2);
					UnityEngine.GUI.DrawTextureWithTexCoords(frameRect, frame.texture, frame.uvRect);
		        }
				
				return lastRect;
			}
		
			public static void LayuotFrameButtons(MZ.Sprites.FrameInfo[] frameInfos, int maxRow, Vector2 size, 
			                                      Func<MZ.Sprites.FrameInfo, int, bool> isDrawOutline, 
			                               		  Action<MZ.Sprites.FrameInfo, int> action, Texture2D cursor) {
				EditorGUILayout.BeginVertical();
				
				for(int col = 0; col < frameInfos.Length/maxRow + 1; col++) {
					EditorGUILayout.BeginHorizontal();
					
					for(int row = 0; row < maxRow; row++) {
						int index = col*maxRow + row;
						if(index >= frameInfos.Length) {
							break;
						}
						
						MZ.Sprites.FrameInfo frame = frameInfos[index];
						
						if(GUILayout.Button("", GUILayout.Width(size.x), GUILayout.Height(size.y)) && action != null) {
							action(frame, index);
						}
						
						Texture2D outlineTexture = (isDrawOutline != null && isDrawOutline(frame, index))? cursor : null;
						
						Rect lastRect = GUILayoutUtility.GetLastRect();
						GUI.DrawFrameToRect(frame, lastRect, 4, "", outlineTexture);
					}
					
					EditorGUILayout.EndHorizontal();
				}
				
				EditorGUILayout.EndVertical();
			}
		
			public static void LayoutFrameButton(MZ.Sprites.FrameInfo frameInfo, Action<MZ.Sprites.FrameInfo, Rect> clickAction, Vector2 size) {
				bool isClick = GUILayout.Button("", GUILayout.Width(size.x), GUILayout.Height(size.y));
		
				Rect lastRect = GUILayoutUtility.GetLastRect();
				GUI.DrawFrameToRect(frameInfo, lastRect, 4, "");
		
				if(isClick && clickAction != null) {
					clickAction(frameInfo, lastRect);
				}
			}
		}
	}	
}

#endif