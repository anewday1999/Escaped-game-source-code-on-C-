//****** Donations are greatly appreciated.  ******
//****** You can donate directly to Jesse through paypal at  https://www.paypal.me/JEtzler   ******

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserMovement : MonoBehaviour {
	public bool jump;
	public InstantiateRotate InsRotate;
	//private float timeSlideDead = 1;
	public float cooldowmTime = 1f;
	//private float theNextTime = 0;
	public float jumpSpeed = 9.0f;
	public float gravity = 20.0f;
	public float runSpeed = 5.5f;
	public float runSpeed1 = 5.5f;
	public float runSpeed2 = 5.5f;
	//private float walkSpeed = 90.0f;
	private float rotateSpeed = 7.0f;
	
	public bool grounded;
	private Vector3 moveDirection = Vector3.zero;
	//private bool isWalking;
	//private string moveStatus = "idle";

	public GameObject maincamera;
	public CharacterController controller;
	public bool isJumping;
	private float myAng = 0.0f;
	public bool canJump = true;
	public float joyHorizontal;
	public float joyVertical;

	void Start () {

		controller = GetComponent<CharacterController>();
	}

	void Update ()
	{
		//force controller down slope. Disable jumping
		if (myAng > 50) {

			canJump = false;
		}
		else {

			canJump = true;
		}

		if(grounded) 
		{
				
				isJumping = false;
				
				if(maincamera.gameObject.GetComponent<UserCamera>().inFirstPerson == true) {
					moveDirection = new Vector3((( (InsRotate.TouchDist.x != 0 || InsRotate.TouchDist.y != 0) ? joyHorizontal : 0) * 65) / 100 ,0,joyVertical);
				}
				moveDirection = new Vector3((joyHorizontal * 65) / 100 ,0,joyVertical);
					
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= runSpeed;

			//moveStatus = "idle";
			/*
			if(moveDirection != Vector3.zero)
				moveStatus = "running";*/
				if (jump && canJump) {		
					moveDirection.y = jumpSpeed;
					isJumping = true;
					jump = false;
				}
				
		}
			
			
			// Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.
			
			if(maincamera.transform.gameObject.transform.GetComponent<UserCamera>().inFirstPerson == false) {
				if((InsRotate.TouchDist.x != 0 || InsRotate.TouchDist.y != 0)) {
					transform.rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
				}
			}
			else {
				if((InsRotate.TouchDist.x != 0 || InsRotate.TouchDist.y != 0)) {
					transform.rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
				}
				
			}
			
			//Apply gravity
			moveDirection.y -= gravity * Time.deltaTime;
			
			
			//Move controller
			CollisionFlags flags;
			if ((InsRotate.TouchDist.x == 0 && InsRotate.TouchDist.y == 0) && joyHorizontal != 0)
				{
					transform.Rotate(0,joyHorizontal * rotateSpeed * Time.deltaTime, 0);
				}
			if(isJumping) {
				flags = controller.Move(moveDirection * Time.deltaTime);
			}
			else {
				flags = controller.Move((moveDirection + new Vector3(0,-100,0)) * Time.deltaTime);
			}
			
			grounded = (flags & CollisionFlags.Below) != 0;

	}
	void LateUpdate() //in fight
	{
		/*
		if (Input.GetMouseButton (1))
		{
			runSpeed = 1.5f;
			if (timeSlideDead >= Time.time)
			{
				runSpeed = 3.5f;
			}
		}
		else if (Input.GetMouseButtonUp (1))
		{
			if (timeSlideDead >= Time.time)
			{
				runSpeed = 5.5f * 2.5f;
			}
			else
			{
				runSpeed = 5.5f;
			}
		}
		else
		{
			
			if (Input.GetButtonDown("slide"))
			{
					if (theNextTime <= Time.time)
					{
						theNextTime = Time.time + cooldowmTime;
						runSpeed = 5.5f * 2.5f;
						timeSlideDead = Time.time + 5.5f;
					}
			}
			else if (timeSlideDead <= Time.time)
			{
				runSpeed = 5.5f;
			}
		}*/
		if (FindObjectOfType<MatchManager>().isRespawn)
        {
			Debug.Log("respawn");
			transform.position = FindObjectOfType<MatchManager>().tarSpawnspot.transform.position;
			transform.rotation = FindObjectOfType<MatchManager>().tarSpawnspot.transform.rotation;
			FindObjectOfType<MatchManager>().isRespawn = false;

		}
		
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {

			myAng = Vector3.Angle(Vector3.up, hit.normal); 
	}
}