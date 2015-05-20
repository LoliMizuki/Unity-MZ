using UnityEngine;
using System.Collections.Generic;

public class TestJsonClass : MZ.Datas.JsonDatas.Data {
	
	// fields
	
	public int intFiled;

	public string strFiled;
	
	public Vector3 vector3Filed;
	
	public Color colorFiled;
	
	public Rect rectFiled;
	
	// properties
}


public class JsonDataTestMain : MonoBehaviour {

	void Start() {
		TestDataToAndFromJson();
	}
	
	void Update() {
	}
	
	void TestDataToAndFromJson() {
		var originalData = new TestJsonClass();
		originalData.intFiled = 123;
		originalData.strFiled = "nekoko";
		originalData.vector3Filed = new Vector3(9, 8, 7);
		originalData.colorFiled = Color.red;
		originalData.rectFiled = new Rect(0,0, 100, 200);
		
		var json = originalData.ToJson();
		
		Debug.Log("original json is: \n" + json);

		var comparisonData = new TestJsonClass();
		comparisonData.FromJson(json);

		MZ.Debugs.Assert(originalData.intFiled == comparisonData.intFiled, "fail");
		MZ.Debugs.Assert(originalData.strFiled == comparisonData.strFiled, "fail");
		MZ.Debugs.Assert(originalData.vector3Filed == comparisonData.vector3Filed, "fail");
		MZ.Debugs.Assert(originalData.colorFiled == comparisonData.colorFiled, "fail");
		MZ.Debugs.Assert(originalData.rectFiled == comparisonData.rectFiled, "fail");
		
		Debug.Log("test ok");
	}
}
