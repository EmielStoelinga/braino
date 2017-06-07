using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
//using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

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

	public float score1;
	public float score2;
	public float score3;
	public float score4;

	private Scene active;
	private bool goBack = false;

	private ModalPanel modalPanel;
    private ModalPanel socialPanel;
    public GameObject UIPanelObject;

	private UnityAction backAction;
	private UnityAction cancelAction;

	private Text recommendationText;

	private float avgScore;
	private int AIchoice = 0; // 0 = no AI, 1 is AI
	private string userIdString = "4";

	void Start () {
		//Screen.orientation = ScreenOrientation.Portrait;
		puzzle.onClick.AddListener(Puzzle);
		rsi.onClick.AddListener(RSI);
		run.onClick.AddListener(Run);
		social.onClick.AddListener(Social);

		recommendationText = GameObject.Find ("RecommendationText").GetComponent<Text> ();

		DontDestroyOnLoad (transform.gameObject);
		active = SceneManager.GetSceneByName("test");

		scorebar1.maxValue = 100;
		scorebar2.maxValue = 100;
		scorebar3.maxValue = 100;
		scorebar4.maxValue = 100;
		if (PlayerPrefs.HasKey ("userID")) {
            PlayerPrefs.GetString("userID", userIdString);
            CalculateScores();
        } else {
            userIdString = Random.Range(0, 1000000).ToString();
            PlayerPrefs.SetString("userID", userIdString);
			
            score1 = 50;
		    score2 = 50;
		    score3 = 50;
		    score4 = 50;
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
		CheckRecommendation ();

        if (Input.GetKeyDown("escape") && active != SceneManager.GetSceneByName("test"))
        {
            modalPanel.Choice("Do you want to stop?", backAction, cancelAction);
        }
    }

	void CheckRecommendation () {
		if (PlayerPrefs.HasKey ("recommendationTimestamp") && Time.time - PlayerPrefs.GetFloat ("recommendationTimestamp") > 60) {
			// query for recommendation
			StartCoroutine (UploadAI (userIdString));
			PlayerPrefs.SetFloat ("recommendationTimestamp", Time.time);
		} else if (!PlayerPrefs.HasKey ("recommendationTimestamp")) {
			// query for first recommendation
			StartCoroutine (UploadAI (userIdString)); //log code
			PlayerPrefs.SetFloat ("recommendationTimestamp", Time.time);
		} else if (recommendationText.text == "RECOMMENDATION") {
			StartCoroutine (UploadAI (userIdString)); //log code
			PlayerPrefs.SetFloat ("recommendationTimestamp", Time.time);
		}
	}

	void UpdateBrainoImages() {
		avgScore = (score1 + score2 + score3 + score4) / 4;
		if (avgScore >= 90.0) {

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

        modalPanel.Choice ("Relax your brain and solve a puzzle. Move a tile by tapping on it.", cancelAction, null);
		SceneManager.LoadScene ("Puzzlescene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("Puzzlescene");
		UIPanelObject.SetActive (false);
	}

	void RSI () {
		Debug.Log ("Clicked RSI");

        modalPanel.Choice ("Relax your brain and your body by doing some exercises. Follow the photo instructions.", cancelAction, null);
		SceneManager.LoadScene ("RSIscene", LoadSceneMode.Additive);
		active = SceneManager.GetSceneByName("RSIscene");
		UIPanelObject.SetActive (false);
	}

	void Run () {
		Debug.Log ("Clicked run");

        modalPanel.Choice ("Get focused by playing a game. Tap to jump, hold to jump higher.", cancelAction, null);
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
	
	IEnumerator Upload(string user, string activity, float s1, float s2, float s3, float s4 ) {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
		form.AddField("query", activity);
		form.AddField("s1", (int) s1);
		form.AddField("s2", (int) s2);
		form.AddField("s3", (int) s3);
		form.AddField("s4", (int) s4); 
 
		WWW www = new WWW("http://eireenwestland.ruhosting.nl/braino/braino.php", form);
        yield return www;
 
        if(www.error != null) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log("Form upload complete!");
        }
    }
	
		// 		AI aanroepen:	StartCoroutine(UploadAI(userIdString)); //log code

	IEnumerator UploadAI(string user) {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
		form.AddField("query", "getSuggestion");
 
		WWW www = new WWW("http://eireenwestland.ruhosting.nl/braino/braino.php", form);
        yield return www;
 
        if(www.error != null) {
            Debug.Log(www.error);
        } else {
            Debug.Log("AI request complete!");
			string suggestion = "Welcome back! Wanna play a game?";
			if(www.text == "logFocus"){
				suggestion = "Focus yourself with the running game!";
			} else if (www.text == "logRSI"){
				suggestion = "Prevent RSI, do some exercises with me!";
			} else if (www.text == "logPuzzle"){
				suggestion = "Relax while solving a fun puzzle!";
			} else if (www.text == "logSocial"){
				suggestion = "Did you already compliment someone today?";
			}
				
			recommendationText.text = suggestion;
			//instructionsPanel.Choice (suggestion, cancelAction);
        }
		
    }
	
	//kiest wel/niet AI voor user
	IEnumerator setAIVar(string user) {
        WWWForm form = new WWWForm();
        form.AddField("user", user);
		form.AddField("query", "initUser");
 
		WWW www = new WWW("http://eireenwestland.ruhosting.nl/braino/braino.php", form);
        yield return www;
 
        if(www.error != null) {
            Debug.Log(www.error);
        } else {
            Debug.Log("AI choice request complete!");
			
			if(www.text == "1"){
				AIchoice = 1;
			}
			
        }
		
    }
	
	
	

	public void Back (float score) {
		Debug.Log ("Back");
		goBack = true;

		if (active == SceneManager.GetSceneByName("Puzzlescene")) {
			score1 += score;
			StartCoroutine(Upload(userIdString, "logPuzzle", score1, score2, score3, score4)); //log code

		} else if (active == SceneManager.GetSceneByName("RSIscene")) {
			score2 += score;
			StartCoroutine(Upload(userIdString, "logRSI", score1, score2, score3, score4)); //log code

		} else if (active == SceneManager.GetSceneByName("EmielRunscene")) {
			score3 += score;
			StartCoroutine(Upload(userIdString, "logFocus", score1, score2, score3, score4)); //log code

		} else if (active == SceneManager.GetSceneByName("Socialscene")) {
			score4 += score;
			StartCoroutine(Upload(userIdString, "logSocial", score1, score2, score3, score4)); //log code

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
