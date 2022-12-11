using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewGamePlus
{

    public abstract class  CharacterStateManager : StateManager
    {
        [Header("Referneces")]
        [HideInInspector] public Animator Anim;
        [HideInInspector] public new Rigidbody RB;
        	public AnimatorHook AnimHook;

        [Header("ControllerValues")]
        public float Vertical;
        public float Horizontal;
        public bool LockOn;
        public float Delta;

        [Header("States")]
		public bool IsGrounded;
		public bool UseRootMotion;

        



        public override void Init()
        {
            Anim = GetComponentInChildren<Animator>();
            RB = GetComponentInChildren<Rigidbody>();
            AnimHook =  GetComponentInChildren<AnimatorHook>();
            Anim.applyRootMotion = false;
            AnimHook.Init(this);
        }

        public void PlayTargetAnimation(string targetAnim , bool isInteracting)
        {   

            Anim.CrossFade(targetAnim , 0.2f);
            Anim.SetBool("isInteracting", isInteracting);
        }
        
    }
}


