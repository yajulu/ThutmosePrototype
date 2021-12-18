using System;
using UnityEngine;

namespace Controllers.Player.Abilities
{
	public abstract class AbilityPerformerBase : MonoBehaviour
	{
		protected Ability ability;
		protected bool isAbilityStarted;
		protected bool isAbilitySelected;
		public Ability Ability => ability;

		public bool IsAbilityStarted => isAbilityStarted;
		public bool IsAbilitySelected => isAbilitySelected;
		
		public event Action Started;
		public event Action Selected;
		public event Action<AbilityPerformerBase> Canceled;
		public event Action<AbilityPerformerBase> Deselected;

		
		private Action _playerStartedAction;
		private Action _playerSelectedAction;
		
		private Action<AbilityPerformerBase> _playerCanceledAction;
		private Action<AbilityPerformerBase> _playerDeselectedAction;
		
		public void SelectAbility(Action onSelected)
		{
			_playerSelectedAction = onSelected;
			Selected += _playerSelectedAction;
			SelectedLogic();
			isAbilitySelected = true;
		}
		public void DeselectAbility(Action<AbilityPerformerBase> onDeselected)
		{
			_playerDeselectedAction = onDeselected;
			Deselected += _playerDeselectedAction;
			DeselectedLogic();
			isAbilitySelected = false;
		}
		public void PerformAbility(Action onStarted, Action<AbilityPerformerBase> onCanceled)
		{
			_playerStartedAction = onStarted;
			_playerCanceledAction = onCanceled;
			Started += _playerStartedAction;
			Canceled += _playerCanceledAction;
			AbilityStartedLogic();
			isAbilityStarted = true;
		}
		public void CancelAbility()
		{
			AbilityCancelLogic();
			isAbilityStarted = false;
		}
		protected virtual void SelectedLogic()
		{
			Selected?.Invoke();
			Selected -= _playerSelectedAction;
		}
		protected virtual void DeselectedLogic()
		{
			Deselected?.Invoke(this);
			Deselected -= _playerDeselectedAction;
		}
		protected virtual void AbilityStartedLogic()
		{
			Started?.Invoke();
			Started -= _playerStartedAction;
		}
		protected virtual void AbilityCancelLogic()
		{
			Canceled?.Invoke(this);
			Canceled -= _playerCanceledAction;
		}
	}
}