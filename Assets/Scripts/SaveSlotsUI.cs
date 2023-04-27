using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsUI : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private SaveLoadGame.SAVE_SLOT _buttonSlot;

	[Header("UI elements")]
	[SerializeField] private TextMeshProUGUI _totalTPText;
	[SerializeField] private TextMeshProUGUI _progressText;
	[SerializeField] private Button _deleteButton;

	private Button _selfButton;

	private void Awake()
	{
		_selfButton = GetComponent<Button>();
	}

	private void Start()
	{
		string totalTP = "";
		string progress = "";

		SaveLoadGame.SaveData currentSaveData = SaveLoadGame.LoadSlot(_buttonSlot);
		if (currentSaveData != null)
		{
			totalTP = LevelData.TPFormatting(currentSaveData.Total);
			progress = currentSaveData.LevelProgress.ToString();
		}
		else
		{
			totalTP = "--:--:--";
			progress = "-";

			_deleteButton.gameObject.SetActive(false);
		}

		_totalTPText.SetText("Time: \n" + totalTP);
		_progressText.SetText("Progress: " + progress);
	}

	public void LoadSlotIntoGame()
	{
		SaveLoadGame.LoadIntoGame(_buttonSlot);
		SceneManager.LoadScene(GameAssets.Instance.scene_MainMenu);
	}

	public void DeleteSaveSlot()
	{
		_totalTPText.SetText("");
		_progressText.SetText("");

		SaveLoadGame.DeleteSave(_buttonSlot);
		_selfButton.interactable = false;
		_deleteButton.interactable = false;
	}
}
