using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ModalPanel : MonoBehaviour {

	public Text question;
	public Image iconImage;
	public Button okButton;
	public Button cancelButton;
	public GameObject modalPanelObject;

	private static ModalPanel modalPanel;

	public static ModalPanel Instance () {
		if (!modalPanel) {
			modalPanel = FindObjectOfType(typeof (ModalPanel)) as ModalPanel;
			if (!modalPanel)
				Debug.LogError ("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}

		return modalPanel;
	}
		
	public void Choice (string question, UnityAction okEvent, UnityAction cancelEvent) {
		modalPanelObject.SetActive (true);

		okButton.onClick.RemoveAllListeners();
		okButton.onClick.AddListener (okEvent);
		okButton.onClick.AddListener (ClosePanel);

		cancelButton.onClick.RemoveAllListeners();
		cancelButton.onClick.AddListener (cancelEvent);
		cancelButton.onClick.AddListener (ClosePanel);

		this.question.text = question;

		this.iconImage.gameObject.SetActive (false);
		okButton.gameObject.SetActive (okEvent != null);
		cancelButton.gameObject.SetActive (cancelEvent != null);

		Time.timeScale = 0; //Pause the game
	}

	void ClosePanel () {
		Time.timeScale = 1; //Resume the game
		modalPanelObject.SetActive (false);
	}
}