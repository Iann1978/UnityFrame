using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.Frame
{
    class FMSState
    {
        public virtual void OnLeave()
        {

        }

        public virtual void OnEnter()
        {

        }

        public virtual void Update()
        {

        }
    }

    class FMSManager
    {
        FMSState[] states;
        int stateNumber;
        int curStateId;
        int nextStateId;

        public FMSManager(int stateNumber)
        {
            this.stateNumber = stateNumber;
            states = new FMSState[stateNumber];
            curStateId = -1;
        }

        public void SetState(int stateId, FMSState state)
        {
            states[stateId] = state;
        }

        public void ChangeState(int stateId)
        {
            if (nextStateId != -1)
            {
                Debug.LogWarning("FMSManager.ChangeState");
            }
            nextStateId = stateId;
        }

        void RealChangeState()
        {
            if (curStateId != -1)
                states[curStateId].OnLeave();
            else
                Debug.LogWarning("");

            states[nextStateId].OnEnter();

            curStateId = nextStateId;

            nextStateId = -1;
        }

        public void Update()
        {
            if (nextStateId != -1)
            {
                RealChangeState();
            }

            if (curStateId != -1)
            {
                states[curStateId].Update();
            }
                
        }
    }
}
