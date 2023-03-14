using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class life : MonoBehaviour
{
    public float life_;
    public string target_tag;
    private float time_ = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time_ += Time.deltaTime;
        if (time_ > life_)
        {
            Destroy(gameObject);
            //            print("died");
        }
        if (gameObject.tag == "Knife")
        {
            gameObject.transform.Rotate(Vector3.up * (360 * Time.deltaTime));
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == target_tag)
        {
            if (target_tag == "Player")
            {
                c.GetComponent<Health_system>().take_damage(5);
            }
            Destroy(gameObject);
        }

    }
}
