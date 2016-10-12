using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private Transform target;

	public float smooth = 0.0f;
	public bool isSmooth = true;

	public float height = 6.0f;
	public float distance = 5.0f;

	public float distanceRotationX = 30.0f;

	private Vector3 nextCameraPosition;
	private Quaternion nextCameraRotation;

	private PlayerContoller pp;

	void Awake(){
		pp = PlayerContoller._instance;
	}
	void Update () {
		smooth = Mathf.Tan(pp.speed / 10.0f);
#region CameraPosition Move.
		Vector3 currentV = transform.position;
		float resultX = target.position.x;
		float resultY = target.position.y + height;
		float resultZ = target.position.z - distance;
		nextCameraPosition = Vector3.Lerp (currentV, new Vector3 (resultX, resultY, resultZ), smooth * Time.deltaTime);
#endregion

#region CameraRotation Move.

		float currentRX = transform.eulerAngles.x;
		float currentRY = transform.eulerAngles.y;
		float currentRZ = transform.eulerAngles.z;

		float resultRotationX = target.eulerAngles.x + distanceRotationX;
		float resultRotationY = target.eulerAngles.y;
		float resultRotationZ = target.eulerAngles.z;

		resultRotationX = Mathf.LerpAngle (currentRX, resultRotationX, smooth * Time.deltaTime);
		resultRotationY = Mathf.LerpAngle (currentRY, resultRotationY, smooth * Time.deltaTime);
		resultRotationZ = Mathf.LerpAngle (currentRZ, resultRotationZ, smooth * Time.deltaTime);
		nextCameraRotation = Quaternion.Euler (resultRotationX, resultRotationY, resultRotationZ);
#endregion

		transform.position = nextCameraPosition;
		transform.rotation = nextCameraRotation;
	}
}