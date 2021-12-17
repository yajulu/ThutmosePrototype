using System;
using UnityEngine;

namespace Controllers.Player.Abilities
{
	public abstract class BaseAbilityPerformer : MonoBehaviour
	{
		protected Ability ability;

		public Ability Ability => ability;
		
		public event Action Started;
		public event Action<BaseAbilityPerformer> Canceled;
		
		public void PerformAbility(Action onStarted, Action<BaseAbilityPerformer> onCanceled)
		{
			Started = onStarted;
			Canceled = onCanceled;
			AbilityLogic();
		}

		protected virtual void AbilityLogic()
		{
			Started?.Invoke();
		}

		public void CancelAbility()
		{
			Canceled?.Invoke(this);
		}
		
	}
}