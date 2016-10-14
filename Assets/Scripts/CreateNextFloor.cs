using UnityEngine;
using System.Collections;

public class CreateNextFloor : MonoBehaviour {

	private  static GameObject gbCopy;
	[SerializeField]
	private float length;
	public static bool hasAdd = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator makeAFloor(){
		hasAdd = true;
		int i = Random.Range (0, FloorManager._instanceFloorManger.floors.Length - 1);
		GameObject gbCopy = GameObject.Instantiate (FloorManager._instanceFloorManger.floors [i],
			new Vector3 (0, 0, /*FloorManager._instanceFloorManger.floorCount * length*/transform.parent.localPosition.z + length), Quaternion.identity,
			FloorManager._instanceFloorManger.Parent) as GameObject;
		FloorManager._instanceFloorManger.floorCount++;
		FloorManager.garbage.Add (gbCopy);
		yield return new WaitForEndOfFrame ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.name == "StoneKing" && !hasAdd) {
			StartCoroutine (makeAFloor ());
			Debug.Log ("添加完成");
		}
	}

	void OnTriggerExit(Collider other){
		Debug.Log ("Out");
		hasAdd = false;
	}
}