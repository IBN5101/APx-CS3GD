using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
	// Singleton pattern
	public static GameController Instance { get; private set; }

	[Header("MP")]
	[SerializeField] private MPAbsolute player;

	[Header("UI")]
	[SerializeField] private GameObject pauseMenu;

	// Timescaling
	private float _currentTimescale;
	private float _fixedDeltaTime;

	// Pause
	private bool _paused = false;

	private void Awake()
	{
		// Singleton check
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;

		_fixedDeltaTime = Time.fixedDeltaTime;
	}

	public void PauseToggle()
	{
		_paused = !_paused;
		if (_paused)
		{
			Time.timeScale = 0f;
			player.mpControlsInput.SetCursorAll(false);
			pauseMenu.SetActive(true);
		}
		else
		{
			Time.timeScale = _currentTimescale;
			player.mpControlsInput.SetCursorAll(true);
			pauseMenu.SetActive(false);
		}
	}

	public void ChangeTimeScale(float timeScale)
	{
		_currentTimescale = timeScale;
		Time.timeScale = _currentTimescale;
		Time.fixedDeltaTime = this._fixedDeltaTime * Time.timeScale;
	}
}
