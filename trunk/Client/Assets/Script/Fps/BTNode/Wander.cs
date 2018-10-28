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
    public class Wander : ActionNode {



        public override void Reset()
        {
        }

        public override void Start()
        {
            BaseAIParameters param = self.GetComponent<BaseAIParameters>();
            param.shotting = false;
        }


        public override Status Update()
        {
            
            BaseAIParameters param = self.GetComponent<BaseAIParameters>();
            if (!param)
            {
                return Status.Error;
            }

            //if (Vector3.Distance(self.transform.position, param.targetVector) < 2)
            //{
            //    return Status.Success;
            //}

            UnityEngine.AI.NavMeshAgent agent = self.GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.Stop();
            //agent.SetDestination(param.targetVector);
            return Status.Success;
        }
    }
}