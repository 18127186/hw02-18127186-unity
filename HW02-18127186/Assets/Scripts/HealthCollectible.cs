using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController ruby = collision.GetComponent<RubyController>();
        if (ruby != null)
        {
            if (ruby.currentHealth < ruby.maxHealth)
            {

                ruby.changeHealth(1);
                ruby.PlaySound(clip);
                Destroy(gameObject);
            }
        }

    }
}
