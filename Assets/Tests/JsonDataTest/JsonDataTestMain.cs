using UnityEngine;
using System;
using System.Collections.Generic;

public class MyType {
	public int x;
	public int y;
}

public class TestJsonData : MZ.Datas.JsonDatas.Data {
	
	public int intFiled;

	public string strFiled;
	
	public Vector3 vector3Filed;
	
	public Color colorFiled;
	
	public Rect rectFiled;
	
	public int setGetProperty { get; set; }
	
	public int internalSetProperty { get; internal set; }
	
	public MyType myTypeField = new MyType();
	
	public int useBeforeToDictAction;
	
	public int useAfterFromDictAction;
	
	public TestJsonData() {
		internalSetProperty = 333;
		
		SetPreExtraActions();
	}
	
	
	
	void SetPreExtraActions() {
		AddActionBeforeToDictionary((data, dict) => {
			var d = data as TestJsonData;
			d.useBeforeToDictAction = 101;
		});
		
		AddActionAfterFromDictionary((data, dict) => {
			var d = data as TestJsonData;
			d.useAfterFromDictAction = 999;
		});
	}
}


public class JsonDataTestMain : MonoBehaviour {

	void Start() {
		TestDataToAndFromJson();
	}
	
	void Update() {
	}
	
	void TestDataToAndFromJson() {
		MZ.Datas.Converts.objectFromTypedValueDict.Add(
			typeof(MyType),
			(type, obj) => {
				var o = obj as MyType;
				return String.Format("[{0}, {1}]", o.x, o.y);
			}
		);
		
		MZ.Datas.Converts.valueFromTypedObjectDict.Add(
			typeof(MyType),
			(type, obj) => {
				string s = obj.ToString();
				s = s.Replace("[", "").Replace("]", "");
				var t = s.Split(',');
				
				var mt = new MyType();
				mt.x = int.Parse(t[0]);
				mt.y = int.Parse(t[1]);
				
				return mt;
			}
		);
	
		var originalData = new TestJsonData();
		originalData.intFiled = 123;
		originalData.strFiled = "nekoko";
		originalData.vector3Filed = new Vector3(9, 8, 7);
		originalData.colorFiled = Color.red;
		originalData.rectFiled = new Rect(0,0, 100, 200);
		originalData.setGetProperty = 555;
		originalData.myTypeField = new MyType();
		originalData.myTypeField.x = 123;
		originalData.myTypeField.y = 456;
		originalData.useBeforeToDictAction = 1;
		originalData.useAfterFromDictAction = 777;
		
		var json = originalData.ToJson();
		
		Debug.Log("original json is: \n" + json);

		var comparisonData = new TestJsonData();
		comparisonData.FromJson(json);

		MZ.Debugs.Assert(originalData.intFiled == comparisonData.intFiled, "fail");
		MZ.Debugs.Assert(originalData.strFiled == comparisonData.strFiled, "fail");
		MZ.Debugs.Assert(originalData.vector3Filed == comparisonData.vector3Filed, "fail");
		MZ.Debugs.Assert(originalData.colorFiled == comparisonData.colorFiled, "fail");
		MZ.Debugs.Assert(originalData.rectFiled == comparisonData.rectFiled, "fail");
		MZ.Debugs.Assert(originalData.myTypeField.x == comparisonData.myTypeField.x, "fail");
		MZ.Debugs.Assert(originalData.myTypeField.y == comparisonData.myTypeField.y, "fail");
		MZ.Debugs.Assert(comparisonData.useBeforeToDictAction == 101, "should be 101, but " + comparisonData.useBeforeToDictAction.ToString());
		MZ.Debugs.Assert(comparisonData.useAfterFromDictAction == 999, "should be 999, but " + comparisonData.useAfterFromDictAction.ToString());
		
		Debug.Log("test ok");
	}
}
