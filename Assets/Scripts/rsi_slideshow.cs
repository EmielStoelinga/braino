﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class rsi_slideshow : MonoBehaviour {

	public Image rsiImage;
	public List<Sprite> images = new List<Sprite>();
	public float showspeed;
	public int imagecount;
	private List<Sprite> slideshow = new List<Sprite>();
	private int index = 0;
	private float timer = 0f;

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		while (slideshow.Count < imagecount) {
			int randomindex = Random.Range(0, images.Count);
			if (!slideshow.Contains (images [randomindex])) {
				slideshow.Add (images[randomindex]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= showspeed && index < slideshow.Count-1) {
			timer = 0f;
			index++;
		}

		rsiImage.sprite = slideshow [index];
	}
}