using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieShow : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject zombieYard;
    public string name;

    void Start()
    {
        zombieYard = GameObject.Find(name);
        zombieYard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            zombieYard.SetActive(true);
        }
    }
}