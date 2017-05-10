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

	public GameObject UIcanvas;

	private int score1 = 100;
	private int score2 = 100;
	private int score3 = 100;
	private int score4 = 100;

	private Scene active;
	private bool goBack = false;

	private float timer = 1;
	void Start () {
		Screen.orientation = ScreenOrientation.Portrait;
		puzzle.onClick.AddListener(Puzzle);
		rhythm.onClick.AddListener(Rhythm);
		run.onClick.AddListener(Run);
		social.onClick.AddListener(Social);
		DontDestroyOnLoad (transform.gameObject);
		active = SceneManager.GetSceneByName("test");
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

		if (Input.GetKeyDown("escape")) {
			Back (0);
		}
	}

	void LateUpdate() {
		if (goBack) {
			goBack = false;
			UIcanvas.SetActive (true);
			SceneManager.UnloadScene(active);
			active = SceneManager.GetSceneByName("test");
		}
	}

	void Puzzle () {
		Debug.Log ("Clicked puzzle");
		SceneManager.LoadScene ("Puzzlescene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("Puzzlescene");
		UIcanvas.SetActive (false);
	}

	void Rhythm () {
		Debug.Log ("Clicked rhythm");
		SceneManager.LoadScene ("Rythmscene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("Rythmscene");
		UIcanvas.SetActive (false);
	}

	void Run () {
		Debug.Log ("Clicked run");
		SceneManager.LoadScene ("Runscene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("Runscene");
		UIcanvas.SetActive (false);
	}

	void Social () {
		Debug.Log ("Clicked social");
		SceneManager.LoadScene ("Socialscene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("Socialscene");
		UIcanvas.SetActive (false);
	}

	public void Back (float score) {
		Debug.Log ("Back");
		goBack = true;

		if (active == SceneManager.GetSceneByName("Puzzlescene")) {
			score1 += (int)score;
		} else if (active == SceneManager.GetSceneByName("Rythmscene")) {
			score2 += (int)score;
		} else if (active == SceneManager.GetSceneByName("Runscene")) {
			score3 += (int)score;
		} else if (active == SceneManager.GetSceneByName("Socialscene")) {
			score4 += (int)score;
		}
	}
}
