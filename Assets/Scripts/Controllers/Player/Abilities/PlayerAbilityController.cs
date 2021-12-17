using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers.Player.Abilities
{
    [RequireComponent(typeof(InputController))]
    public class PlayerAbilityController : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private AbilitiesConfig abilitiesConfig;

        [Header("Ability Settings")] [SerializeField]
        private float maxAbilityPoints = 100;
        
        [Space(10), Header("Debug")]
        [SerializeField, ReadOnly] private BaseAbilityPerformer selectedAbilityPerformer;
        [SerializeField, ReadOnly] private Ability selectedAbility;
        [SerializeField, ReadOnly] private float currentAbilityPoints;
        [SerializeField, ReadOnly] private bool isPerformingAbility;
        [SerializeField, ReadOnly] private bool isAbilityStarted;

        private InputController _inputController;
        private void Awake()
        {
            _inputController = GetComponent<InputController>();
        }

        private void Start()
        {
            selectedAbilityPerformer = null;
            currentAbilityPoints = maxAbilityPoints;
        }
        private void Update()
        {
            AbilityFinder();
            AbilityHandler();
        }
        
        private void AbilityFinder()
        {
            //Skip finder if we already performing an ability
            if (isPerformingAbility)
                return;
            //Find abilities

            if (selectedAbilityPerformer != null)
            {
                selectedAbility = selectedAbilityPerformer.Ability;
            }
        }

        private void AbilityHandler()
        {
            switch (_inputController.performAbility)
            {
                case true when !isPerformingAbility:
                {
                    if (selectedAbilityPerformer != null)
                    {
                        //Perform Ability Logic
                        if (selectedAbility.Cost < currentAbilityPoints)
                        {
                            isPerformingAbility = true;
                            selectedAbilityPerformer.PerformAbility(OnAbilityStarted, OnAbilityCanceled);
                        }
                    }
                    else
                    {
                        //Reset perform Ability Input
                        _inputController.performAbility = false;
                    }

                    break;
                }
                case true when isPerformingAbility && isAbilityStarted:
                {
                    currentAbilityPoints -= selectedAbility.Cost * Time.deltaTime;

                    if (currentAbilityPoints <= 0)
                    {
                        selectedAbilityPerformer.CancelAbility();
                    }
                    break;
                }
                case false when isPerformingAbility:
                {
                    if (selectedAbilityPerformer != null)
                    {
                        selectedAbilityPerformer.CancelAbility();
                    }

                    break;
                }
            }
        }

        private void OnAbilityStarted()
        {
            if (!selectedAbility.IsTimeBased)
            {
                currentAbilityPoints -= selectedAbility.Cost;
            }
            else
            {
                isAbilityStarted = true;
            }
        }
        private void OnAbilityCanceled(BaseAbilityPerformer abilityPerformer)
        {
            // selectedAbilityPerformer.Canceled -= OnAbilityCanceled;
            if (abilityPerformer.Ability.IsTimeBased)
            {
                isPerformingAbility = false;
                selectedAbilityPerformer = null;    
            }
        }
    }
}
