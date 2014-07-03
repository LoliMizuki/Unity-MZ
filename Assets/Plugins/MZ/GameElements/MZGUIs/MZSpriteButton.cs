#if WE_HAVE_FG

using UnityEngine;
using System;
using System.Collections.Generic;

static public partial class MZ {
 
 	public partial class GUI {
 	
		static public MZSpriteButton AddSpriteButtonToObject(GameObject obj, Sprite releaseSprite, Sprite pressSprite, Func<bool> enableFunc) {
			MZ.Debugs.AssertIfNullWithMessage(obj, "set to null");
			MZSpriteButton sb = MZ.Components.Apply<MZSpriteButton>(obj);
			sb.SetSprites(releaseSprite, pressSprite);
			sb.enableFunc = enableFunc;
			
			return MZ.Components.Apply<MZSpriteButton>(obj);
		}
	}
	
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(MZGesturesResponder))]
	[RequireComponent(typeof(BoxCollider2D))]
	public class MZSpriteButton : MonoBehaviour {
		
		public Sprite releaseSprite;
		public Sprite pressSprite;
		
		public System.Action enterAction;
		public System.Action pressAction;
		public Func<bool> enableFunc;
		
		public void SetSprites(Sprite releaseSprite, Sprite pressSprite) {
			this.pressSprite = pressSprite;
			this.releaseSprite = releaseSprite;
			
			GetComponent<SpriteRenderer>().sprite = this.releaseSprite;
			gameObject.GetComponent<BoxCollider2D>().size = GetComponent<SpriteRenderer>().sprite.bounds.size;
		}
		
		public void SetClickAction(System.Action enterAction, System.Action pressAction) {
			MZ.Debugs.AssertIfNullWithMessage(releaseSprite, "call for releaseSprite first");
		
			MZGesturesResponder gesturesResponder = GetComponent<MZGesturesResponder>();
			MZ.Debugs.AssertIfNullWithMessage(gesturesResponder, "gesturesResponder is null");
			
			this.enterAction = enterAction;
			this.pressAction = pressAction;
			
			gesturesResponder.hoverAction = (evt) => {
				if(enableFunc != null && !enableFunc()) {
					return;
				}
				
				switch(evt.Phase) {
				case FingerHoverPhase.Enter:
					if(enterAction != null){
						enterAction(); 
					}
					GetComponent<SpriteRenderer>().sprite = pressSprite;
					break;
					
				case FingerHoverPhase.Exit:
					GetComponent<SpriteRenderer>().sprite = releaseSprite;
					
					Vector2 worldPosFromTouch = MZVectors.Vector2FromVector3(Camera.main.ScreenToWorldPoint(evt.Position));
					
					if(!evt.Selection.GetComponent<BoxCollider2D>().OverlapPoint(worldPosFromTouch)) {
						return;
					}
					
					if(pressAction != null) {
						pressAction();
					}
					
					break;
				}
			};
		}
		
		public void SetClickAction(System.Action pressAction) {
			SetClickAction(enterAction, pressAction);
		}
		
		public void RemvoeClickAction() {
			MZGesturesResponder gesturesResponder = GetComponent<MZGesturesResponder>();
			gesturesResponder.hoverAction = null;
		}
	}
}

#endif