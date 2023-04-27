using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
	[Header("UI panels")]
	[SerializeField] private GameObject _playPanel;
	[SerializeField] private GameObject _pausePanel;
	[SerializeField] private GameObject _levelCompletedPanel;

	[Header("Play panel")]
	[SerializeField] private TextMeshProUGUI _TPText;
	[SerializeField] private Image _dashingIndicator;

	[Header("Level completed panel")]
	[SerializeField] private TextMeshProUGUI _TPResultText;
	[SerializeField] private GameObject _newRecordText;
	[SerializeField] private GameObject _levelCompletedText;
	[SerializeField] private GameObject _gameCompletedText;

	private void Start()
	{
		LevelController.Instance.OnLevelComplete += LevelController_OnLevelComplete;
		LevelController.Instance.OnGamePause += LevelController_OnGamePause;
		LevelController.Instance.OnGameCompleted += LevelController_OnGameCompleted;

		LevelController.Instance.OnTPUpdated += LevelController_OnTPUpdated;

		// (IBN) ... spaghetti!
		LevelController.Instance.player.SpecialMovement.OnDashingIdle += SpecialMovement_OnDashingIdle;
	}

	public void ResumeButton()
	{
		LevelController.Instance.PauseToggle();
	}

	public void MapButton()
	{
		LevelController.Instance.ChangeTimeScale(1f);
		SceneManager.LoadScene(GameAssets.Instance.scene_Levels);
	}

	public void MainMenuButton()
	{
		LevelController.Instance.ChangeTimeScale(1f);
		SceneManager.LoadScene(GameAssets.Instance.scene_MainMenu);
	}

	private void LevelController_OnLevelComplete(object sender, bool newRecord)
	{
		// Disable play panel (the TP counter and dashing indicator)
		_playPanel.SetActive(false);
		// Enable level complete panel
		_levelCompletedPanel.SetActive(true);
		_levelCompletedText.SetActive(true);
		_TPResultText.SetText(LevelData.TPFormatting(LevelController.Instance.CurrentTP));
		// Enable the new record if new record TP is set
		_newRecordText.SetActive(newRecord);
	}

	private void LevelController_OnGamePause(object sender, bool paused)
	{
		_pausePanel.SetActive(paused);
	}

	private void LevelController_OnGameCompleted(object sender, EventArgs e)
	{
		// Disable play panel (the TP counter and dashing indicator)
		_playPanel.SetActive(false);
		// Enable level complete panel
		_levelCompletedPanel.SetActive(true);
		_gameCompletedText.SetActive(true);
		_TPResultText.SetText("Total time: \n" + LevelData.TPFormatting(LevelData.GetTotalScore()));
	}

	private void LevelController_OnTPUpdated(object sender, EventArgs e)
	{
		_TPText.SetText(LevelData.TPFormatting(LevelController.Instance.CurrentTP));
	}

	private void SpecialMovement_OnDashingIdle(object sender, bool dashIdle)
	{
		// (IBN) Hardcoding
		Color dashingIdleTrue = new Color(0, 0.77f, 1);
		Color dashingIdleFalse = new Color(1, 0.22f, 0);
		_dashingIndicator.color = dashIdle ? dashingIdleTrue : dashingIdleFalse;
	}
}
