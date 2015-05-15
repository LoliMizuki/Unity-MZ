using UnityEngine;
using System;
using System.Collections;

public class MZGameObjectsPoolElement : MonoBehaviour {

	public int indexInPool;
    public Action<GameObject> onReturnPoolAction;

	public MZ.GameObjectsPool pool;

	public void ReturnToPool() {
		MZ.Debugs.AssertIfNullWithMessage(pool, "no pool to return");

		if(onReturnPoolAction != null) onReturnPoolAction(gameObject);

		pool.ReturnGameObject(gameObject);
	}
}
