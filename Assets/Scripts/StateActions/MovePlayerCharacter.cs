using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewGamePlus
{
    public class MovePlayerCharacter : StateAction
    {   
        PlayerStateManager _states;

        public MovePlayerCharacter(PlayerStateManager stateManager)
        {
            _states = stateManager;
        }
        public override bool Execute()
        {   Debug.Log("This is a test check");
            float frontY = 0;
            RaycastHit hit;
            Vector3 origin =_states.MTransform.position + (_states.MTransform.forward * _states.FrontRayOffset );
            origin.y += 0.5f;
            Debug.DrawRay(origin , - Vector3.up , Color.red ,  0.01f ,  false);
            if(Physics.Raycast(origin , -Vector3.up , out hit , 1 , _states.IgnoreForGroundCheck ))
            {
                float y = hit.point.y;
                frontY = y - _states.MTransform.position.y;
                
            }
            Vector3 currentVelocity = _states.RB.velocity;
            Vector3 targetVelocity = _states.MTransform.forward * _states.MoveAmount * _states.MovementSpeed;

            //if(_states.LockOn)
            //{
            //    targetVelocity = _states.RotateDirection * _states.MoveAmount * _states.MovementSpeed;
            //}

            if(_states.IsOnGrounded)
            {
                float MoveAmunt = _states.MoveAmount;

                if(MoveAmunt > 0.1f)
                {
                    _states.RB.isKinematic = false;
                    _states.RB.drag = 0;
                    if(Mathf.Abs(frontY) > 0.02F)
                    {
                        targetVelocity.y = ((frontY > 0) ? frontY + 0.2f : frontY - 0.2F) * _states.MovementSpeed;
                    }
                }
                else
                {
                    float abs = Mathf.Abs(frontY);

                    if(abs > 0.02f)
                    {
                        _states.RB.isKinematic = true;
                        targetVelocity.y = 0;
                        _states.RB.drag = 4;
                    }
                }

                HandleRotation();
                

                
                // Vector3.Lerp(currentVelocity , targetVelocity , _states.Delta * _states.AdaptSpeed);
            }
            else
			{
				//states.collider.height = colStartHeight;
				_states.RB.isKinematic = false;
				_states.RB.drag = 0;
				targetVelocity.y = currentVelocity.y;
			}


            HandleAnimations();
            Debug.DrawRay((_states.MTransform.position + Vector3.up * 0.2f ) , targetVelocity ,Color.green , 0.01f , false);
            _states.RB.velocity =   targetVelocity;

            //	Debug.Log(targetVelocity);
			//states.rigidbody.velocity = Vector3.Lerp(currentVelocity, targetVelocity, states.delta * states.adaptSpeed);

            return false;
        }

        void HandleRotation()
		{
			float h = _states.Horizontal;
			float v = _states.Vertical;

			Vector3 targetDir = _states.Camera.transform.forward * v;
			targetDir += _states.Camera.transform.right * h;
			targetDir.Normalize();

			targetDir.y = 0;
			if (targetDir == Vector3.zero)
				targetDir = _states.MTransform.forward;

			Quaternion tr = Quaternion.LookRotation(targetDir);
			Quaternion targetRotation = Quaternion.Slerp(
				_states.MTransform.rotation, tr,
				_states.Delta * _states.MoveAmount * _states.RotationSpeed);

			_states.MTransform.rotation = targetRotation;
		}

        void HandleAnimations()
		{
			if (_states.IsOnGrounded)
			{
				float m = _states.MoveAmount;
				float f = 0;
				if (m > 0 && m <= .5f)
					f = .5f;
				else if (m > 0.5f)
					f = 1;


				_states.Anim.SetFloat("forward", f, .2f, _states.Delta);
			}
			else
			{

			}
		}

    }
}

