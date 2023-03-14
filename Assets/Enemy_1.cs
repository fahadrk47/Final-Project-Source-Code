using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public GameObject object_;
    public Transform[] mouth;
    public float speed_;
    [Range(0.1f, 0.9f)]
    public float shoot_rate_;
    private int number_of_projectiles = 0;
    private float temp_;
    public int move_speed;
    public int health_;
    public GameObject player_;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        player_ = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - player_.transform.position;
        temp_ = shoot_rate_;


    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + player_.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 0.6f);

    }
    public void Attack(int i)
    {
        var object_temp = Instantiate(object_, mouth[i].position, mouth[i].rotation);
        object_temp.GetComponent<Rigidbody>().velocity = mouth[i].forward * speed_;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Knife")
        {
            health_--;
            if (health_ < 1)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Die");
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
        if (c.transform.tag == "Knife_blood")
        {
            health_ = health_ - 3;
            if (health_ < 1)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Die");
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
    public void die_()
    {
        Enemy_manager.Enemies_destroy++;
        Destroy(gameObject);

    }



}
