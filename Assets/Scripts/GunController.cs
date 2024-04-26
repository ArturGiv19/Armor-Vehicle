using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private float delayShoot = 0.5f;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private Transform bullets;
    [SerializeField] private GameObject laser;
    private Queue<Bullet> bulletQueue = new Queue<Bullet>();
    private float time = 0;

    private void Start()
    {
        for (int i = 0; i < 1f / delayShoot * 5; i++)
        {
            Bullet curBullet = Instantiate(bullet);
            AddBullet(curBullet);
        }
        UIController.instance.Drag += Drag;
        UIController.instance.Up += Up;
    }

    private void Down(Vector2 _vector){}

    private void Drag(Vector2 _vector)
    {
        if (GameStateController.instance.curGameState != GameState.Game)
            return;
        if (!laser.activeSelf)
            OnOffLaser(true);
        Vector3 direction = (transform.position - transform.position + new Vector3(-(Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f) * 20, 0, -10)) / 2f;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(-90, angle - 180, 0);

        time += Time.deltaTime;
        if (time >= delayShoot && GameStateController.instance.curGameState == GameState.Game)
        {
            time = 0;
            Shoot();
        }
    }

    private void Up(Vector2 _vector)
    {
        if (GameStateController.instance.curGameState != GameState.Game)
            return;
        OnOffLaser(false);
    }

    public void OnOffLaser(bool isTrue)
    {
        laser.SetActive(isTrue);
    }
    private void AddBullet(Bullet curBullet)
    {
        curBullet.transform.SetParent(bulletPos);
        curBullet.transform.localRotation = Quaternion.Euler(90, curBullet.transform.localEulerAngles.y, curBullet.transform.localEulerAngles.z);
        curBullet.transform.localPosition = bulletPos.localPosition;
        bulletQueue.Enqueue(curBullet);
    }
    private void Shoot()
    {
        Bullet curBullet = bulletQueue?.Dequeue();
        curBullet.transform.SetParent(bullets);
        curBullet.Enable((curBullet) => { AddBullet(curBullet); });
    }
}
