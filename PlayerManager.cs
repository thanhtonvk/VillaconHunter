using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public AudioClip headShot;

    public int medicine = 1;
    public int bottle = 0;
    public int dna = 0;
    public int health = 100;

    private AudioSource audioSource;


    public Text healthText, dnaText, bottleText, medicineText;

    // Start is called before the first frame update
    void Start()
    {
        Instance();
        audioSource = GetComponent<AudioSource>();
        healthText.text = health + "";
        dnaText.text = dna + "/10";
        bottleText.text = bottle + "/1";
        medicineText.text = medicine + "";
    }

    void Instance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Update is called once per frame
    void Update()
    {
        healthText.text = health + "";

        healthText.text = health + "";
        dnaText.text = dna + "/10";
        bottleText.text = bottle + "/1";
        medicineText.text = medicine + "";
    }

    public void pickBottle()
    {
        if (bottle == 0)
        {
            bottle++;
            bottleText.text = bottle + "/1";
        }
    }

    public void pickDNA()
    {
        dna++;
        dnaText.text = dna + "/10";
    }

    public void pickMedicine()
    {
        medicine++;
        medicineText.text = medicine + "";
    }


    public void isAttacked(int hp)
    {
        new WaitForSeconds(1);
        health -= hp;
    }

    public void playHeadShot()
    {
        audioSource.PlayOneShot(headShot);
    }
}