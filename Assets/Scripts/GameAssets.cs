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
	public string scene_G01 = "Level G01";

	[Header("JumpPad")]
	public Material m_JumpPadDisabled;
	public Material m_JumpPadEnabled;

	[Header("Checkpoint")]
	public Material m_checkpointDisabled;
	public Material m_checkpointEnabled;
}
