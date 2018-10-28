//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using Fps;

namespace BehaviourMachine
{
    /// <summary>
    /// Returns Success if the Animation component is playing an animation; Failure otherwise.
    /// </summary>
    [NodeInfo(category = "Demo/Condition/",
                icon = "Animation",
                description = "Returns Success if the Animation component is playing an animation; Failure otherwise",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Animation-isPlaying.html")]
    public class IfLessToTarget : ConditionNode
    {

        Memory memory;
        BaseAIParameters param;
        public float threshold;

        public override void Reset()
        {
        }

        public override void Start()
        {
            memory = self.GetComponent<Memory>();
            param = self.GetComponent<BaseAIParameters>();
        }

        public override Status Update()
        {
            if (!memory || !param)
                return Status.Error;

            if (param.disToMemeyTarget < threshold)
                return Status.Success;
            else
                return Status.Failure;
        }
    }
}