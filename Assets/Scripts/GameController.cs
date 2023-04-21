using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
	// Singleton pattern
	public static GameController Instance { get; private set; }

	[Header("MP")]
	[SerializeField] private MPAbsolute player;

	[Header("Others")]
	[SerializeField] private Volume _dashingVolume;
	[SerializeField] private Volume _teleportVolume;

	// Timescaling
	private float _currentTimescale;
	private float _fixedDeltaTime;

	// Pause
	private bool _paused = false;

	// Event
	public event EventHandler OnLevelReset;
	public event EventHandler<bool> OnGamePause;

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

	private void Update()
	{
		// TESTING:
		if (Input.GetKeyDown(KeyCode.T))
		{
			player.ResetAllMovements();

			// Hack to change transform
			player.gameObject.SetActive(false);
			Vector3 currentPosition = player.transform.position;
			currentPosition.y += 10;
			player.transform.position = currentPosition;
			player.gameObject.SetActive(true);
		}
	}

	public void PauseToggle()
	{
		_paused = !_paused;
		if (_paused)
		{
			Time.timeScale = 0f;
			Time.fixedDeltaTime = this._fixedDeltaTime * Time.timeScale;

			OnGamePause?.Invoke(this, true);
		}
		else
		{
			Time.timeScale = _currentTimescale;
			Time.fixedDeltaTime = this._fixedDeltaTime * Time.timeScale;

			OnGamePause?.Invoke(this, false);
		}
	}

	public void ChangeTimeScale(float timeScale)
	{
		_currentTimescale = timeScale;
		Time.timeScale = _currentTimescale;
		Time.fixedDeltaTime = this._fixedDeltaTime * Time.timeScale;
	}

	public void ForceTeleportPlayer(Vector3 newPosition)
	{
		// Hack to change transform
		player.gameObject.SetActive(false);
		player.transform.position = newPosition;
		player.gameObject.SetActive(true);

		player.ResetAllMovements();
	}

	public void ToggleDashingVolume(bool toggle)
	{
		if (toggle)
			_dashingVolume.weight = 1f;
		else
			_dashingVolume.weight = 0f;
	}

	public void ToggleTeleportVolume(bool toggle)
	{
		if (toggle)
			_teleportVolume.weight = 1f;
		else
			_teleportVolume.weight = 0f;

	}
}
