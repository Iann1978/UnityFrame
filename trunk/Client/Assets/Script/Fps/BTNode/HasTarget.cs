//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;
using Fps;
namespace BehaviourMachine {
    /// <summary>
    /// Returns Success if the Animation component is playing an animation; Failure otherwise.
    /// </summary>
    [NodeInfo(  category = "Demo/Condition/",
                icon = "Animation",
                description = "Returns Success if the Animation component is playing an animation; Failure otherwise",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Animation-isPlaying.html")]
    public class HasTarget : ConditionNode {

    	
        public override Status Update () {

            Memory memory = self.GetComponent<Memory>();
            if (!memory)
                return Status.Error;

            //BaseAIParameters param = self.GetComponent<BaseAIParameters>();
            //if (!param)
            //    return Status.Error;
            ITarget[] targets = memory.AllTargets();
            if (targets == null)
                return Status.Failure;

            if (targets.Length == 0)
                return Status.Failure;

            return Status.Success;

            //ITarget target = targets[0];

            

            //IMemorable target = memory.GetLast();

            //return target != null ? Status.Success : Status.Failure;
            //return Status.Failure;

            
        }
    }
}