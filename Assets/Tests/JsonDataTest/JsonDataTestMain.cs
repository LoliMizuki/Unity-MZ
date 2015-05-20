using UnityEngine;
using System.Collections;

public class TestJsonClass : MZ.Datas.JsonDatas.Data {
	
	// fields
	
//	public int intFiled;

//	public string strFiled;
	
	public Vector3 vector3Filed;
	
	// properties
}


public class JsonDataTestMain : MonoBehaviour {

	void Start() {
		TestDataToAndFromJson();
	}
	
	void Update() {
	
	}
	
	void TestDataToAndFromJson() {
		var data = new TestJsonClass();
//		data.intFiled = 123;
//		data.strFiled = "nekoko";
		data.vector3Filed = new Vector3(9, 8, 7);
		
		var json = data.ToJson();
		
//		Debug.Log(data.ToDictionary()["vector3Filed"]);		
//		Debug.Log(json);

		var data2 = new TestJsonClass();
		data2.FromJson(json);

//		MZ.Debugs.Assert(data.intFiled == data2.intFiled, "fail");
//		MZ.Debugs.Assert(data.strFiled == data2.strFiled, "fail");
//		MZ.Debugs.Assert(data.vector3Filed == data2.vector3Filed, "fail");
		
		Debug.Log("test ok");
	}
}
