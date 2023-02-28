using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GameController : MonoBehaviour
{
    public void OnExitGame(InputValue value)
    {
        Application.Quit();
    }
}
