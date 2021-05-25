using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float speed = 8f;

    public int hp = 100;
    public HPBar hpbar;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();    
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);
        playerRigidbody.velocity = newVelocity;
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
