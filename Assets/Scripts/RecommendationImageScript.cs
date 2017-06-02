using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecommendationImageScript : MonoBehaviour {

	private RawImage image;

	// Use this for initialization
	void Start () {
		image = gameObject.GetComponent<RawImage> ();
	}

	// Update is called once per frame
	void Update () {
		var width = (int)(.25 * Screen.width);
		var height = (int)(.2 * Screen.height);

		image.GetComponent<RectTransform> ().sizeDelta = new Vector2(width, height);
	}
}
