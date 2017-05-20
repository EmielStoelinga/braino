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

	private float score1;
	private float score2;
	private float score3;
	private float score4;

	private Scene active;
	private bool goBack = false;

	private ModalPanel modalPanel;
	public GameObject UIPanelObject;

	private UnityAction backAction;
	private UnityAction cancelAction;

	private float lastTime = 0;

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
		if (PlayerPrefs.HasKey ("timestamp")) {
			CalculateScores();
        } else {
		    score1 = 50;
		    score2 = 50;
		    score3 = 50;
		    score4 = 50;
            lastTime = Time.time;
            LogScores();
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
		PlayerPrefs.SetFloat("score1", score1);
		PlayerPrefs.SetFloat("score2", score2);
		PlayerPrefs.SetFloat("score3", score3);
		PlayerPrefs.SetFloat("score4", score4);
		PlayerPrefs.SetFloat("timestamp", Time.time);
	}

	void CalculateScores() {
		float elapsed_seconds = Time.time - PlayerPrefs.GetFloat("timestamp");
		score1 = PlayerPrefs.GetFloat("score1") - (elapsed_seconds / decreaseScoreAfterSeconds);
		score2 = PlayerPrefs.GetFloat("score2") - (elapsed_seconds / decreaseScoreAfterSeconds);
		score3 = PlayerPrefs.GetFloat("score3") - (elapsed_seconds / decreaseScoreAfterSeconds);
		score4 = PlayerPrefs.GetFloat("score4") - (elapsed_seconds / decreaseScoreAfterSeconds);
		score1 = Mathf.Clamp(score1, 0, 100);
		score2 = Mathf.Clamp(score2, 0, 100);
		score3 = Mathf.Clamp(score3, 0, 100);
		score4 = Mathf.Clamp(score4, 0, 100);
	}

	void Update () {
        CalculateScores();
        LogScores ();

        if (Input.GetKeyDown("escape") && active != SceneManager.GetSceneByName("test"))
        {
            modalPanel.Choice("Do you want to stop?", backAction, cancelAction);
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
			score1 += score;
		} else if (active == SceneManager.GetSceneByName("RSIscene")) {
			score2 += score;
		} else if (active == SceneManager.GetSceneByName("EmielRunscene")) {
			score3 += score;
		} else if (active == SceneManager.GetSceneByName("Socialscene")) {
			score4 += score;
		}
        LogScores();
    }

	void BackFunction () {
		Back (0);
	}

	void CancelFunction () {
		return;
	}
}
