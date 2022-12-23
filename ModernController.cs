using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModernController : MonoBehaviour
{
    // Start is called before the first frame update

    //nhân vật
    private GameObject player;

    //animation
    private Animator animator;

    private Rigidbody rigidbody;

    //tốc độ di chuyển
    private float speed = 10;

    //máu
    private int health = 10;

    //trạng thái chết
    private bool isDead = false;

    //trạng thái tấn công
    private bool attack = false;

    //âm thanh
    private AudioSource audioSource;
    public AudioClip zombieAttack, zombieDie, zombieRun, zombieIsFired;

    void Start()
    {
        //tìm đối tượng player
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
            //nếu tìm thấy người chơi trong ngưỡng zombie
            if (checkNear())
            {
                //âm thanh khi zombie chạy
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(zombieRun);
                }


                // lấy ra tọa độ để zombie di chuyển tới người
                Vector3 pos = Vector3.MoveTowards(transform.position, player.transform.position,
                    speed * Time.deltaTime);
                //I will use these two built-in functions to follow the player

                //nhìn theo nhân vật
                gameObject.transform.LookAt(player.transform);

                //di chuyển tới chỗ nhân vật
                rigidbody.MovePosition(pos);


                // kiểm tra trạng thái tấn công
                if (attack)
                {
                    // phát âm thanh taasan công của zombie
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(zombieAttack);
                    }

                    //play animation khi tấn công
                    animator.Play("Z_Attack");
                }
                else
                {
                    // phát âm thanh di chuyển của zombie 
                    if (!audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(zombieRun);
                    }
                    //play animation của zombie khi chạy

                    animator.Play("Z_Run_InPlace");
                }
            }
          
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        //kiểm tra va chạm của zombie với người
        if (other.collider.CompareTag("Player") && !isDead)
        {
            // zombie sang chế độ tấn công
            attack = true;
            
            // mỗi lần tấn công mất 5hp
            PlayerManager.instance.isAttacked(5);
            // play âm thanh khi tấn công
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(zombieAttack);
            }
        }
        
        // kiểm tra va chạm zombie với đạn
        if (other.collider.CompareTag("Bullet") && !isDead)
        {
            // play âm thanh khi bị dính đạn
            audioSource.PlayOneShot(zombieIsFired);
            
            
            Debug.Log(other.impulse.y);
            
            
            // kiểm tra vị trí trúng đạn
            float y = other.impulse.y;
            if (y > 100)
            {
                // nếu y>100 headshot
                PlayerManager.instance.playHeadShot();
                health = 0;
            }
            // bình thường trừ 1
            health -= 1;
            //play animation khi tấn công
            animator.Play("Z_Attack");
        }

        // kiểm tra khi bị hp dưới 0
        if (health < 0)
        {
            // play âm thanh chết
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(zombieDie);
            }
            // chuyern trajgn thái thành die
            isDead = true;
            // play animation chết
            animator.Play("Z_FallingForward");
            // xóa object sau 2s
            Destroy(gameObject, 2);
        }
    }
// kiểm tra khi rời khỏi player
    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            // trạng thái tấn công = false.
            attack = false;
        }
    }

    private float playerX, playerZ;

    //kieerem tra khoảng cách giữa zombie và người = 100
    private float thresholdX = 100, thresholdZ = 100;


    public bool checkNear()
    {
        // get vị trí x z của người chơi
        playerX = player.transform.position.x;
        playerZ = player.transform.position.z;


        if (!transform.Equals(null))
        {
            // kiểm tra trong khoảng +-100
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