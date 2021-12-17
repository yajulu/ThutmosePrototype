using System;
using UnityEngine;

namespace Controllers.Player.Abilities
{
	public abstract class AbilityPerformerBase : MonoBehaviour
	{
		protected Ability ability;
		protected bool isAbilityStarted;
		public Ability Ability => ability;
		
		public event Action Started;
		public event Action<AbilityPerformerBase> Canceled;
		
		public void PerformAbility(Action onStarted, Action<AbilityPerformerBase> onCanceled)
		{
			Started = onStarted;
			Canceled = onCanceled;
			isAbilityStarted = true;
			AbilityLogic();
		}

		protected virtual void AbilityLogic()
		{
			Started?.Invoke();
		}

		public void CancelAbility()
		{
			AbilityCancelLogic();
			isAbilityStarted = false;
		}

		protected virtual void AbilityCancelLogic()
		{
			Canceled?.Invoke(this);
		}
	}
}