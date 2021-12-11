using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    public AudioClip hitEnemy;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Launch (Vector2 direct, float force)
    {
        rb.AddForce(direct * force );
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemyController = collision.collider.GetComponent<EnemyController>();
        Destroy(gameObject);
        if (enemyController)
        {
            enemyController.PlaySound(hitEnemy);
            enemyController.ChangeBroken();
        }
    }
}
