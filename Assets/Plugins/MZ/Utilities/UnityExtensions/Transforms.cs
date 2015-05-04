using UnityEngine;
using System;
using System.Collections.Generic;

public partial class MZ {

	public static class Transforms {
	
		public static void RemoveChildren(Transform parent, Func<Transform, bool> removeCondition = null) {
			Queue<Transform> removeTrans = new Queue<Transform>();
			
			foreach (Transform childTrans in parent) {
				if (removeTrans != null || removeCondition(childTrans)) removeTrans.Enqueue(childTrans);
				else removeTrans.Enqueue(childTrans);
			}
			
			while (removeTrans.Count > 0) {
				GameObject.DestroyImmediate(removeTrans.Dequeue().gameObject);
			}
		}
	}
}