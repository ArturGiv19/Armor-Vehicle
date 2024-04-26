using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private CarController car;
    [SerializeField] private GameObject finish;
    private float _distance;
    private float startDist;
    private Vector3 startPos;
    private void Start()
    {
        startDist = Vector3.Distance(car.transform.position, finish.transform.position);
        startPos = car.transform.position;
    }

    void Update()
    {
        if(GameStateController.instance.curGameState == GameState.Game)
        {
            car.Move();
            _distance = distanceValue(car.transform, startPos);
            UIController.instance.UpdateProgress(_distance);
            if (_distance >= 1)
            {
                GameStateController.instance.ChangeState(GameState.Win);
            }
        }
            
    }

    private float distanceValue(Transform pos1, Vector3 pos2)
    {
        return  Vector3.Distance(pos1.position, pos2) / startDist;
    }
}
