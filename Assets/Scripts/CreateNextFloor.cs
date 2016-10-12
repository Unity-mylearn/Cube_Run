using UnityEngine;
using System.Collections;

public class CreateNextFloor : MonoBehaviour {

	private  static GameObject gbCopy;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator makeAFloor(){
		Debug.Log ("what");
		int i = Random.Range (0, FloorManager._instanceFloorManger.floors.Length - 1);
		GameObject gbCopy = GameObject.Instantiate (FloorManager._instanceFloorManger.floors [i],
			new Vector3 (0, 0, FloorManager._instanceFloorManger.floorCount * 449), Quaternion.identity,
			FloorManager._instanceFloorManger.Parent) as GameObject;
		FloorManager._instanceFloorManger.floorCount++;
		FloorManager.garbage.Add (gbCopy);
		yield return new WaitForEndOfFrame ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "body") {
			StartCoroutine (makeAFloor ());
			Debug.Log ("添加完成");
		}
	}

	void OnTriggerExit(Collider other){
		Debug.Log ("Out");
	}
}