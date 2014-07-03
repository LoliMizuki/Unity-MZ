// Can not add to MZ, bcuz Unity (suck) component can not handle different class and file name :D

#if WE_HAVE_FG

using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class MZGesturesResponder : MonoBehaviour {

    public Action<TapGesture> tapAction;
    public Action<DragGesture> dragAction;
	public Action<FingerHoverEvent> hoverAction;

	public void TapWithGesture(TapGesture gesture) {
		if(tapAction != null) {
			tapAction(gesture);
		}
	}

	public void DragWithGesture(DragGesture gesture) {
		if(dragAction != null) {
			dragAction(gesture);
		}
	}

	public void HoverWithEvent(FingerHoverEvent e) {
		if(hoverAction != null) {
			hoverAction(e);
		}
	}
}

#endif