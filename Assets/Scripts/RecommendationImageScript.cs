using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RecommendationImageScript : MonoBehaviour {

	public float percentageWidth;
	public float percentageHeight;

	public float width;
	public float height;

	private RawImage image;

	// Use this for initialization
	void Start () {
		image = gameObject.GetComponent<RawImage> ();

		width = (int)(percentageWidth/100 * Screen.width);
		height = (int)(percentageHeight/100 * Screen.height);

		image.GetComponent<RectTransform> ().sizeDelta = new Vector2(width, height);
	}

	// Update is called once per frame
	void Update () {
	}
}
