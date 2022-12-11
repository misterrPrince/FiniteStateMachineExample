using UnityEngine;
using System.Collections;

namespace NewGamePlus
{
	public class MonitorInteractingAnimation : StateAction
	{
		CharacterStateManager _states;
		string _targetBool;
		string _targetState;

		public MonitorInteractingAnimation(CharacterStateManager characterStateManager, string targetBool, string targetState)
		{
			_states = characterStateManager;
			this._targetBool = targetBool;
			this._targetState = targetState;
		}

		public override bool Execute()
		{
			bool isInteracting = _states.Anim.GetBool(_targetBool);

			if (isInteracting)
			{
				return false;
			}
			else
			{
				_states.ChangeState(_targetState);

				return true;
			}
		}
	}
}
