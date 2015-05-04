using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class FastRuntimeSpritesheetMain : MonoBehaviour {
	
	float _delayCount = 5.0f;
	
	Action _currentUpdate = null;
	
	string _message = "";
	
	Queue<Action> _testCaseActions = new Queue<Action>();
	
	long _preMem = 0;

	void Start() {
		TestToMatchOriginSprite();
//		_testCaseActions.Enqueue(Make1024x1_NonPoT);
//		_testCaseActions.Enqueue(Make1024xN_NonPoT);
//		_testCaseActions.Enqueue(Make1024x1_PoT);
//		_testCaseActions.Enqueue(Make1024xN_PoT);
//				
//		StartDelayAndDequeueNextAction();
	}
	
	void Update() {
		if (_currentUpdate != null) _currentUpdate();
	}
	
	void OnGUI() {
		GUI.Label(new Rect(50, 50, 400, 400), _message);
		
		if(GUI.Button(new Rect(40, 40, 100, 100), "Suck")) {
			Application.LoadLevel("Another");
		}
	}
	
	void AddMessageToNewLine(string message) {
		_message += "\n" + message;
	}
	
	void StartDelayAndDequeueNextAction() {
		long currMem = GC.GetTotalMemory(false);
		long inc = currMem - _preMem;
		_preMem = currMem;
		AddMessageToNewLine("current mem: " + currMem + ", increase: " + inc);
		GC.Collect();
	
		if (_testCaseActions.Count == 0) {
			AddMessageToNewLine("done!");
			_currentUpdate = null;
			return;
		}
	
		AddMessageToNewLine("rest 5 sec");
		_delayCount = 5.0f;
		_currentUpdate = DelayAction;
	}
	
	void DelayAction() {
		if(_testCaseActions.Count == 0) return;
	
	
		_delayCount -= Time.deltaTime;
		
		if (_delayCount > 0) return;
		_currentUpdate = _testCaseActions.Dequeue();
	}
	
	void Make1024x1_NonPoT() {
		DateTime pre = System.DateTime.Now;
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, NonPotTexturesSet2());
		DateTime after = System.DateTime.Now;
		
		double passedTime = 0.001 * (after - pre).TotalMilliseconds;
		
		AddMessageToNewLine("1024x1, non-pot: passed time = " + passedTime.ToString());
		
		StartDelayAndDequeueNextAction();
	}
	
	void Make1024x1_PoT() {
		DateTime pre = System.DateTime.Now;
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, PotTexturesSet1());
		DateTime after = System.DateTime.Now;
		
		double passedTime = 0.001 * (after - pre).TotalMilliseconds;
		
		AddMessageToNewLine("1024x1, pot: passed time = " + passedTime.ToString());
			
		StartDelayAndDequeueNextAction();
	}
	
	void TestToMatchOriginSprite() {
		MZ.Sprites.FastRuntimeSpritesheet frss = MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, PotTexturesSet1());
		
		GameObject go1 = new GameObject("create");
		go1.AddComponent<SpriteRenderer>().sprite = frss.frameInfos[0].sprite;
		go1.transform.position = new Vector3(0, 0, -50);
		go1.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f);
		
		string name = frss.frameInfos[0].name;
		Texture2D originTex = Resources.Load<Texture2D>("pot/" + name);
		Sprite s = Sprite.Create(originTex, new Rect(0, 0, originTex.width, originTex.height), new Vector2(.5f, .5f), 1);
		
		GameObject go2 = new GameObject("origin");
		go2.AddComponent<SpriteRenderer>().sprite = s;
		go2.transform.position = Vector3.zero;
	}
	
	void Make1024xN_NonPoT() {
		DateTime pre = System.DateTime.Now;
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, NonPotTexturesSet2());
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, NonPotTexturesSet2());
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, NonPotTexturesSet2());
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, NonPotTexturesSet2());
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, NonPotTexturesSet2());
		DateTime after = System.DateTime.Now;
		
		double passedTime = 0.001 * (after - pre).TotalMilliseconds;
		
		AddMessageToNewLine("1024x5, non-pot: passed time = " + passedTime.ToString());
	
		StartDelayAndDequeueNextAction();
	}
	
	void Make1024xN_PoT() {
		DateTime pre = System.DateTime.Now;
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, PotTexturesSet1());
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, PotTexturesSet1());
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, PotTexturesSet1());
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, PotTexturesSet1());
		MZ.Sprites.FastRuntimeSpritesheet.NewWithSizeAndTextures(1024, PotTexturesSet1());
		DateTime after = System.DateTime.Now;
		
		double passedTime = 0.001 * (after - pre).TotalMilliseconds;
		
		AddMessageToNewLine("1024x5, pot: passed time = " + passedTime.ToString());
		
		StartDelayAndDequeueNextAction();
	}
	
	Texture2D[] NonPotTexturesSet() {
		Texture2D[] texes = {
			Resources.Load<Texture2D>("non-pot-1/barrack_lv1_01"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv1_02"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv1_03"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv1_04"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv2_01"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv2_02"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv2_03"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv2_04"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv3_01"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv3_02"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv3_03"),
			Resources.Load<Texture2D>("non-pot-1/barrack_lv3_04"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv1_01"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv1_02"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv1_03"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv1_04"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv2_01"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv2_02"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv2_03"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv2_04"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv3_01"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv3_02"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv3_03"),
			Resources.Load<Texture2D>("non-pot-1/cannon_turret_lv3_04"),
			Resources.Load<Texture2D>("non-pot-1/e_shield_rare_move0_01"),
			Resources.Load<Texture2D>("non-pot-1/e_shield_rare_move0_02"),
			Resources.Load<Texture2D>("non-pot-1/e_shield_rare_move0_03"),
			Resources.Load<Texture2D>("non-pot-1/e_shield_rare_move180_01"),
			Resources.Load<Texture2D>("non-pot-1/e_shield_rare_move180_02"),
			Resources.Load<Texture2D>("non-pot-1/e_shield_rare_move180_03"),
			Resources.Load<Texture2D>("non-pot-1/shell_mortar"),
			Resources.Load<Texture2D>("non-pot-1/shell_panzerfaust"),
			Resources.Load<Texture2D>("non-pot-1/shell_tank"),
			Resources.Load<Texture2D>("non-pot-1/assault_move0_01"),
			Resources.Load<Texture2D>("non-pot-1/assault_move0_02"),
			Resources.Load<Texture2D>("non-pot-1/assault_move0_03"),
			Resources.Load<Texture2D>("non-pot-1/assault_move45_01"),
			Resources.Load<Texture2D>("non-pot-1/assault_move45_02"),
			Resources.Load<Texture2D>("non-pot-1/assault_move45_03"),
			Resources.Load<Texture2D>("non-pot-1/assault_move90_01"),
			Resources.Load<Texture2D>("non-pot-1/assault_move90_02"),
			Resources.Load<Texture2D>("non-pot-1/assault_move90_03"),
			Resources.Load<Texture2D>("non-pot-1/assault_move90_01"),
			Resources.Load<Texture2D>("non-pot-1/assault_move90_02"),
			Resources.Load<Texture2D>("non-pot-1/assault_move90_03"),
			Resources.Load<Texture2D>("non-pot-1/assault_move135_01"),
			Resources.Load<Texture2D>("non-pot-1/assault_move135_02"),
			Resources.Load<Texture2D>("non-pot-1/assault_move135_03"),
			Resources.Load<Texture2D>("non-pot-1/assault_move180_01"),
			Resources.Load<Texture2D>("non-pot-1/assault_move180_02"),
			Resources.Load<Texture2D>("non-pot-1/assault_move180_03"),
			Resources.Load<Texture2D>("non-pot-1/assault_move225_01"),
			Resources.Load<Texture2D>("non-pot-1/assault_move225_02"),
			Resources.Load<Texture2D>("non-pot-1/assault_move225_03"),
			Resources.Load<Texture2D>("non-pot-1/assault_move270_01"),
			Resources.Load<Texture2D>("non-pot-1/assault_move270_02"),
			Resources.Load<Texture2D>("non-pot-1/assault_move270_03"),
			Resources.Load<Texture2D>("non-pot-1/assault_move315_01"),
			Resources.Load<Texture2D>("non-pot-1/assault_move315_02"),
			Resources.Load<Texture2D>("non-pot-1/assault_move315_03"),
			Resources.Load<Texture2D>("non-pot-1/e_assault_move0_01"),
			Resources.Load<Texture2D>("non-pot-1/e_assault_move0_02"),
			Resources.Load<Texture2D>("non-pot-1/e_assault_move0_03"),
			Resources.Load<Texture2D>("non-pot-1/e_assault_move180_01"),
			Resources.Load<Texture2D>("non-pot-1/e_assault_move180_02"),
			Resources.Load<Texture2D>("non-pot-1/e_assault_move180_03"),
			Resources.Load<Texture2D>("non-pot-1/prison_camp_lv1_01"),
			Resources.Load<Texture2D>("non-pot-1/prison_camp_lv1_02"),
			Resources.Load<Texture2D>("non-pot-1/prison_camp_lv1_03"),
			Resources.Load<Texture2D>("non-pot-1/prison_camp_lv2_01"),
			Resources.Load<Texture2D>("non-pot-1/prison_camp_lv2_02"),
			Resources.Load<Texture2D>("non-pot-1/prison_camp_lv2_03"),
			Resources.Load<Texture2D>("non-pot-1/prison_camp_lv3_01"),
			Resources.Load<Texture2D>("non-pot-1/prison_camp_lv3_02"),
			Resources.Load<Texture2D>("non-pot-1/prison_camp_lv3_03"),
		};
		
		return texes;
	}
	
	Texture2D[] PotTexturesSet1() {
		Texture2D[] texes = {
			Resources.Load<Texture2D>("pot/sev0001a"),
			Resources.Load<Texture2D>("pot/sev0001b"),
			Resources.Load<Texture2D>("pot/sev0001c"),
			Resources.Load<Texture2D>("pot/sev0001d"),
			Resources.Load<Texture2D>("pot/sev0002a"),
			Resources.Load<Texture2D>("pot/sev0002b"),
			Resources.Load<Texture2D>("pot/sev0002c"),
			Resources.Load<Texture2D>("pot/sev0002d"),
			Resources.Load<Texture2D>("pot/sev0003a"),
			Resources.Load<Texture2D>("pot/sev0003b"),
			Resources.Load<Texture2D>("pot/sev0003c"),
			Resources.Load<Texture2D>("pot/sev0003d"),
			Resources.Load<Texture2D>("pot/snm0056c"),
			Resources.Load<Texture2D>("pot/snm0056d"),
			Resources.Load<Texture2D>("pot/snm0057a"),
			Resources.Load<Texture2D>("pot/snm0057b"),
			Resources.Load<Texture2D>("pot/snm0057c"),
			Resources.Load<Texture2D>("pot/snm0057d"),
			Resources.Load<Texture2D>("pot/snm0058a"),
			Resources.Load<Texture2D>("pot/snm0058b"),
			Resources.Load<Texture2D>("pot/snm0058c"),
			Resources.Load<Texture2D>("pot/snm0058d"),
			Resources.Load<Texture2D>("pot/snm0059a"),
			Resources.Load<Texture2D>("pot/snm0059b"),
			Resources.Load<Texture2D>("pot/snm0059c"),
			Resources.Load<Texture2D>("pot/snm0059d"),
			Resources.Load<Texture2D>("pot/snm0060a"),
			Resources.Load<Texture2D>("pot/snm0060b"),
			Resources.Load<Texture2D>("pot/snm0060c"),
			Resources.Load<Texture2D>("pot/snm0060d"),
			Resources.Load<Texture2D>("pot/snm0061a"),
			Resources.Load<Texture2D>("pot/snm0061b"),
			Resources.Load<Texture2D>("pot/snm0061c"),
			Resources.Load<Texture2D>("pot/snm0061d"),
			Resources.Load<Texture2D>("pot/snm0062a"),
			Resources.Load<Texture2D>("pot/snm0062b"),
			Resources.Load<Texture2D>("pot/snm0062c"),
			Resources.Load<Texture2D>("pot/snm0062d"),
			Resources.Load<Texture2D>("pot/snm0063a"),
			Resources.Load<Texture2D>("pot/snm0063b"),
			Resources.Load<Texture2D>("pot/snm0063c"),
			Resources.Load<Texture2D>("pot/snm0063d"),
			Resources.Load<Texture2D>("pot/snm0064a"),
			Resources.Load<Texture2D>("pot/snm0064b"),
			Resources.Load<Texture2D>("pot/snm0064c"),
			Resources.Load<Texture2D>("pot/snm0064d"),
			Resources.Load<Texture2D>("pot/snm0065a"),
			Resources.Load<Texture2D>("pot/snm0065b"),
			Resources.Load<Texture2D>("pot/snm0065c"),
		};
		
		return texes;
	}
	
	Texture2D[] NonPotTexturesSet2() {
		Texture2D[] texes = {
			Resources.Load<Texture2D>("non-pot-2/sev0001a"),
			Resources.Load<Texture2D>("non-pot-2/sev0001b"),
			Resources.Load<Texture2D>("non-pot-2/sev0001c"),
			Resources.Load<Texture2D>("non-pot-2/sev0001d"),
			Resources.Load<Texture2D>("non-pot-2/sev0002a"),
			Resources.Load<Texture2D>("non-pot-2/sev0002b"),
			Resources.Load<Texture2D>("non-pot-2/sev0002c"),
			Resources.Load<Texture2D>("non-pot-2/sev0002d"),
			Resources.Load<Texture2D>("non-pot-2/sev0003a"),
			Resources.Load<Texture2D>("non-pot-2/sev0003b"),
			Resources.Load<Texture2D>("non-pot-2/sev0003c"),
			Resources.Load<Texture2D>("non-pot-2/sev0003d"),
			Resources.Load<Texture2D>("non-pot-2/snm0056c"),
			Resources.Load<Texture2D>("non-pot-2/snm0056d"),
			Resources.Load<Texture2D>("non-pot-2/snm0057a"),
			Resources.Load<Texture2D>("non-pot-2/snm0057b"),
			Resources.Load<Texture2D>("non-pot-2/snm0057c"),
			Resources.Load<Texture2D>("non-pot-2/snm0057d"),
			Resources.Load<Texture2D>("non-pot-2/snm0058a"),
			Resources.Load<Texture2D>("non-pot-2/snm0058b"),
			Resources.Load<Texture2D>("non-pot-2/snm0058c"),
			Resources.Load<Texture2D>("non-pot-2/snm0058d"),
			Resources.Load<Texture2D>("non-pot-2/snm0059a"),
			Resources.Load<Texture2D>("non-pot-2/snm0059b"),
			Resources.Load<Texture2D>("non-pot-2/snm0059c"),
			Resources.Load<Texture2D>("non-pot-2/snm0059d"),
			Resources.Load<Texture2D>("non-pot-2/snm0060a"),
			Resources.Load<Texture2D>("non-pot-2/snm0060b"),
			Resources.Load<Texture2D>("non-pot-2/snm0060c"),
			Resources.Load<Texture2D>("non-pot-2/snm0060d"),
			Resources.Load<Texture2D>("non-pot-2/snm0061a"),
			Resources.Load<Texture2D>("non-pot-2/snm0061b"),
			Resources.Load<Texture2D>("non-pot-2/snm0061c"),
			Resources.Load<Texture2D>("non-pot-2/snm0061d"),
			Resources.Load<Texture2D>("non-pot-2/snm0062a"),
			Resources.Load<Texture2D>("non-pot-2/snm0062b"),
			Resources.Load<Texture2D>("non-pot-2/snm0062c"),
			Resources.Load<Texture2D>("non-pot-2/snm0062d"),
			Resources.Load<Texture2D>("non-pot-2/snm0063a"),
			Resources.Load<Texture2D>("non-pot-2/snm0063b"),
			Resources.Load<Texture2D>("non-pot-2/snm0063c"),
			Resources.Load<Texture2D>("non-pot-2/snm0063d"),
			Resources.Load<Texture2D>("non-pot-2/snm0064a"),
			Resources.Load<Texture2D>("non-pot-2/snm0064b"),
			Resources.Load<Texture2D>("non-pot-2/snm0064c"),
			Resources.Load<Texture2D>("non-pot-2/snm0064d"),
			Resources.Load<Texture2D>("non-pot-2/snm0065a"),
			Resources.Load<Texture2D>("non-pot-2/snm0065b"),
			Resources.Load<Texture2D>("non-pot-2/snm0065c"),
		};
		
		return texes;
	}
}