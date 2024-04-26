using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public UIController() { instance = this; }
    public List<UImodule> uiModules;

    private Dictionary<string, Dictionary<string, UIComponent>> modulesDic = new Dictionary<string, Dictionary<string, UIComponent>>();
    private GameStateController gameStateController;

    [HideInInspector] public UIComponent helpButton;

    private void Start()
    {
        Init();
    }

    public void ChangeUIState(GameState nextState)
    {
        switch (nextState)
        {
            case GameState.Menu:
                modulesDic["menu"]["menuModule"].On();
                modulesDic["game"]["gameModule"].Off();
                break;
            case GameState.GamePrepare:
                modulesDic["menu"]["menuModule"].Off();
                modulesDic["game"]["gameModule"].On();
                break;
            case GameState.Game:
                modulesDic["menu"]["menuModule"].Off();
                modulesDic["game"]["gameModule"].On();
                modulesDic["game"]["swipe"].Off();
                break;
            case GameState.Win:
                modulesDic["game"]["gameModule"].Off();
                modulesDic["game"]["winModule"].On();
                break;
            case GameState.Lose:
                modulesDic["game"]["gameModule"].Off();
                modulesDic["game"]["loseModule"].On();
                break;  
        }
    }


    public void Init()
    {
        for (int i = 0; i < uiModules.Count; i++)
        {
            Dictionary<string, UIComponent> componentsDic = new Dictionary<string, UIComponent>();
            for (int j = 0; j < uiModules[i].uiComponents.Count; j++)
            {
                componentsDic.Add(uiModules[i].uiComponents[j].componentName, uiModules[i].uiComponents[j].uiComponent);
            }
            modulesDic.Add(uiModules[i].moduleName, componentsDic);
        }

        gameStateController = GameStateController.instance;



        modulesDic["menu"]["go"].GetButtonComponent().onClick.AddListener(() => { CameraMover.instance.Follow(() => { }); GameStateController.instance.ChangeState(GameState.GamePrepare); });
        modulesDic["game"]["restartLose"].GetButtonComponent().onClick.AddListener(() => SceneManager.LoadScene("SampleScene", LoadSceneMode.Single));
        modulesDic["game"]["next"].GetButtonComponent().onClick.AddListener(() => SceneManager.LoadScene("SampleScene", LoadSceneMode.Single));

    }

    public void UpdateProgress(float _input)
    {
        modulesDic["game"]["slider"].GetSlider().value = _input;
    }

    public Action<Vector2> Down;
    public Action<Vector2> Drag;
    public Action<Vector2> Up;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Down?.Invoke(Input.mousePosition);
        if (Input.GetMouseButton(0))
            Drag?.Invoke(Input.mousePosition);
        if (Input.GetMouseButtonUp(0))
            Up?.Invoke(Input.mousePosition);
    }

}



