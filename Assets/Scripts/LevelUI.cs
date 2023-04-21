using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
	[Header("UI elements")]
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private TextMeshProUGUI _TPText;
	[SerializeField] private Image _dashingIndicator;

	private void Start()
	{
		LevelController.Instance.OnTPUpdated += LevelController_OnTPUpdated;
		LevelController.Instance.OnGamePause += LevelController_OnGamePause;
		// (IBN) ... spaghetti!
		LevelController.Instance.player.SpecialMovement.OnDashingIdle += SpecialMovement_OnDashingIdle;
	}

	public void ResumeButton()
	{
		LevelController.Instance.PauseToggle();
	}

	public void MapButton()
	{

	}

	public void MainMenuButton()
	{

	}

	private void LevelController_OnTPUpdated(object sender, int tp)
	{
		// Hours, Minutes, Seconds
		int tp_h = 0;
		int tp_m = 0;
		int tp_s = 0;

		// I am lazy, let's just call this secret feature.
		if (tp >= 3600)
		{
			tp_h = 99; tp_m = 99; tp_s = 99;
		}
		// Minutes and Seconds
		else if (tp >= 60)
		{
			tp_m = Mathf.FloorToInt(tp / 60.0f);
			tp_s = tp - tp_m * 60;
		}
		// Seconds only
		else
		{
			tp_s = tp;
		}

		string result = tp_h.ToString("D2") + ":" + tp_m.ToString("D2") + ":" + tp_s.ToString("D2");
		_TPText.SetText(result);
	}

	private void LevelController_OnGamePause(object sender, bool paused)
	{
		_pauseMenu.SetActive(paused);
	}

	private void SpecialMovement_OnDashingIdle(object sender, bool dashIdle)
	{
		// (IBN) Hardcoding
		Color dashingIdleTrue = new Color(0, 0.77f, 1);
		Color dashingIdleFalse = new Color(1, 0.22f, 0);
		_dashingIndicator.color = dashIdle ? dashingIdleTrue : dashingIdleFalse;
	}
}
