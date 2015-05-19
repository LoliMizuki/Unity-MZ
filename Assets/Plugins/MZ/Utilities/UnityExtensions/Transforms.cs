using UnityEngine;
using System;
using System.Collections;

public static partial class MZ {

    public class Transforms {

        public static void MapToChildren(Transform transform, Action<Transform> action) {
            if (transform == null) return;
            foreach (Transform childTransform in transform) action(childTransform);
        }

		public static void MapToChildrenWithArgvs(Transform transform, Action<Transform, object[]> action, object[] argv) {
            if (transform == null) return;
            foreach (Transform childTransform in transform) action(childTransform, argv);
        }

        public static void RemoveAllChildren(Transform trans) {
            while(trans.childCount > 0) GameObject.DestroyImmediate(trans.GetChild(0).gameObject);
        }
    }
}