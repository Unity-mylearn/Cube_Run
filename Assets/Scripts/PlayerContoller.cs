using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerContoller : MonoBehaviour {


	public static PlayerContoller _instance;
	[SerializeField]
	private Transform player;

	[SerializeField]
	private Text speed_text;
	[SerializeField]
	private Text time_text;
	[SerializeField]
	private Slider blood_slider;
	[SerializeField]
	private Slider energy_slider;

	[SerializeField]
	private float stepfar;
	[SerializeField]
	private float Thust;
	[SerializeField]
	private float SpeedAddRate;
	[SerializeField]
	private float SpeedUpVar;

#region mutiplay data;
	public float speed { get; set;}
	public float currentTime { get; set;}
	public float currentBlood { get; set; }
	public float currentEnergy { get; set; }
#endregion
#region need record data;
	public float Totalmileage { get; set;}
	public float TotalTime { get; set;}
	public float successProbabily { get; set;}
	public int passObstruct { get ; set;}
#endregion

	private bool isTouch = false;
	private bool isDie = false;
	private bool isJump = false;

	private Vector3 beginPosition;
	private Vector3 endPosition;
	private float currentX;
	private float currentY;
	private float currentZ;

	private float speedUpBeginTime = 0.0f;
	private float speedUpLastTime = 0.1f;


	void Awake(){
		_instance = this;
		speed = 10;
		currentTime = 0.0f;
		currentBlood = 100;
		currentEnergy = 100;

		speed_text.text = speed.ToString ();
		time_text.text = currentTime.ToString ();
		blood_slider.value = currentBlood;
		energy_slider.value = currentEnergy;
	}
	// Use this for initialization
	void Start () {
		// load data from local record;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDie)
			return;
		if (!FloorManager.gameStart)
			return;
		if (Input.GetMouseButtonDown (0)) {
			isTouch = true;
			beginPosition = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0) && isTouch) {
			isTouch = false;
			endPosition = Input.mousePosition;

			if (Vector3.Distance (beginPosition, endPosition) < 0.1f && !isJump) {
//				Debug.Log ("Player jump");
				isJump = true;
				GetComponentInChildren<Rigidbody> ().AddForce (transform.up * Thust);
			}
			AnalyzeSwipe (beginPosition, endPosition);
		}
		UpdateTime ();
		if (speedUpLastTime - speedUpBeginTime >= SpeedAddRate) {
			UpdateSpeed (SpeedUpVar);
		}
		currentX = transform.position.x;
		currentY = transform.position.y;
		currentZ = transform.position.z;
		if (currentY <= 2.0f) {
			isJump = false;
		}
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
//			Debug.Log ("down");
			player.Translate(new Vector3(0,-stepfar,0));
		} else if (angle > 22.5 && angle <= 67.5) {
//			Debug.Log ("down-right");
		} else if (angle > 67.5 && angle <= 112.5) {
//			Debug.Log ("right");
			player.Translate(new Vector3(stepfar,0,0));
		} else if (angle > 112.5 && angle <= 157.5) {
//			Debug.Log ("up-right");
		} else if (angle > 157.5 && angle <= 202.5) {
//			Debug.Log ("up");
		} else if (angle > 202.5 && angle <= 247.5) {
//			Debug.Log ("up-left");
		} else if (angle > 247.5 && angle <= 292.5) {
//			Debug.Log ("left");
			player.Translate(new Vector3(-stepfar,0,0));
		} else if (angle > 292.5 && angle <= 337.5) {
//			Debug.Log ("down-left");
		}
		beginPosition = endPosition; 
	}

	public void AddEnergy(int value){
		if (currentEnergy >= 100) {
			currentEnergy = currentEnergy;
			return;
		} else {
			currentEnergy = value;
		}
	}

	public void CostLife(int value){
		if (currentBlood <= 0) {
			currentBlood = 0;
			isDie = true;
			return;
		} else {
			currentBlood -= value;
		}
	}

	/// <summary>
	/// Updates the speed.
	/// Each N second,the speed will add.
	/// </summary>
	public void UpdateSpeed(float value){
//		Debug.LogFormat ("Speed Up {0}.", value);
		speedUpBeginTime = currentTime;
		if (speed >= 200) {
				
		} else {
			speed += value;
		}
		speed_text.text = speed.ToString ();
	}

	public void UpdateTime(){
		currentTime += Time.deltaTime;
		speedUpLastTime = currentTime;
		time_text.text = currentTime.ToString ();
	}
}
