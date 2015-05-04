using UnityEngine;
using System;
using System.Collections.Generic;

public static partial class MZ {

	public class GameObjectsPool {
	
		public static GameObjectsPool NewPool(
			int number,
	        Func<GameObject> gameObjectCreateFunc,
			Action<GameObject> onGetAction,
	        Action<GameObject> onReturnAction) {
			MZ.Debugs.AssertIfNullWithMessage(gameObjectCreateFunc, "game-object create function can not be null");
	
			GameObjectsPool pool = new GameObjectsPool();
	
			pool._currentIndex = 0;
			pool._poolDatas = new List<MZPoolData>();
			pool._onGetAction = onGetAction;
			pool._onReturnAction = onReturnAction;
	
			for(int i = 0; i < number; i++) {
				GameObject gObj = gameObjectCreateFunc();
	
				MZGameObjectsPoolElement poolElem = gObj.GetComponent<MZGameObjectsPoolElement>();
				if(poolElem == null) {
					poolElem = gObj.AddComponent<MZGameObjectsPoolElement>();
				}
	
				pool._poolDatas.Add(new MZPoolData(gObj));
			
				poolElem.pool = pool;
				poolElem.indexInPool = pool._poolDatas.Count - 1;
			}
	
			return pool;
		}
	
		public int inactiveCount {
			get {
				int count = 0;
				foreach(MZPoolData pd in _poolDatas) {
					if(pd.state == false) {
						count++;
					}
				}
				return count;
			}	
		}
	
		public GameObject GetGameObject() {
			if(_poolDatas == null) {
				return null;
			}
	
			for(int i = 0; i < _poolDatas.Count; i++) {
				int index = _currentIndex;
				_currentIndex++;
				_currentIndex = _currentIndex%_poolDatas.Count;
	
				if(_poolDatas[index].state == false) {
					_poolDatas[index].state = true;
	
					GameObject gameObj = _poolDatas[index].gameObject;
	
					if(_onGetAction != null) {
						_onGetAction(gameObj);
					}
	
					return gameObj;
				}
			}
	
			return null;
		}
	
		public void ReturnGameObject(GameObject gameObject) {
			MZGameObjectsPoolElement poolElem = gameObject.GetComponent<MZGameObjectsPoolElement>();
			MZ.Debugs.Assert(poolElem.pool == this, "your are not my element");
			MZ.Debugs.Assert(gameObject == _poolDatas[poolElem.indexInPool].gameObject, "who are you?");
	
			_poolDatas[poolElem.indexInPool].state = false;
	
			if(_onReturnAction != null) {
				_onReturnAction(gameObject);
			}
		}
	
		#region - private
	
		int _currentIndex;
		Action<GameObject> _onGetAction;
		Action<GameObject> _onReturnAction;
		List<MZPoolData> _poolDatas;
		
		private GameObjectsPool() {
		}
	
		class MZPoolData {
			public GameObject gameObject;
			public bool state;
	
			public MZPoolData(GameObject gObj) {
				gameObject = gObj;
				state = false;
			}
		}
	
		#endregion
	}
}