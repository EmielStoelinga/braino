using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class run_game : MonoBehaviour {
	private float speed = 10.0f;
	private float grav = 100.0f;
	private float chance = 1;
	private int maxobstacles = 4;
	private List<GameObject> obstacles;
	private List<GameObject> removes;
	public GameObject obstacle;

	private float camh;
	private float camw;
	private float blocktime = 0;
	private float mintime = 0.5f;
	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("Runscene"));
		obstacles = new List<GameObject> ();
		removes = new List<GameObject> ();
		camh = Camera.main.orthographicSize;
		camw = camh * Camera.main.aspect;
		Physics.gravity = new Vector3 (0, -grav, 0);
		Instantiate (obstacle);
	}
	
	// Update is called once per frame
	void Update () {
		blocktime += Time.deltaTime;
		if (Random.Range (0, 100) <= chance && obstacles.Count < maxobstacles && blocktime > mintime) {
			obstacles.Add( Instantiate (obstacle));
			blocktime = 0;
		}

		foreach(GameObject t in obstacles) {
			t.transform.Translate (new Vector3(-speed * Time.deltaTime, 0, 0));
			if (t.transform.position.x < -camw) {
				removes.Add(t);
			}
		}

		foreach(GameObject t in removes) {
			if (Mathf.Abs (t.transform.position.x) > camw) {
				obstacles.Remove(t);
				Destroy (t);
			}
		}

		removes.Clear();
	}
}
