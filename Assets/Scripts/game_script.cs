using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class game_script : MonoBehaviour {
	public Button puzzle;
	public Button rsi;
	public Button run;
	public Button social;

	public Slider scorebar1;
	public Slider scorebar2;
	public Slider scorebar3;
	public Slider scorebar4;

	public int decreaseScoreAfterSeconds;

	private int score1;
	private int score2;
	private int score3;
	private int score4;

	private Scene active;
	private bool goBack = false;

	private ModalPanel modalPanel;
	public GameObject UIPanelObject;

	private UnityAction backAction;
	private UnityAction cancelAction;

	private float timer = 1;

	void Start () {
		//Screen.orientation = ScreenOrientation.Portrait;
		puzzle.onClick.AddListener(Puzzle);
		rsi.onClick.AddListener(RSI);
		run.onClick.AddListener(Run);
		social.onClick.AddListener(Social);

		DontDestroyOnLoad (transform.gameObject);
		active = SceneManager.GetSceneByName("test");

		scorebar1.maxValue = 100;
		scorebar2.maxValue = 100;
		scorebar3.maxValue = 100;
		scorebar4.maxValue = 100;
		if (PlayerPrefs.HasKey ("score1")) {
			CalculateScores ();
		} else {
			score1 = 100;
			score2 = 100;
			score3 = 100;
			score4 = 100;
		}
	}

	void Awake () {
		modalPanel = ModalPanel.Instance ();

		backAction = new UnityAction (BackFunction);
		cancelAction = new UnityAction (CancelFunction);
	}

	void OnGUI() {
		scorebar1.value = score1;
		scorebar2.value = score2;
		scorebar3.value = score3;
		scorebar4.value = score4;

		scorebar1.transform.Find("Fill Area").GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, (float)score1/100f);
		scorebar2.transform.Find("Fill Area").GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, (float)score2/100f);
		scorebar3.transform.Find("Fill Area").GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, (float)score3/100f);
		scorebar4.transform.Find("Fill Area").GetComponentInChildren<Image>().color = Color.Lerp(Color.red, Color.green, (float)score4/100f);
	}

	void LogScores() {
		PlayerPrefs.SetInt ("score1", score1);
		PlayerPrefs.SetInt ("score2", score2);
		PlayerPrefs.SetInt ("score3", score3);
		PlayerPrefs.SetInt ("score4", score4);

		System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
		PlayerPrefs.SetInt ("timestamp", cur_time);
	}

	void CalculateScores() {
		System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
		int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
		int elapsed_seconds = cur_time - PlayerPrefs.GetInt ("timestamp");
		score1 = PlayerPrefs.GetInt ("score1") - (elapsed_seconds / decreaseScoreAfterSeconds);
		score2 = PlayerPrefs.GetInt ("score2") - (elapsed_seconds / decreaseScoreAfterSeconds);
		score3 = PlayerPrefs.GetInt ("score3") - (elapsed_seconds / decreaseScoreAfterSeconds);
		score4 = PlayerPrefs.GetInt ("score4") - (elapsed_seconds / decreaseScoreAfterSeconds);
	}

	void Update () {
		timer -= Time.deltaTime;
		if (timer <= 0) {
			score1 -= 1;
			score2 -= 1;
			score3 -= 1;
			score4 -= 1;
			timer = decreaseScoreAfterSeconds;
			LogScores ();
		}
		score1 = Mathf.Clamp(score1, 0, 100);
		score2 = Mathf.Clamp(score2, 0, 100);
		score3 = Mathf.Clamp(score3, 0, 100);
		score4 = Mathf.Clamp(score4, 0, 100);

		if (Input.GetKeyDown("escape")) {
			modalPanel.Choice ("Do you want to stop?", backAction, cancelAction);
		}
	}

	void LateUpdate() {
		if (goBack) {
			goBack = false;
			UIPanelObject.SetActive (true);
			SceneManager.UnloadScene(active);
			active = SceneManager.GetSceneByName("test");
		}
	}

	void Puzzle () {
		Debug.Log ("Clicked puzzle");
		SceneManager.LoadScene ("Puzzlescene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("Puzzlescene");
		UIPanelObject.SetActive (false);
	}

	void RSI () {
		Debug.Log ("Clicked RSI");
		SceneManager.LoadScene ("RSIscene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("RSIscene");
		UIPanelObject.SetActive (false);
	}

	void Run () {
		Debug.Log ("Clicked run");
		SceneManager.LoadScene ("EmielRunscene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("EmielRunscene");
		UIPanelObject.SetActive (false);
	}

	void Social () {
		Debug.Log ("Clicked social");
		SceneManager.LoadScene ("Socialscene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("Socialscene");
		UIPanelObject.SetActive (false);
	}

	public void Back (float score) {
		Debug.Log ("Back");
		goBack = true;

		if (active == SceneManager.GetSceneByName("Puzzlescene")) {
			score1 += (int)score;
		} else if (active == SceneManager.GetSceneByName("RSIscene")) {
			score2 += (int)score;
		} else if (active == SceneManager.GetSceneByName("EmielRunscene")) {
			score3 += (int)score;
		} else if (active == SceneManager.GetSceneByName("Socialscene")) {
			score4 += (int)score;
		}
	}

	void BackFunction () {
		Back (0);
	}

	void CancelFunction () {
		return;
	}
}
