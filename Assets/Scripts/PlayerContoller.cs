using UnityEngine;
using System.Collections;

public class PlayerContoller : MonoBehaviour {


	[SerializeField]
	private Transform player;

	public float speed;
	public float stepfar;
	public float Thust;

	private float score = 0.0f;
	private float TotalTime = 0.0f;
	private int passObstruct = 0;
	private float successProbabily = 0.0f;

	private bool isTouch = false;
	private bool isDie = false;
	private bool isJump = false;

	private Vector3 beginPosition;
	private Vector3 endPosition;
	private float currentX;
	private float currentY;
	private float currentZ;
	// Use this for initialization
	void Start () {
		// load data from local record;
	}
	
	// Update is called once per frame
	void Update () {
		if (!FloorManager.gameStart)
			return;
		if (Input.GetMouseButtonDown (0)) {
			isTouch = true;
			beginPosition = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0) && isTouch) {
			isTouch = false;
			endPosition = Input.mousePosition;

			if (Vector3.Distance (beginPosition, endPosition) < 0.1f) {
				Debug.Log ("Player jump");
				GetComponentInChildren<Rigidbody> ().AddForce (transform.up * Thust);
			}
			AnalyzeSwipe (beginPosition, endPosition);
		}
		currentX = transform.position.x;
		currentY = transform.position.y;
		currentZ = transform.position.z;
		transform.position = new Vector3 (currentX, currentY, currentZ + speed * Time.deltaTime * stepfar);
	}

	public void AnalyzeSwipe (Vector2 start, Vector2 end) {
		if (Vector2.Distance (start, end) < 0.1f) {
			return;
		}
		Vector2 direction = end - start;
		float angle;
		float side = end.x > start.x ? 1 : -1;
		float num = Vector2.Angle (direction, -Vector2.up);
		if (side == 1) {
			angle = num;
		} else {
			angle = 360 - num;
		}

		if (angle > 337.5 || angle <= 22.5) {
			Debug.Log ("down");
			player.Translate(new Vector3(0,-stepfar,0));
		} else if (angle > 22.5 && angle <= 67.5) {
			Debug.Log ("down-right");
		} else if (angle > 67.5 && angle <= 112.5) {
			Debug.Log ("right");
			player.Translate(new Vector3(stepfar,0,0));
		} else if (angle > 112.5 && angle <= 157.5) {
			Debug.Log ("up-right");
		} else if (angle > 157.5 && angle <= 202.5) {
			Debug.Log ("up");
		} else if (angle > 202.5 && angle <= 247.5) {
			Debug.Log ("up-left");
		} else if (angle > 247.5 && angle <= 292.5) {
			Debug.Log ("left");
			player.Translate(new Vector3(-stepfar,0,0));
		} else if (angle > 292.5 && angle <= 337.5) {
			Debug.Log ("down-left");
		}
		beginPosition = endPosition; 
	}
}
