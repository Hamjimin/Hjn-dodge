using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float speed = 8f;
    public float rotSpeed = 120.0f;

    private Transform tr;

    public int hp = 100;
    public HPBar hpbar;

    private float spawmRate = 0.2f;
    private float timerAfterSpawn;
    public GameObject playerbulletPrefab;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        timerAfterSpawn = 0f;
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        playerRigidbody.velocity = newVelocity;

        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            Vector3 projectedPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 currentPos = transform.position;
            Vector3 rotation = projectedPos - currentPos;
            tr.forward = rotation;
        }

        timerAfterSpawn += Time.deltaTime;

        if(Input.GetButton("Fire1") && timerAfterSpawn >= spawmRate)
        {
            timerAfterSpawn = 0;
            GameObject bullet = Instantiate(playerbulletPrefab, transform.position, transform.rotation);
        }
    }

    public void GetHeal(int heal)
    {
        hp += heal;
        if(hp > 100)
        {
            hp = 100;
        }
        hpbar.SetHp(hp);
    }
    public void GetDamage(int damage)
    {
        hp -= damage;
        hpbar.SetHp(hp);
        if(hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}
