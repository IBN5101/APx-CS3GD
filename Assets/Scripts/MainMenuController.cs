using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class MainMenuController : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene(GameAssets.Instance.scene_Levels);
	}

	public void OnExitGame(InputValue value)
	{
		Application.Quit();
	}

	public void ExitGameButton()
	{
		Application.Quit();
	}
}
