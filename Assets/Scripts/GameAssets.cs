using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
	// Singleton pattern ++
	private static GameAssets instance;
	public static GameAssets Instance
	{
		get
		{
			if (instance == null)
				instance = Resources.Load<GameAssets>("GameAssets");
			return instance;
		}
	}

	[Header("Scene name")]
	public string scene_MainMenu = "Main Menu";
	public string scene_Levels = "Level selection";
	public string scene_G01 = "Level G01";
	public string scene_F2_01 = "Level F2_01";
	public string scene_F2_02 = "Level F2_02";
	public string scene_F2_03 = "Level F2_03";
	public string scene_F2_04 = "Level F2_04";

	[Header("JumpPad")]
	public Material m_JumpPadDisabled;
	public Material m_JumpPadEnabled;

	[Header("Checkpoint")]
	public Material m_checkpointDisabled;
	public Material m_checkpointEnabled;
}
