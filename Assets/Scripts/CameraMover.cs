using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Gart;

public class CameraMover : MonoBehaviour
{
    public static CameraMover instance;
    public CameraMover() { instance = this; }
    [SerializeField] private Transform car;
    private bool isFollow = false;
    public void Follow(Action action)
    {
        StartCoroutine(ToStartPos(action));
    }

    private IEnumerator ToStartPos(Action action)
    {
        float t = 0;
        float targetT = 1f;
        Vector3 startPos = transform.position;
        Vector3 pos = car.transform.position + new Vector3(0, 20, -16);
        Vector3 startAngl = transform.localEulerAngles;
        while (t <= targetT)
        {
            t += Time.deltaTime;
            transform.localRotation = Quaternion.Euler(FromToRotation(startAngl, new Vector3(38, 0, 0), t / targetT));
            transform.position = FromTo(startPos, pos, t / targetT);
            yield return new WaitForEndOfFrame();
        }
        isFollow = true;
        action.Invoke();
    }

    void LateUpdate()
    {
        if (!isFollow)
            return;
        transform.position = Vector3.MoveTowards(transform.position, car.transform.position + new Vector3(0, 20, -16), Time.deltaTime * 100);
    }

}
