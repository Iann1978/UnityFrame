//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine {

    /// <summary>
    /// Moves the "Game Object" in the direction and distance of "Translation".
    /// <seealso cref="BehaviourMachine.MovePosition" />
    /// </summary>
    [NodeInfo(  category = "Demo/Action/",
                icon = "Transform",
                description = "Moves the \"Game Object\" in the direction and distance of \"Translation\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform.Translate.html")]
    public class SetTargetAsDestination : ActionNode {



        public override void Reset()
        {
        }

        public override void Start()
        {
        }


        public override Status Update()
        {
            BaseAIParameters param = self.GetComponent<BaseAIParameters>();
            if (!param || param.target == null)
            {
                return Status.Error;
            }


            param.destination = param.target.pos;
            param.destinationEnable = true;

            return Status.Success;
            
        }
    }
}