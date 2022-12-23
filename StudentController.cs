using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StudentController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Animator animator;
    private int speed = 20;
    private Rigidbody rigidbody;
    private bool isCheck = false;
    private float xSpace = 2, zSpace = 2;
    private GameObject helicopter;
    private bool isCheckHelicopter = false;

    void Start()
    {
        player = GameObject.Find("Player");
        helicopter = GameObject.Find("Helicopter");

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheck)
        {
            Vector3 target = new Vector3(player.transform.position.x - xSpace, player.transform.position.y,
                player.transform.position.z - zSpace);
            Vector3 pos = Vector3.MoveTowards(transform.position, target,
                speed * Time.deltaTime);
            gameObject.transform.LookAt(player.transform);
            rigidbody.MovePosition(pos);
            animator.Play("Walking");
        }

        if (isCheckHelicopter)
        {
            gameObject.transform.LookAt(helicopter.transform);
            Vector3 targetHelicopter = new Vector3(helicopter.transform.position.x, helicopter.transform.position.y,
                helicopter.transform.position.z);
            Vector3 vector3 = Vector3.MoveTowards(transform.position, targetHelicopter,
                speed * Time.deltaTime);
            rigidbody.MovePosition(vector3);
            animator.Play("Walking");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isCheck = true;
        }
        
        if (other.collider.CompareTag("Helicopter"))
        {
            Destroy(gameObject, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoTo"))
        {
            speed = 30;
            isCheck = false;
            isCheckHelicopter = true;
            Debug.Log("Cham may bay");
        }
    }
}