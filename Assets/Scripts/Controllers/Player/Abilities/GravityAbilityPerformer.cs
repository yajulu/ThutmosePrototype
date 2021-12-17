using System;
using UnityEngine;

namespace Controllers.Player.Abilities
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityAbilityPerformer : AbilityPerformerBase
    {

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            ability = AbilityManager.Instance.AbilityConfig.GravityAbility;
        }

        private void OnEnable()
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = true;
        }

        protected override void AbilityLogic()
        {
            _rigidbody.isKinematic = false;
            base.AbilityLogic();
        }

        protected override void AbilityCancelLogic()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            base.AbilityCancelLogic();
        }
    }
}
