using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class LevelController : MonoBehaviour
{
	// Singleton pattern
	public static LevelController Instance { get; private set; }

	[Header("Settings")]
	[Tooltip("Current level name")]
	public LevelData.LevelName LevelName = LevelData.LevelName.TESTING;
	[Tooltip("Current time point (TP)")]
	[SerializeField]
	[ReadOnlyInspector]
	private int _currentTP;
	[Space(10)]

	[Tooltip("Default checkpoint")]
	[SerializeField] 
	private Checkpoint _defaultCheckpoint;
	[Tooltip("Current respawn position")]
	[SerializeField]
	[ReadOnlyInspector]
	private Vector3 _currentRespawnPosition;
	

	[Header("MP")]
	public MPAbsolute player;

	[Header("Others")]
	[SerializeField] private Volume _dashingVolume;
	[SerializeField] private Volume _teleportVolume;

	// Timescaling
	private float _currentTimescale = 1.0f;
	private float _fixedDeltaTime;

	// Pause
	private bool _paused = false;

	// Event
	public event EventHandler<int> OnTPUpdated;
	public event EventHandler OnLevelReset;
	public event EventHandler<bool> OnGamePause;
	public event EventHandler OnCheckpointDisable;

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

	private void Start()
	{
		UpdateTP(LevelData.GetLevelScore(LevelName));

		// Enable (visually) default checkpoint
		_defaultCheckpoint.EnableCheckpoint();
		SetRespawnLocation(_defaultCheckpoint.transform.position);

	}

	private void Update()
	{
		// TESTING:
		if (Input.GetKeyDown(KeyCode.T))
		{
			LevelReset();
		}
	}

	public void LevelReset()
	{
		// Increase TP by 1
		UpdateTP(_currentTP + 1);
		ForceTeleportPlayer(_currentRespawnPosition);
	}

	public void ForceTeleportPlayer(Vector3 newPosition)
	{
		// Hack to change transform
		player.gameObject.SetActive(false);
		player.transform.position = newPosition;
		player.gameObject.SetActive(true);

		player.ResetAllMovements();
	}

	public void DisableAllCheckpoints()
	{
		OnCheckpointDisable?.Invoke(this, EventArgs.Empty);
	}

	public void SetRespawnLocation(Vector3 newPosition)
	{
		_currentRespawnPosition = newPosition;
	}

	private void UpdateTP(int value)
	{
		_currentTP = value;
		OnTPUpdated?.Invoke(this, _currentTP);
	}

	#region Pause & TimeScale
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
	#endregion

	#region Post-processing Volume toggle
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
	#endregion
}
