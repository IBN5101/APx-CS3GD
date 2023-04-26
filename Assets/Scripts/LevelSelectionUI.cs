using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour
{
	[SerializeField] private Button _buttonG01;
	[SerializeField] private Button _buttonF2_01;
	[SerializeField] private Button _buttonF2_02;
	[SerializeField] private Button _buttonF2_03;
	[SerializeField] private Button _buttonF2_04;
	[SerializeField] private TextMeshProUGUI _totalTP;

	private void Start()
	{
		// (IBN) Programming insanity
		int level_progress = LevelData.level_progress;

		if (level_progress >= 1)
			_buttonG01.interactable = true;
		if (level_progress >= 2)
			_buttonF2_01.interactable = true;
		if (level_progress >= 3)
			_buttonF2_02.interactable = true;
		if (level_progress >= 4)
			_buttonF2_03.interactable = true;
		if (level_progress >= 5)
			_buttonF2_04.interactable = true;

		_totalTP.SetText(LevelData.TPFormatting(LevelData.GetTotalScore()));
	}

	public void GoToLevel(LevelData.LevelName level)
	{
		switch (level)
		{
			case LevelData.LevelName.G01:
				SceneManager.LoadScene(GameAssets.Instance.scene_G01);
				break;
			case LevelData.LevelName.F2_01:
				SceneManager.LoadScene(GameAssets.Instance.scene_F2_01);
				break;
			case LevelData.LevelName.F2_02:
				SceneManager.LoadScene(GameAssets.Instance.scene_F2_02);
				break;
			case LevelData.LevelName.F2_03: 
				break;
			case LevelData.LevelName.F2_04:
				break;
		}
	}

}
