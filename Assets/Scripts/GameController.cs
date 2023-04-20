using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
	// Singleton pattern +
	public static GameController Instance { get; private set; }

	// Timescaling
	private float _currentTimescale;
	private float _fixedDeltaTime;

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

	public void ChangeTimeScale(float timeScale)
	{
		_currentTimescale = timeScale;
		Time.timeScale = _currentTimescale;
		Time.fixedDeltaTime = this._fixedDeltaTime * Time.timeScale;
	}
}
