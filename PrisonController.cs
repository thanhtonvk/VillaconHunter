using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Animator animator;
    private Rigidbody rigidbody;
    private float speed = 2;
    private int health = 20;
    private bool isDead = false;
    private bool attack = false;
    private AudioSource audioSource;
    public AudioClip zombieAttack, zombieDie, zombieRun, zombieIsFired;

    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (health < 0)
            {
                isDead = true;
                Debug.Log("DEAD");
                animator.Play("Dying");

                audioSource.PlayOneShot(zombieDie);

                Destroy(gameObject, 3);
            }

            if (checkNear())
            {
                if (!isDead)
                {
                    if (checkNear())
                    {
                        Vector3 pos = Vector3.MoveTowards(transform.position, player.transform.position,
                            speed * Time.deltaTime);
                        //I will use these two built-in functions to follow the player

                        gameObject.transform.LookAt(player.transform);
                        rigidbody.MovePosition(pos);
                        if (attack)
                        {
                            if (!audioSource.isPlaying)
                            {
                                audioSource.PlayOneShot(zombieAttack);
                            }


                            animator.Play("Zombie Attack");
                        }
                        else
                        {
                            if (!audioSource.isPlaying)
                            {
                                audioSource.PlayOneShot(zombieRun);
                            }


                            animator.Play("Walking");
                        }
                    }
                    else
                    {
                        if (!audioSource.isPlaying)
                        {
                        }

                        animator.Play("Zombie Transition");
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player") && !isDead)
        {
            attack = true;
            PlayerManager.instance.isAttacked(5);
            Debug.Log(attack);
        }

        if (other.collider.CompareTag("Bullet") && !isDead)
        {
            Debug.Log("impulse" + other.impulse.y);

            audioSource.PlayOneShot(zombieIsFired);

            float y = other.impulse.y;
            if (y > 100)
            {
                PlayerManager.instance.playHeadShot();
                health = 0;
            }

            health -= 1;
            Debug.Log("HEALth" + health);

            animator.Play("Zombie Attack");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            attack = false;
        }
    }

    private float playerX, playerZ;
    private float thresholdX = 100, thresholdZ = 100;

    public bool checkNear()
    {
        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;
        float playerY = player.transform.position.y;

        if (!transform.Equals(null))
        {
            if (transform.position.x - thresholdX < playerX &&
                playerX < transform.position.x + thresholdX &&
                transform.position.z - thresholdZ < playerZ &&
                playerZ < transform.position.z + thresholdZ)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}