using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IMovable, IHitable
{
    [SerializeField] private CarController car;
    [SerializeField] private float speed = 10; 
    [SerializeField] private float health = 100;
    [SerializeField] private Animator animator;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private ParticleSystem deathEffect;

    private EnemyState state;
    private float distanceToEnemy = 1000;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            HitEnemy(0);
            Debug.Log("TRIGGER");
        }
    }

    public void Set(CarController _car, ParticleSystem _deathEffect)
    {
        car = _car;
        deathEffect = _deathEffect;
        this.enabled = true;
    }
    public void Hit()
    {
        ChangeState(EnemyState.Hitted);
        health -= 50;
        healthSlider.gameObject.SetActive(true);
        healthSlider.value = health * 0.01f;
        StartCoroutine(HitEffect());

        if (health <= 0)
            Kill();
    }

    public void ChangeState(EnemyState _newState)
    {
        if (state == _newState)
            return;
        switch (_newState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Run:
                animator.Play("running");
                break;
            case EnemyState.Attack:
                animator.Play("punch");
                HitEnemy(0.5f);
                break;
            case EnemyState.Hitted:
                animator.Play("hitted");
                StartCoroutine(Delay(1, () => {
                    ChangeState(EnemyState.Run);
                }));
                break;
                
        }
        state = _newState;
    }

    private void Kill()
    {
        gameObject.SetActive(false);
        deathEffect.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        deathEffect.Play();
    }
    private void HitEnemy(float timeDelay)
    {
        StartCoroutine(Delay(timeDelay, () => {
            car.Hit();
            Kill();
        }));
    }
    public void Move()
    {
        Vector3 direction = (car.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed * 2f);
    }
    void Update()
    {
        if (GameStateController.instance.curGameState != GameState.Game)
            return;
        distanceToEnemy = Vector3.Distance(transform.position, car.transform.position);
        if (distanceToEnemy < 30 && state == EnemyState.Idle)
            ChangeState(EnemyState.Run);
        if (distanceToEnemy < 3)
            ChangeState(EnemyState.Attack);

        if (state == EnemyState.Run)
            Move();
    }

    public IEnumerator Delay(float _time, Action action)
    {
        yield return new WaitForSeconds(_time);
        action.Invoke();
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

