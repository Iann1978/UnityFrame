//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

namespace BehaviourMachine
{

    /// <summary>
    /// Moves the "Game Object" in the direction and distance of "Translation".
    /// <seealso cref="BehaviourMachine.MovePosition" />
    /// </summary>
    [NodeInfo(category = "Demo/Action/",
                icon = "Transform",
                description = "Moves the \"Game Object\" in the direction and distance of \"Translation\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform.Translate.html")]
    public class Stop : ActionNode
    {



        public override void Reset()
        {
        }

        public override void Start()
        {
        }


        public override Status Update()
        {
            UnityEngine.AI.NavMeshAgent agent = self.GetComponent<UnityEngine.AI.NavMeshAgent>();            
            agent.Stop();
            return Status.Success;
        }
    }
}