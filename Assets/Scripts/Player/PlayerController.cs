using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1;//0:leftlane 1:middlelane 2:rightlane
    public float laneDistance = 4;//the distance between lanes

    public float jumpDistance;
    public float Gravity = -20;

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public Animator animator;
    private bool isSliding = false;
    private Health_system hb;

    // Start is called before the first frame update
    public GameObject weapon;
    public GameObject weapon_power;
    public GameObject weapon_power1;
    public GameObject Shield;
    public GameObject Shield1;
    public GameObject MovementIcon;
    public GameObject AttackSpeedIcon;
    public GameObject AttackDmgIcon;
    public Transform[] shooters;
    public AudioClip jump_audio;
    public AudioClip hit_audio;
    public AudioClip special_attack;
    public AudioSource cam_;
    public int Attack_speed;
    public GameObject shieldImage;
    public GameObject shieldImage2;


    //public static update_values enemy_mode;
    void Start()
    {
        cam_ = this.GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        hb = GetComponent<Health_system>();
        Enemy_divider.Attack_speed_new = Attack_speed;
        Enemy_divider.maxSpeed_new = maxSpeed;
        Enemy_divider.forwardSpeed_new = forwardSpeed;
        if (PlayerPrefs.GetInt("power1") == 1)
        {
            weapon = weapon_power;
            AttackDmgIcon.SetActive(true);
        }
        if (PlayerPrefs.GetInt("power5") == 1)
        {
            Attack_speed += 5;
            AttackSpeedIcon.SetActive(true);
        }
        if (PlayerPrefs.GetInt("power2") == 1)
        {
            forwardSpeed += 3;
            maxSpeed += 5;
            MovementIcon.SetActive(true);
        }
        if (PlayerPrefs.GetInt("power3") == 1)
        {
            Shield.SetActive(true);
        }
        if (PlayerPrefs.GetInt("power4") == 1)
        {
            Shield1.SetActive(true);
        }
    }

    void Update()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }
        //increase speed overtime
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }


        animator.SetBool("isGameStarted", true);
        direction.z = forwardSpeed;

        if(PlayerPrefs.GetInt("power3") == 1)
        {
            shieldImage.SetActive(true);
        }
        if (PlayerPrefs.GetInt("power4") == 1)
        {
            shieldImage2.SetActive(true);
        }
        else
        {
            shieldImage.SetActive(false);
            shieldImage2.SetActive(false);
        }



        // Inputs for the Lane


        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        if (controller.isGrounded)
        {
            direction.y = -1;
            if (SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;
        }
        if (SwipeManager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }

        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        // calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;

        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        controller.Move(direction * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10 * Time.fixedDeltaTime);
        controller.center = controller.center;

    }
    private void Jump()
    {
        direction.y = jumpDistance;
        cam_.clip = jump_audio;
        cam_.Play();

    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            //PlayerManager.gameOver = true;
            if (PlayerPrefs.GetInt("power3") == 1)
            {
                Shield.SetActive(false);
                PlayerPrefs.SetInt("power3", 0);
            }
            else
            {
                hb.take_damage(1);
                cam_.clip = hit_audio;
                cam_.Play();
                Destroy(hit.gameObject);
            }

        }
        else if (hit.transform.tag == "Projectile")
        {
            hb.take_damage(1);
            print("abc");
        }
        else if (hit.transform.tag == "power_up")
        {
            PlayerManager.level_clear = true;
        }
        else
        {
            //Attack_speed = Enemy_divider.Attack_speed_new;
            //maxSpeed = Enemy_divider.maxSpeed_new;
            //forwardSpeed = Enemy_divider.forwardSpeed_new;
        }

    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, 0.3f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(0.5f);
        controller.center = new Vector3(0, 1.1f, 0);
        controller.height = 2;
        animator.SetBool("isSliding", false);
        isSliding = false;
    }
    public void SP_attack(int x)
    {
        if (Enemy_manager.add_wave == true)
        {
            if (x == 5)
            {
                PlayerManager.sp_used = true;
                StartCoroutine(sp_generate());
                var object_temp = Instantiate(weapon_power, shooters[0].position, shooters[0].rotation);
                object_temp.GetComponent<Rigidbody>().velocity = shooters[0].forward * Attack_speed;
                cam_.clip = special_attack;
                cam_.Play();
            }
            else if (x == 0 || x == 1 || x == 2)
            {
                var object_temp = Instantiate(weapon_power, shooters[x].position, shooters[x].rotation);
                object_temp.GetComponent<Rigidbody>().velocity = shooters[x].forward * Attack_speed;
            }

            else
            {
                var object_temp = Instantiate(weapon, shooters[x].position, shooters[x].rotation);
                object_temp.GetComponent<Rigidbody>().velocity = shooters[x].forward * Attack_speed;
            }



        }
        else
        {
            return;
        }

    }



    IEnumerator sp_generate()
    {

        yield return new WaitForSeconds(5);
        PlayerManager.sp_used = false;

    }
}
