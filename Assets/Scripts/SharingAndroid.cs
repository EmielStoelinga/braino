using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SharingAndroid : MonoBehaviour {

    public float reward;
    public List<byte> images = new List<byte>();

    private float score1;
    private float score2;
    private float score3;
    private float score4;
    private float avgScore;

    public void Start()
    {
        score1 = GameObject.Find("Game").GetComponent<game_script>().score1;
        score2 = GameObject.Find("Game").GetComponent<game_script>().score2;
        score3 = GameObject.Find("Game").GetComponent<game_script>().score3;
        score4 = GameObject.Find("Game").GetComponent<game_script>().score4;

        avgScore = (score1 + score2 + score3 + score4) / 4;

        if (avgScore >= 90)
        {
            avgScore = 90;
        }
        else if (avgScore >= 80)
        {
            avgScore = 80;
        }
        else if (avgScore >= 70)
        {
            avgScore = 70;
        }
        else if (avgScore >= 60)
        {
            avgScore = 60;
        }
        else if (avgScore >= 50)
        {
            avgScore = 50;
        }
        else if (avgScore >= 40)
        {
            avgScore = 40;
        }
        else if (avgScore >= 30)
        {
            avgScore = 30;
        }
        else if (avgScore >= 20)
        {
            avgScore = 20;
        }
        else
        {
            avgScore = 10;
        }

        byte[] dataToSave = Resources.Load<TextAsset>(avgScore.ToString()).bytes;
        string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".png");
        Debug.Log(destination);
        File.WriteAllBytes(destination, dataToSave);
        shareImage("Braino", "Braino", "Good job!", destination);
        GameObject.Find("Game").GetComponent<game_script>().Back(reward);
    }

    public static void shareImage(string subject, string title, string message, string imagePath)
    {
    #if UNITY_ANDROID

        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        intentObject.Call<AndroidJavaObject>("setType", "image/png");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), title);
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), message);

        AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
        AndroidJavaClass fileClass = new AndroidJavaClass("java.io.File");

        AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + imagePath);

        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("startActivity", intentObject);

    #endif
    }
}
