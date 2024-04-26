using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private GameObject grountPrefab;
    [SerializeField] private CarController car;
    [SerializeField] private GameObject finish;
    [SerializeField] private ParticleSystem deathEffectEnemy;

    private Queue<Enemy> enemies = new Queue<Enemy>();//Потім можна буде використовувати, якщо рівень буде більше
    private Queue<GameObject> grounds = new Queue<GameObject>();
    private int groundCount;



    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject curGround = Instantiate(grountPrefab, transform);
            grounds.Enqueue(curGround);
            curGround.transform.localPosition = new Vector3(0, 0, 105 * groundCount);
            groundCount++;
        }

        int countEnemies = Random.Range(20, 40);
        for (int i = 0; i < countEnemies; i++)
        {
            Enemy curEnemy = Instantiate(enemyPrefab);
            curEnemy.transform.position = car.transform.position + new Vector3(Random.Range(-50,50) / 5f, 0, 50 + Random.Range(0, Vector3.Distance(finish.transform.position, car.transform.position) - 50 + 10));
            curEnemy.Set(car, deathEffectEnemy);
            curEnemy.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            enemies.Enqueue(curEnemy);
        }
    }
    private void Update()
    {
        if ((grounds.Peek().transform.position.z - car.transform.position.z) < -100)
        {
            UpdateGround();
        }
    }
    public void UpdateGround()
    {
        GameObject curGround = grounds.Dequeue();
        curGround.transform.localPosition = new Vector3(0, 0, 105 * groundCount);
        groundCount++;
        grounds.Enqueue(curGround);
    }

}
