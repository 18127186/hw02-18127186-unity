using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JambiScript : MonoBehaviour
{
    public GameObject dialog;
    public float displayTime = 4.0f;
    float timeDisplay;
    // Start is called before the first frame update
    void Start()
    {
        dialog.SetActive(false);
        timeDisplay = -1.0f; ;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDisplay > 0)
        {
            timeDisplay -= Time.deltaTime;
            if (timeDisplay <= 0)
            {
                dialog.SetActive(false);
            }
        }
    }
    public void DisplayDialog ()
    {
        timeDisplay = displayTime;
        dialog.SetActive(true);
    }
}
