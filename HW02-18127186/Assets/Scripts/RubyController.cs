using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    Rigidbody2D rb2d;
    float h;
    float v;
    public float speed = 2f;
    // Start is called before the first frame update
    public float timeInvincible = 2f;
    bool isInvincible;
    float invicibleTimer;
    Animator anim;

    Vector2 direction = Vector2.zero;
    public GameObject projectile;
    public AudioSource audioSource;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            invicibleTimer -= Time.deltaTime;
            if (invicibleTimer < 0)
            {
                isInvincible = false;
            }
        }
        Vector2 moving = new Vector2(h, v);
        if (!Mathf.Approximately(moving.x, 0) || !Mathf.Approximately(moving.y, 0))
        {
            direction = moving.normalized;
        }
        anim.SetFloat("Look X", direction.x);
        anim.SetFloat("Look Y", direction.y);
        anim.SetFloat("Speed", moving.magnitude);
    }
    private void FixedUpdate()
    {

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector2 position = transform.position;
        position.x += (speed * h) * Time.deltaTime;
        position.y += (speed * v) * Time.deltaTime;

        rb2d.MovePosition(position); 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject projectileObj = Instantiate(projectile, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectileScript = projectileObj.GetComponent<Projectile>();
            projectileScript.Launch(direction, 200f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb2d.position + Vector2.up * 0.5f,direction, 1.5f , LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                JambiScript jambi = hit.collider.GetComponent<JambiScript>();
                if (jambi != null)
                {
                    jambi.DisplayDialog();
                }
            }
        }
    }
    public void changeHealth(int amount)
    {
        if (amount < 0)
        {
            anim.SetTrigger("Hit");
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invicibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        HealthRuby.instance.setValue((1.0f * currentHealth) / maxHealth);
        Debug.Log(currentHealth);
    }
    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
