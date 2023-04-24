using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour
{
	[SerializeField] private LevelSelectionUI levelSelection;
	[SerializeField] private LevelData.LevelName buttonLevel;

	public void GoToLevel()
	{
		levelSelection.GoToLevel(buttonLevel);
	}
}
