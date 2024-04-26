using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TrailRenderer trail;
    Action<Bullet> returnBullet;
    void LateUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * 60;
    }
    public void Enable(Action<Bullet> _returnBullet)
    {
        trail.Clear();
        gameObject.SetActive(true);
        returnBullet = _returnBullet;
        StartCoroutine(DelayReturn(_returnBullet));
    }

    IEnumerator DelayReturn(Action<Bullet> _returnBullet)
    {
        yield return new WaitForSeconds(4);
        gameObject.SetActive(false);
        _returnBullet.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Hit();
            StopCoroutine(DelayReturn(returnBullet));
            returnBullet.Invoke(this);
            gameObject.SetActive(false);

        }
    }
}
