using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class InstructionsPanel : MonoBehaviour {

	public Text instruction;
	public Image iconImage;
	public Button okButton;
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

	public void Choice (string instruction, UnityAction okEvent) {
		instructionsPanelObject.SetActive (true);

		okButton.onClick.RemoveAllListeners();
		okButton.onClick.AddListener (okEvent);
		okButton.onClick.AddListener (ClosePanel);

		this.instruction.text = instruction;

		this.iconImage.gameObject.SetActive (false);
		okButton.gameObject.SetActive (okEvent != null);

		Time.timeScale = 0; //Pause the game
	}

	void ClosePanel () {
		Time.timeScale = 1; //Resume the game
		instructionsPanelObject.SetActive (false);
	}
}