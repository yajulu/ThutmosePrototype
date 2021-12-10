using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeAbilitiesController : MonoBehaviour
{
    Old_PlayerInput _oldPlayerInput;
    PlayerMovement playerMovement;
    SlingShotController slingShotController;
    CharacterController playerController;
    GameObject playerMesh;
    GameObject playerReplica;
    [SerializeField]
    GameObject fakePlayerPrefab;
    [HideInInspector]
    public GameObject fakePlayer;

    #region Rewind Varaibles
    List<Vector3> trackingList;
    [HideInInspector]
    public bool isCurrentlyRewinding;
    #endregion
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<CharacterController>();
        slingShotController = GetComponent<SlingShotController>();
        playerMesh = transform.GetChild(0).gameObject;
        playerReplica = transform.GetChild(2).gameObject;
        _oldPlayerInput = new Old_PlayerInput();
        InitializeInput();
        trackingList = new List<Vector3>();
        StartCoroutine(PlayerTracker());
    }

    private void OnEnable()
    {
        _oldPlayerInput.CharacterActionMap.Enable();
    }

    private void OnDisable()
    {
        _oldPlayerInput.CharacterActionMap.Disable();
    }
    void InitializeInput()
    {
        _oldPlayerInput.CharacterActionMap.TimeAbility.started += OnAbilityInput;
        _oldPlayerInput.CharacterActionMap.TimeAbility.canceled += OnAbilityInput;
        _oldPlayerInput.CharacterActionMap.TimeAbility.started += InstantiateFakePlayer;
        _oldPlayerInput.CharacterActionMap.TimeAbility.canceled += DissolveFakePlayer;

    }


    void OnAbilityInput(InputAction.CallbackContext context)
    {
        if (!slingShotController.isCurrentlyAiming)
        {
            isCurrentlyRewinding = context.ReadValueAsButton();
            playerMovement.Stop();
            playerController.enabled = !isCurrentlyRewinding;
            playerMesh.SetActive(!isCurrentlyRewinding);
            playerReplica.SetActive(isCurrentlyRewinding);
          
            

            if (isCurrentlyRewinding)
                StartCoroutine(ActivateRewind());
            else
            {
                playerMovement.enabled = true;
                StartCoroutine(PlayerTracker());
                trackingList.Clear();
            }
        }
      
           
    }


    void InstantiateFakePlayer(InputAction.CallbackContext context)
    {
        if (!slingShotController.isCurrentlyAiming && isCurrentlyRewinding)
        {
            fakePlayer = Instantiate(fakePlayerPrefab, transform.position, Quaternion.identity);
        }
        
    }


    void DissolveFakePlayer(InputAction.CallbackContext context)
    {
        if (fakePlayer != null)
        {
            fakePlayer.transform.GetChild(0).GetComponent<Animator>().SetTrigger("DissolveTrig");
        }

    }


    IEnumerator ActivateRewind()
    {
        while (isCurrentlyRewinding)
        {
            if (trackingList.Count > 0)
            {
               
                StartCoroutine(TweenMovement(trackingList[trackingList.Count - 1]));
                
                trackingList.RemoveAt(trackingList.Count - 1);

               
              
            }
            yield return new WaitForSeconds(0.2f);
           
        }
        TogglePlayer(true);

    }
    IEnumerator PlayerTracker()
    {
        while (!isCurrentlyRewinding)
        {
            
            trackingList.Add(transform.position);
           
            if (trackingList.Count > 10)
            {
                trackingList.RemoveAt(0);
            }

            yield return new WaitForSeconds(0.5f);
            
        }
    }

    IEnumerator TweenMovement(Vector3 endPos)
    {
        float t = 0;
        while (t <= 0.2f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, endPos, Mathf.SmoothStep(0f, 1f, t));
           
           
            yield return null;
        }
        transform.position = endPos;
    }


    void TogglePlayer(bool isActivate)
    {
        
        playerMovement.enabled = isActivate;
        playerController.enabled = isActivate;
        playerMesh.SetActive(isActivate);
        playerReplica.SetActive(!isActivate);
        
    }
}
