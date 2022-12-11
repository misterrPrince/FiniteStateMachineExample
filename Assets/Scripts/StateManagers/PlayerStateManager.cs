using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewGamePlus
{
    public class PlayerStateManager : CharacterStateManager
    {   
        [Header("Inputs")]
        public float MouseX;
        public float MouseY;
        public float MoveAmount;
        public Vector3 RotateDirection;
        [Header("States")]
        public bool IsOnGrounded;

        [Header("References")]
		public new Transform Camera;

        [Header("Variables")]
        public float Horizontal;
        public  const string LocomotionId ="locomotion";
        public  string AttackState = "attackState";


        [HideInInspector]
        public LayerMask IgnoreForGroundCheck;
         
    
        [HideInInspector]

        [Header("MovementStats")]
        public float FrontRayOffset =.5F;
        public float MovementSpeed =2;
        public float AdaptSpeed = 1;
        public float RotationSpeed;


        
        public override void Init()
        {  
            Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>It is a Test"); 
            base.Init();

            State _locomotion = new State(
                new List<StateAction>() // FixedUpdate
                {
                    
                    new MovePlayerCharacter(this)
                }
                ,
                new List<StateAction>() // Update
                {
                    new InputManager(this)
                }
                ,
                new List<StateAction>() // LateUpdate
                {

                }

            );

            _locomotion.On_Enter = DisableRootMotion;


             State _attackState = new State(
                new List<StateAction>() // FixedUpdate
                {

                }
                ,
                new List<StateAction>() // Update
                {
                    new MonitorInteractingAnimation(this,"isInteracting", LocomotionId),
                }
                ,
                new List<StateAction>() // LateUpdate
                {

                }

            );

            RegisterState( LocomotionId , _locomotion);
            RegisterState(AttackState , _locomotion);

            ChangeState( LocomotionId);
            IgnoreForGroundCheck = ~(1 << 9 | 1 << 10);
            
        }

        void FixedUpdate()
        {   Delta = Time.fixedDeltaTime;
            base.FixedTick();
        }
        void Update()
        {   Delta = Time.deltaTime;
            base.Tick();
        }
        
        void LateUpdate()
        {
            base.LateTick();
        }

#region StateEvents
        void DisableRootMotion()
		{
			UseRootMotion = false;
		}

		void EnableRootMotion()
		{
			UseRootMotion = true;
		}
#endregion

       
    }
}

