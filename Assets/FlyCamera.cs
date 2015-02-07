using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour 
{
	public GameObject defaultCam;
	public GameObject targetObject;

	public float flySpeed = 0.5f;
	public float rotateSpeed = 2f;

	bool isEnabled;
	//float accelerationAmount = 3f;
	float accelerationRatio = 1f;
	float slowDownRatio = 0.5f;

	void Start()
	{
		if(defaultCam != null) {
			defaultCam.gameObject.camera.enabled = false;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			flySpeed *= accelerationRatio;
		}
		
		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
		{
			flySpeed /= accelerationRatio;
		}

		if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
		{
			flySpeed *= slowDownRatio;
		}
		if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
		{
			flySpeed /= slowDownRatio;
		}

		if (Input.GetAxis("Vertical") != 0)
		{
			transform.Translate(defaultCam.transform.forward * flySpeed * Input.GetAxis("Vertical"));
		}
		if (Input.GetAxis("Horizontal") != 0)
		{
			transform.Translate(defaultCam.transform.right * flySpeed * Input.GetAxis("Horizontal"));
		}

		if (Input.GetKey(KeyCode.C))
		{
			transform.Translate(defaultCam.transform.up * flySpeed*0.5f);
		}
		else if (Input.GetKey(KeyCode.Z))
		{
			transform.Translate(-defaultCam.transform.up * flySpeed*0.5f);
		}
		else if (Input.GetKey(KeyCode.Q))
		{
			transform.RotateAround (targetObject.transform.position, Vector3.up, -rotateSpeed );
		}
		else if (Input.GetKey(KeyCode.E))
		{
			transform.RotateAround( targetObject.transform.position, Vector3.up, rotateSpeed);
		}

		if (Input.GetKeyDown(KeyCode.F12))
			switchCamera();
		//Moves the player to the flycam's position. Make sure not to just move the player's camera.
		if (Input.GetKeyDown(KeyCode.M))
			targetObject.transform.position = transform.position; 
	}

	void switchCamera()
	{
		if(defaultCam == null) {
			Debug.LogWarning("No default camera!");
			return;
		}

		// TODO
		//means it is currently disabled. code will enable the flycam. you can NOT use 'enabled' as boolean's name.
		if (!isEnabled) 
		{
			transform.position = defaultCam.transform.position; //moves the flycam to the defaultcam's position
			defaultCam.camera.gameObject.SetActive(false);
			this.camera.gameObject.SetActive(true);
			isEnabled = true;
		}
		//if it is not disabled, it must be enabled. the function will disable the freefly camera this time.
		else
		{
			this.camera.gameObject.SetActive(false);
			defaultCam.camera.gameObject.SetActive(true);
			isEnabled = false;
		}
	}
}
