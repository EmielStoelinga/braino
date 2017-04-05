using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class game_script : MonoBehaviour {
	public Button puzzle;
	public Button rhythm;
	public Button run;
	public Button social;
	// Use this for initialization
	void Start () {
		puzzle.onClick.AddListener(Puzzle);
		rhythm.onClick.AddListener(Rhythm);
		run.onClick.AddListener(Run);
		social.onClick.AddListener(Social);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Puzzle () {
		Debug.Log ("Clicked puzzle");
	}

	void Rhythm () {
		Debug.Log ("Clicked rhythm");
	}

	void Run () {
		Debug.Log ("Clicked run");
	}

	void Social () {
		Debug.Log ("Clicked social");
	}
}
