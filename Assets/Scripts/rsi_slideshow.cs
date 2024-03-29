﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class rsi_slideshow : MonoBehaviour {

	public List<Sprite> images = new List<Sprite>();
	public float showspeed;
	public int imagecount;

	public int reward;

	private List<Sprite> slideshow = new List<Sprite>();
	private int index = 0;
	private float timer = 0f;

    public Text counter;

	// Use this for initialization
	void Start () {
        //Screen.orientation = ScreenOrientation.Portrait;
        for (int i = 0; i < imagecount; i++) {
			slideshow.Add (images[i]);
		}
		/*
		while (slideshow.Count < imagecount) {
			int randomindex = Random.Range(0, images.Count);
			if (!slideshow.Contains (images [randomindex])) {
				slideshow.Add (images[randomindex]);
			}
		}
		*/
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= showspeed && index < slideshow.Count-1) {
			timer = 0f;
			index++;
		}

		if (timer >= showspeed && index >= slideshow.Count-1) {
			GameObject.Find ("Game").GetComponent<game_script> ().Back (reward);
		}

        GetComponent<SpriteRenderer>().sprite = slideshow [index];

        ResizeSpriteToScreen();

        counter.text = ((int)(showspeed - timer)).ToString();
    }

    void ResizeSpriteToScreen () {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector2((float)worldScreenWidth / width, (float)worldScreenHeight / height);
    }
}
