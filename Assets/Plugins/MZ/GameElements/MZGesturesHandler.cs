// Can not add to MZ, bcuz Unity (suck) component can not handle different class and file name :D

using UnityEngine;
using System.Collections;

public class MZGesturesHandler : MonoBehaviour {

    void OnTap(TapGesture gesture) {
        if (gesture.Selection == null) return;
       
        MZGesturesResponder responder = gesture.Selection.GetComponent<MZGesturesResponder>();
        if (responder == null) return;
       
        responder.TapWithGesture(gesture);
    }

    void OnDrag(DragGesture gesture) {
        if (gesture.Selection == null) return;
        
        MZGesturesResponder responder = gesture.Selection.GetComponent<MZGesturesResponder>();
        if (responder == null) return;
        
        responder.DragWithGesture(gesture);
    }

    void OnFingerHover(FingerHoverEvent e) { 
        if (e.Selection == null) return;
       
        MZGesturesResponder responder = e.Selection.GetComponent<MZGesturesResponder>();
        if (responder == null) return;
        
        responder.HoverWithEvent(e);
    }
}