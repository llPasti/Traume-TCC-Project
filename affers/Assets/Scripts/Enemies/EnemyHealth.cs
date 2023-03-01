using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    Enemy enemy;
    public bool isDamaged;
    public GameObject deathEffect;
    public SpriteRenderer sprite;
    Blinks material;
    public Rigidbody2D rb;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        material = GetComponent<Blinks>();
        enemy = GetComponent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon") && !isDamaged)
            {

            enemy.healthPoints -= 2f;
            AudioManager.instance.PlayAudio(AudioManager.instance.hit);
            if (collision.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector2(enemy.knockBackForceX, enemy.knockBackForceY), ForceMode2D.Force);
            }
            else
            {
                rb.AddForce(new Vector2(-enemy.knockBackForceX, enemy.knockBackForceY), ForceMode2D.Force);
            }

            

            StartCoroutine(Damager());
            if(enemy.healthPoints <= 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                AudioManager.instance.PlayAudio(AudioManager.instance.death);
                Destroy(gameObject);
            }
        }
    }
    IEnumerator Damager()
    {
        isDamaged = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(0.5f);
        isDamaged = false;
        sprite.material = material.original;
    }
}
