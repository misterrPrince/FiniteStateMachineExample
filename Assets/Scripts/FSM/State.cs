using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewGamePlus
{
    public class State 
    {
        // Start is called before the first frame update
        bool _forceExit;
        List<StateAction> _fixedUpdateActions;
        List<StateAction> _updateActions;
        List<StateAction> _lateUpdateActions;

        public delegate void OnEnter();
		public OnEnter On_Enter;

        public State(List<StateAction> fixedUpdateActions , List<StateAction> updateActions , List<StateAction> lateUpdateActions)
        {
            this._fixedUpdateActions = fixedUpdateActions;
            this._updateActions = updateActions;
            this._lateUpdateActions = lateUpdateActions;
        }

        public void FixedTick()
        {
            ExecuteListOfActions(_fixedUpdateActions);
        }

        public void Tick()
        {
            ExecuteListOfActions(_updateActions);

        }

        public void LateTick()
        {
            ExecuteListOfActions(_lateUpdateActions);
            _forceExit = false;
        }

        void ExecuteListOfActions(List<StateAction> l)
        {
            for(int i = 0 ; i < l.Count ; i++)
            {
                if(_forceExit)
                    return;
                
                _forceExit = l[i].Execute();

            }
        }
        
        
    }

}
