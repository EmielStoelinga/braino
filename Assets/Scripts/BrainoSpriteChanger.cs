using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainoSpriteChanger : MonoBehaviour {

	public Texture[] images;
	private RawImage image;

	private float score1;
	private float score2;
	private float score3;
	private float score4;
	private float avgScore;

	// Use this for initialization
	void Start () {
		image = gameObject.GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
		score1 = GameObject.Find ("Game").GetComponent<game_script> ().score1;
		score2 = GameObject.Find ("Game").GetComponent<game_script> ().score2;
		score3 = GameObject.Find ("Game").GetComponent<game_script> ().score3;
		score4 = GameObject.Find ("Game").GetComponent<game_script> ().score4;

		avgScore = (score1 + score2 + score3 + score4) / 4;

		if (avgScore >= 90) {
			image.texture = images[8];
		} else if (avgScore >= 80) {
			image.texture = images[7];
		} else if (avgScore >= 70) {
			image.texture = images[6];
		} else if (avgScore >= 60) {
			image.texture = images[5];
		} else if (avgScore >= 50) {
			image.texture = images[4];
		} else if (avgScore >= 40) {
			image.texture = images[3];
		} else if (avgScore >= 30) {
			image.texture = images [2];
		} else if (avgScore >= 20) {
			image.texture = images[1];
		} else {
			image.texture = images[0];
		}
		var height = (int)(.5 * Screen.height);
		var width = height; // To make it square

		image.GetComponent<RectTransform> ().sizeDelta = new Vector2(width, height);

	}
}
