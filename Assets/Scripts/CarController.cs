using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour, IMovable, IHitable
{
    public GunController gun;
    [SerializeField] private float health = 100;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private ParticleSystem deathEffect;
    private float minX = -1.5f;
    private float maxX = 1.5f;
    private float angle = 0;
    private int direction = 1;
    public void Hit()
    {
        health -= 40;
        StartCoroutine(HitEffect());
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = health * 0.01f;
        if (health <= 0)
            Kill();
    }

    private void Kill()
    {
        deathEffect.transform.position = transform.position;
        deathEffect.Play();
        gun.OnOffLaser(false);
        GameStateController.instance.ChangeState(GameState.Lose);
    }
    public void Move()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * 10;
        transform.rotation = Quaternion.Euler(0, angle, 0);
        angle = Mathf.Clamp(angle + (Random.Range(0, 10) / 500f) * direction, -5, 5);
        if (transform.localPosition.x >= maxX)
            direction = -1;
        if (transform.localPosition.x <= minX)
            direction = 1;
    }
    private IEnumerator HitEffect()
    {
        float t = 0.2f;
        float targetT = t / 2f;
        float startScale = transform.localScale.x;
        while (t > 0)
        {
            t -= Time.deltaTime;
            transform.localScale = Vector3.one * (startScale + Mathf.PingPong(Time.time, targetT));
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.one * startScale;
    }
}

