using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBoxController : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioClip);
            Destroy(gameObject, 1);
        }
    }
}