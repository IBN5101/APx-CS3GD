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
	public int CurrentTP;
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

	// Events
	public event EventHandler<bool> OnLevelComplete;
	public event EventHandler OnLevelReset;
	public event EventHandler<bool> OnGamePause;
	public event EventHandler OnGameCompleted;

	public event EventHandler OnTPUpdated;
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
		// Retrieve data from LevelData
		int startingTP = LevelData.GetLevelScore(LevelName);
		if (startingTP == LevelData.DEFAULT_TP)
			startingTP = 1;
		UpdateTP(startingTP);

		// Enable (visually) default checkpoint
		_defaultCheckpoint.EnableCheckpoint();
		SetRespawnLocation(_defaultCheckpoint.transform.position);

	}

	private void Update()
	{
		// TESTING:
		if (Input.GetKeyDown(KeyCode.T))
		{
			UpdateTP(CurrentTP - 1);
		}
	}

	public void LevelReset()
	{
		// Increase TP by 1
		UpdateTP(CurrentTP + 1);
		// Respawn by forcefully teleporting to respawn point
		ForceTeleportPlayer(_currentRespawnPosition);

		OnLevelReset?.Invoke(this, EventArgs.Empty);
	}

	public void LevelComplete()
	{
		// Stop time
		PauseTime();
		// Check with database to see if this is a new record or not
		bool newRecord = false;
		int previousBestTP = LevelData.GetLevelScore(LevelName);
		if (CurrentTP < previousBestTP)
		{
			LevelData.SetLevelScore(LevelName, CurrentTP);
			newRecord = true;
		}
		// Update level progress
		// (IBN) Very much a hack
		if (((int) LevelName) >= LevelData.level_progress)
			LevelData.level_progress = ((int) LevelName) + 1;

		OnLevelComplete?.Invoke(this, newRecord);
	}

	// This should only be called once at the final level
	public void GameComplete()
	{
		// Stop time
		PauseTime();
		// Check with database to see if this is a new record or not
		int previousBestTP = LevelData.GetLevelScore(LevelName);
		if (CurrentTP < previousBestTP)
		{
			LevelData.SetLevelScore(LevelName, CurrentTP);
		}

		OnGameCompleted?.Invoke(this, EventArgs.Empty);
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
		CurrentTP = value;

		OnTPUpdated?.Invoke(this, EventArgs.Empty);
	}

	#region Pause & TimeScale
	public void PauseToggle()
	{
		// (IBN) Yep, dumb dumb code.
		_paused = !_paused;
		if (_paused)
		{
			PauseTime();

			OnGamePause?.Invoke(this, true);
		}
		else
		{
			ResumeTime();

			OnGamePause?.Invoke(this, false);
		}
	}

	private void PauseTime()
	{
		Time.timeScale = 0f;
		Time.fixedDeltaTime = this._fixedDeltaTime * Time.timeScale;
	}

	private void ResumeTime()
	{
		Time.timeScale = _currentTimescale;
		Time.fixedDeltaTime = this._fixedDeltaTime * Time.timeScale;
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
