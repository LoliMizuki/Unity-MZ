using UnityEngine;
using System;
using System.Collections.Generic;

static public partial class MZ {

	public partial class Action {
	
		static public ActionSpawn Spawn(params Action[] actions) {
			return new ActionSpawn(actions);
		}
	
		public class ActionSpawn : Action {
	
			public ActionSpawn(params Action[] actions) {
				_actions = new List<Action>(actions);
			}
	
			public override bool isActive {
				get {
					foreach(Action a in _actions) {
						if(a.isActive) return true;
					}
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
	
			List<Action> _actions;
		}
	}
}