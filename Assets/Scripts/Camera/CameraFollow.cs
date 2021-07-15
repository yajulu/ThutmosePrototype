using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform playerToFollow;
    [SerializeField]
    float defaultSmoothSpeed;
    float currentSmoothSpeed;
    [SerializeField]
    Vector3 cameraOffset;
    TimeAbilitiesController playerAbilitiesController;
    PlayerMovement playerMovementController;

    Vector3 desiredPos;
    Vector3 smoothedPos;


    private void Awake()
    {
        playerAbilitiesController = playerToFollow.GetComponent<TimeAbilitiesController>();
      
        currentSmoothSpeed = defaultSmoothSpeed;
    }

    private void LateUpdate()
    {
        desiredPos = playerToFollow.position + cameraOffset;
        if (playerAbilitiesController.isCurrentlyRewinding )
        {
            smoothedPos = Vector3.Lerp(transform.position, desiredPos, 15 * Time.deltaTime);
        }
        else
        {
            smoothedPos = Vector3.Lerp(transform.position, desiredPos, currentSmoothSpeed * Time.deltaTime);
            transform.LookAt(playerToFollow);
        }
        transform.position = smoothedPos;
       
    }


}
