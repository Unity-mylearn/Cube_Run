using UnityEngine;
using System.Collections;

public class CreateNextFloor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("pengpeng");
		if (other.name == "body") {
			int i = Random.Range (0, FloorManager._instanceFloorManger.floors.Length - 1);
			GameObject gb = GameObject.Instantiate (FloorManager._instanceFloorManger.floors [i],
				new Vector3 (0, 0, FloorManager._instanceFloorManger.floorCount * 449), Quaternion.identity,
				FloorManager._instanceFloorManger.Parent) as GameObject;
			FloorManager._instanceFloorManger.floorCount++;
			FloorManager.garbage.Add (gb);
			Debug.Log ("添加完成");
		}
	}
}
