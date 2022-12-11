using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewGamePlus
{
    public class AnimatorHook : MonoBehaviour
    {
        CharacterStateManager _states;

		public virtual void Init(CharacterStateManager stateManager)
		{
			_states = (CharacterStateManager)stateManager;
		}

		public void OnAnimatorMove()
		{
			OnAnimatorMoveOverrride();
		}

		protected virtual void OnAnimatorMoveOverrride()
		{
			if (_states.UseRootMotion == false)
				return;

			if (_states.IsGrounded && _states.Delta > 0)
			{
				Vector3 v = (_states.Anim.deltaPosition) / _states.Delta;
				v.y = _states.RB.velocity.y;
				_states.RB.velocity = v;
			}
		}
    }

}

