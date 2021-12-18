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
        // Debug.Log("TaG ENTER: " + other.tag);
        if (other.CompareTag("Player"))
        {
            textUI.text = "Stone";
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        // Debug.Log("TaG EXIT: " + other.tag);

        if (other.CompareTag("Player"))
        {
            textUI.text = "";
        }
    }
}
