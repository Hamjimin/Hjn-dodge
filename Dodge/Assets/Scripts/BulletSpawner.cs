using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 0.5f;

    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    public int hp = 100;
    public HPBar hpbar;
    public GameObject level;

    public bool isMoving = false;
    private NavMeshAgent nvAgent;
  
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(MonsterAI());
        nvAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if(timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.LookAt(target);

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        hpbar.SetHp(hp);
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator MonsterAI()
    {
        while(hp > 0)
        {
            yield return new WaitForSeconds(0.2f);

            if(isMoving)
            {
                nvAgent.destination = target.position;
                nvAgent.isStopped = false;
            }
            else
            {
                nvAgent.isStopped = true;
            }
        }
    }
}
