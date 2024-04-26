using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController instance;
    public GameStateController() { instance = this; }
    public GameState curGameState;

    private void Start()
    {
        UIController.instance.Down += StartGame;
    }

    public void ChangeState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                break;
            case GameState.GamePrepare:
                break;
            case GameState.Game:
                break;
            default:
                break;
        }
        curGameState = gameState;
        UIController.instance.ChangeUIState(gameState);
    }

    private void StartGame(Vector2 _vector)
    {
        if(curGameState == GameState.GamePrepare)
            ChangeState(GameState.Game);
    }
}

