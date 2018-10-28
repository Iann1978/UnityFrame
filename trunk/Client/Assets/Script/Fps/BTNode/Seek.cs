//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using System.Collections;

using Fps;
namespace BehaviourMachine {

    /// <summary>
    /// Moves the "Game Object" in the direction and distance of "Translation".
    /// <seealso cref="BehaviourMachine.MovePosition" />
    /// </summary>
    [NodeInfo(  category = "Demo/Action/",
                icon = "Transform",
                description = "Moves the \"Game Object\" in the direction and distance of \"Translation\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform.Translate.html")]
    public class Seek : ActionNode {



        public override void Reset()
        {
        }

        public override void Start()
        {
        }


        public override Status Update()
        {
            //BaseAIParameters param = self.GetComponent<BaseAIParameters>();
            //if (!param || !param.destinationEnable)
            //{
            //    return Status.Failure;
            //}

            //if (Vector3.Distance(self.transform.position, param.destination) < param.endSeekingDistance)
            //{
            //    return Status.Success;
            //}

            Memory memory = self.GetComponent<Memory>();
            if (!memory)
                return Status.Failure;

            
            ITarget[] targets = memory.AllTargets();
            bool hasTarget = targets.Length > 0;
            if (hasTarget)
            {
                ITarget target = targets[0];
                IMemorable memorable = target as IMemorable;
                IMemorableItem memItem = memory.Find(memorable);
                Vector3 destination = memItem.lastOccurPosition;
                UnityEngine.AI.NavMeshAgent agent = self.GetComponent<UnityEngine.AI.NavMeshAgent>();
                agent.SetDestination(destination);
                agent.Resume();
                return Status.Running;
            }
            else
            {
                UnityEngine.AI.NavMeshAgent agent = self.GetComponent<UnityEngine.AI.NavMeshAgent>();
                agent.Stop();
                return Status.Failure;
            }
        }
    }
}