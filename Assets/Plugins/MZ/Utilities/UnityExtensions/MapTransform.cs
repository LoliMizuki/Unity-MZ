using UnityEngine;
using System.Collections;

static public partial class MZ {

    public class MapTransform {
        public delegate void ObjectAction(GameObject gameObject);

        public delegate void ObjectActionWithArgvs(GameObject gameObject, object[] argv);
    
        static public void MapActionToTransformChildren(Transform transform, ObjectAction objectAction) {
            if (transform == null) {
                return;
            }

            foreach (Transform childTransform in transform) {
                objectAction(childTransform.gameObject);
            }
        }

        static public void MapActionWithArgvsToTransformChildren(Transform transform, ObjectActionWithArgvs objectAction, object[] argv) {
            if (transform == null) {
                return;
            }

            foreach (Transform childTransform in transform) {
                objectAction(childTransform.gameObject, argv);
            }
        }

        static public void RemoveAllChildrenFromTransform(Transform trans) {
            while(trans.childCount > 0) {
                GameObject.DestroyImmediate(trans.GetChild(0).gameObject);
            }
        }
    }
}