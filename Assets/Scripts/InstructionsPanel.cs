using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class InstructionsPanel : MonoBehaviour {

	public Text instruction;
	public Image iconImage;
	public Button button1;
    public Button button2;
    public Button button3;
    public GameObject instructionsPanelObject;

	private static InstructionsPanel instructionsPanel;

	public static InstructionsPanel Instance () {
		if (!instructionsPanel) {
			instructionsPanel = FindObjectOfType(typeof (InstructionsPanel)) as InstructionsPanel;
			if (!instructionsPanel)
				Debug.LogError ("There needs to be one active InstructionsPanel script on a GameObject in your scene.");
		}

		return instructionsPanel;
	}

	public void Choice (string instruction, string path, UnityAction event1, UnityAction event2, UnityAction event3) {
		instructionsPanelObject.SetActive (true);

		button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener (event1);
        button1.onClick.AddListener (ClosePanel);

        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(event2);
        button2.onClick.AddListener(ClosePanel);

        button3.onClick.RemoveAllListeners();
        button3.onClick.AddListener(event3);
        button3.onClick.AddListener(ClosePanel);

        this.instruction.text = instruction;
        byte[] data = Resources.Load<TextAsset>(path).bytes;
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(data);
        this.iconImage.sprite = Sprite.Create(tex, new Rect(.0f, .0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
	}

	void ClosePanel () {
		instructionsPanelObject.SetActive (false);
	}
}