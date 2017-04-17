using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_script : MonoBehaviour {
	public Button puzzle;
	public Button rhythm;
	public Button run;
	public Button social;

	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;

	private int score1 = 100;
	private int score2 = 100;
	private int score3 = 100;
	private int score4 = 100;

	private Scene active;

	private float timer = 1;
	void Start () {
		puzzle.onClick.AddListener(Puzzle);
		rhythm.onClick.AddListener(Rhythm);
		run.onClick.AddListener(Run);
		social.onClick.AddListener(Social);
		DontDestroyOnLoad (transform.gameObject);
	}

	void OnGUI() {
		text1.text = score1.ToString();
		text2.text = score2.ToString();
		text3.text = score3.ToString();
		text4.text = score4.ToString();
	}

	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			score1 -= 1;
			score2 -= 1;
			score3 -= 1;
			score4 -= 1;
			timer = 1;
		}
		score1 = Mathf.Clamp(score1, 0, 100);
		score2 = Mathf.Clamp(score2, 0, 100);
		score3 = Mathf.Clamp(score3, 0, 100);
		score4 = Mathf.Clamp(score4, 0, 100);
	}

	void Puzzle () {
		Debug.Log ("Clicked puzzle");
		//SceneManager.LoadScene ("Puzzlescene", LoadSceneMode.Additive);
		score1 += 20;
	}

	void Rhythm () {
		Debug.Log ("Clicked rhythm");
		score2 += 20;
	}

	void Run () {
		Debug.Log ("Clicked run");
		score3 += 20;
	}

	void Social () {
		Debug.Log ("Clicked social");
		score4 += 20;
	}
}
