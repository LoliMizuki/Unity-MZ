// Can not add to MZ, bcuz Unity (suck) component can not handle different class and file name :D

using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class MZGesturesResponder : MonoBehaviour {

    public Action<TapGesture> tapAction = null;
    
	public Action<DragGesture> dragAction = null;
    
	public Action<FingerHoverEvent> hoverAction = null;

	public void TapWithGesture(TapGesture gesture) {
		if (tapAction == null) return;
		tapAction(gesture);
	}

	public void DragWithGesture(DragGesture gesture) {
		if (dragAction == null) return;
		dragAction(gesture);
	}

	public void HoverWithEvent(FingerHoverEvent e) {
		if (hoverAction == null) return;
		hoverAction(e);
	}
}