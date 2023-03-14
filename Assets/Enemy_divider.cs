using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_divider : MonoBehaviour
{
    // Start is called before the first frame update

    public static float maxSpeed_new;
    public static float forwardSpeed_new;
    public static int Attack_speed_new;
    public AudioSource camera_;

    void Start()
    {
        camera_ = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            Enemy_manager.add_wave = true;
            forwardSpeed_new = 4;
            maxSpeed_new = 4;
            Attack_speed_new = 10;
        }
    }
}
