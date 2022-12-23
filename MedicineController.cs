using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MedicineController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _medicineHealing;
    private bool _toggle = false;

    void Start()
    {
        _medicineHealing = GameObject.Find("MedicineHealing");
        _medicineHealing.SetActive(false);
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (PlayerManager.instance.medicine > 0)
            {
                if (!_toggle)
                {
                    _medicineHealing.SetActive(true);
                    PlayerManager.instance.medicine--;
                  
                    _toggle = true;
                    
                    PlayerManager.instance.health = 100;
                }
                else
                {
                    _medicineHealing.SetActive(false);
                    _toggle = false;
                }
            }
        }
    }
}