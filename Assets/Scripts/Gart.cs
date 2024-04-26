using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Gart
{
    public static Vector3 FromToRotation(Vector3 start, Vector3 end, float value)
    {
        start.x = start.x > 180 ? -360 + start.x : start.x;
        start.y = start.y > 180 ? -360 + start.y : start.y;
        start.z = start.z > 180 ? -360 + start.z : start.z;

        end.x = end.x > 180 ? -360 + end.x : end.x;
        end.y = end.y > 180 ? -360 + end.y : end.y;
        end.z = end.z > 180 ? -360 + end.z : end.z;
        float x = Mathf.MoveTowards(start.x, end.x, Mathf.Abs(start.x - end.x) * value);
        float y = Mathf.MoveTowards(start.y, end.y, Mathf.Abs(start.y - end.y) * value);
        float z = Mathf.MoveTowards(start.z, end.z, Mathf.Abs(start.z - end.z) * value);

        return new Vector3(x, y, z);
    }
    public static Vector3 FromTo(Vector3 start, Vector3 end, float value)
    {
        return Vector3.MoveTowards(start, end, Vector3.Distance(start, end) * value);
    }
}
interface IMovable
{
    void Move();
}
interface IHitable
{
    void Hit();
}
public enum EnemyState
{
    Idle,
    Run,
    Attack,
    Hitted
}
public enum GameState
{
    Menu,
    Game,
    GamePrepare,
    Win,
    Lose
}
[Serializable]
public struct UIcomp
{
    public string componentName;
    public UIComponent uiComponent;
}

[Serializable]
public struct UImodule
{
    public string moduleName;
    public List<UIcomp> uiComponents;
}