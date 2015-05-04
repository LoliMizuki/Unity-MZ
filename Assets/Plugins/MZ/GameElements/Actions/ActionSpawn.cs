using UnityEngine;
using System;
using System.Collections.Generic;

public static partial class MZ {

	public partial class Actions {
	
		public static ActionSpawn Spawn(params ActionBase[] actions) {
			return new ActionSpawn(actions);
		}
	
		public class ActionSpawn : ActionBase {
	
			public ActionSpawn(params ActionBase[] actions) {
				_actions = new List<ActionBase>(actions);
			}
	
			public override bool isActive {
				get {
					foreach (ActionBase a in _actions) if (a.isActive) return true;
					return false;
				}
			}
	
			public override void Start() {
				MapReduce.MapToList(_actions, (a) => {
	                a.deltaTimeFunc = deltaTimeFunc;
	                a.gameObject = this.gameObject;
					a.Start();
	            });
				base.Start();
			}
	
			public override void Update() {
				MapReduce.MapToList(_actions, (a) => a.Update());
				base.Update();
			}
	
			public override void End() {
				MapReduce.MapToList(_actions, (a) => a.End());
				base.End();
			}
	
			List<ActionBase> _actions;
		}
	}
}