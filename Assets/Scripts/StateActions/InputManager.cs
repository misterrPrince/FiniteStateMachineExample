using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGamePlus
{

    public class InputManager : StateAction
    {
        // Start is called before the first frame update
        PlayerStateManager s;


        

        //Triggers
        bool _isAttacking;
        bool _Rb;
        bool _Rt;
        bool _Lb;
        bool _Lt;
        //Inventory
        bool _inventoryInput;
        // prompts
        bool _bInput;
        bool _yInput;
        bool _xInput;
        //Dpad
        bool _leftArrow;
        bool _rightArrow;
        bool _upArrow;
        bool _downArrow;

        public InputManager(PlayerStateManager states)
        {
            s = states;
        }
        public override bool Execute()
        {   

            
            bool retValue;
            _isAttacking = false;
           
           s.Horizontal = Input.GetAxis("Horizontal");
           s.Vertical = Input.GetAxis("Vertical");
           _Rb = Input.GetButton("RB");
           _Rt = Input.GetButton("RT");
           _Lb = Input.GetButton("LB");
           _Lt = Input.GetButton("LT");
           _inventoryInput =  Input.GetButton("Inventory");
           _bInput =  Input.GetButton("B");
           _yInput =  Input.GetButton("Y");
           _xInput =  Input.GetButton("X");  
           _leftArrow =  Input.GetButton("Left");
           _rightArrow =  Input.GetButton("Right");
           _upArrow =  Input.GetButton("Up");
           _downArrow =  Input.GetButton("Down");
           s.MouseX = Input.GetAxis("Mouse X");
           s.MouseY = Input.GetAxis("Mouse Y");
           s.MoveAmount = Mathf.Clamp01(Mathf.Abs(s.Horizontal) + Mathf.Abs(s.Vertical));
           
           retValue = HandleAttacking();
           
           return retValue;
        }

        bool HandleAttacking()
        {
            if(_Rb || _Rt || _Lb || _Lt)
                _isAttacking = true;
            

           if (_yInput)
			{
				_isAttacking = false;
			}

			if (_isAttacking)
			{
				//Find the actual attack animation from the items etc.
				//play animation
				s.PlayTargetAnimation("Attack 1", true);
				s.ChangeState(s.AttackState);
			}

            return _isAttacking;
        }
    }

}
