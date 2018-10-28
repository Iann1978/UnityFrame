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
    public class HasCover : ConditionNode {

        Memory memory;

        public override void Reset()
        {
        }

        public override void Start()
        {
            memory = self.GetComponent<Memory>();
        }
        
        public override Status Update ()
        {

            if (!memory)
                return Status.Error;

            ICover[] covers = memory.AllCovers();
            if (covers == null)
                return Status.Failure;

            if (covers.Length == 0)
                return Status.Failure;

            return Status.Success;
        }
    }
}