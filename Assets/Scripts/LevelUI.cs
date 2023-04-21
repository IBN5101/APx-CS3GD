using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
	[SerializeField] private GameObject _pauseMenu;

	private void Start()
	{
		GameController.Instance.OnGamePause += GameController_OnGamePause;
	}

	public void ResumeButton()
	{
		GameController.Instance.PauseToggle();
	}

	public void MapButton()
	{

	}

	public void MainMenuButton()
	{

	}
	private void GameController_OnGamePause(object sender, bool paused)
	{
		_pauseMenu.SetActive(paused);
	}
}
