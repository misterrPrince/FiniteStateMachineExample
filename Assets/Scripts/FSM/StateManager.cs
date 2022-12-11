using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewGamePlus
{
    public abstract class StateManager : MonoBehaviour
    {
        // Start is called before the first frame update
        State _currentState;
        Dictionary <string ,State> _allStates = new Dictionary<string, State>(); 
        
        [HideInInspector]
        public Transform MTransform;

        void Start()
        {
            MTransform = this.transform;
            Init();

        }

        // Update is called once per frame
        public abstract void Init();
       
        public void FixedTick()
        {
            if(_currentState == null)
                return;
            
            _currentState.FixedTick();
            
        }

        public void Tick()
        {
            if(_currentState == null)
                return;
            
            _currentState.Tick();
        }

        public void LateTick()
        {
            if(_currentState == null)
                return;

            _currentState.LateTick();
            
        }

        public void ChangeState(string targetId)
        {
                if(_currentState != null)
                {
                    // Run on exit actions of CurrentState
                }

                State targetState = GetState(targetId);
                // Run on enter actions;
                _currentState = targetState;
        }

        State GetState(string targetId)
        {
            _allStates.TryGetValue(targetId , out State retVal);
            return retVal;
        }

        protected void RegisterState(string stateId , State state)
        {
            _allStates.Add(stateId , state);
        }
       
    }

}
