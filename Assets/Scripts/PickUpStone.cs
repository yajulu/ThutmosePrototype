using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickUpStone : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textUI;


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textUI.text = "Stone";
          
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textUI.text = "";
        }
    }
}
