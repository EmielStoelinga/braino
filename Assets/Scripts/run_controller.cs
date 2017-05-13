using UnityEngine;
using System.Collections;

public class run_controller : MonoBehaviour {
	private Rigidbody rb;
	private float jumpSpeed = 1000;
	private bool canJump = false;
	private float time = 0;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (canJump) {
			if (((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)
			    || Input.GetMouseButtonDown (0))) {
				rb.AddForce(new Vector3(0, jumpSpeed), 0);
				canJump = false;
			}
		}

		time += Time.deltaTime;
	}

	void OnCollisionEnter(Collision col) {
		Debug.Log ("collision!");
		if (col.gameObject.tag == "Obstacle") {
			GameObject.Find ("Game").GetComponent<game_script> ().Back (time);
		} else if (col.gameObject.tag == "Ground") {
			canJump = true;
		}
	}
}
