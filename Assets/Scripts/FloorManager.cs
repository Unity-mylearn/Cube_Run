using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FloorManager : MonoBehaviour {

	public static FloorManager _instanceFloorManger;
	public GameObject[] floors;
	public Transform Parent;
	public static List<GameObject> garbage = new List<GameObject>();

	public int floorCount = 1;
	public static bool gameStart = false;
	void Awake(){
		_instanceFloorManger = this;
	}
	// Use this for initialization
	void Start () {
		GameObject gb = Instantiate (
			(Object)floors[0],
			new Vector3 (0, 0, 0),
			Quaternion.identity,
			Parent) as GameObject;
		garbage.Add (gb);
		gameStart = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (garbage.Count > 2) {
			Destroy (garbage [0].gameObject);
			garbage.RemoveAt (0);
		}
	}

}
