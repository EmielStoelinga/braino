using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerControl : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;
	public float walkJumpForce;

	public float jumpTime;
	private float jumpTimeCounter;

	private Rigidbody2D myRigidbody;

	private bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;

	public GameManager theGameManager;

	private Collider2D myCollider;

	public ScoreManager myScoreManager;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();

		myCollider = GetComponent<Collider2D> ();

		jumpTimeCounter = jumpTime;
	}
	
	// Update is called once per frame
	void Update () {

		// grounded = Physics2D.IsTouchingLayers (myCollider, whatIsGround);

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		if (grounded && Physics2D.IsTouchingLayers(myCollider, whatIsGround)) {
			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, walkJumpForce);
		}

		int fingerCount = 0;
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
				fingerCount++;
		}

		if (fingerCount == 1 || Input.GetMouseButtonDown(0)) {
			if (jumpTimeCounter > 0) {
				myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}

		if (fingerCount == 0) {
			jumpTimeCounter = 0;
		}

		if (grounded) {
			jumpTimeCounter = jumpTime;
		}

		myRigidbody.velocity = new Vector2 (moveSpeed, myRigidbody.velocity.y);	
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "killbox") {
			if (myScoreManager.scoreCount > 10) {
				GameObject.Find ("Game").GetComponent<game_script> ().Back (myScoreManager.scoreCount);
			} else {
				GameObject.Find ("Game").GetComponent<game_script> ().Back (0);
			}
		}
	}
}
