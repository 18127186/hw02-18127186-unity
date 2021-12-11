using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public bool vertical, broken = true;
    private int direction = 1;
    public float changeDirectionTime = 1.5f;
    float timer;
    Rigidbody2D rb;
    public GameObject smokeEffect;
    Animator anim;
    public AudioClip enemyFixed;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timer = changeDirectionTime;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            return;
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction *= -1;
            timer = changeDirectionTime;
        }
        if (vertical)
        {
            anim.SetFloat("Move X", 0);
            anim.SetFloat("Move Y", direction);
        }
        else
        {
            anim.SetFloat("Move X", direction);
            anim.SetFloat("Move Y", 0);
        }
    }
    private void FixedUpdate()
    {

        if (!broken)
        {
            return;
        }
        Vector2 position = transform.position;
        if (vertical)
        {
            position.y += (speed * direction * Time.deltaTime);
        } else
        {
            position.x += (speed * direction * Time.deltaTime);
        }
        rb.MovePosition(position);
    }
    public void ChangeBroken()
    {
        anim.SetBool("Fixed", true);
        broken = false;
        rb.simulated = false;
        Destroy(smokeEffect);
        PlaySound(enemyFixed);
    }
    public void PlaySound (AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
