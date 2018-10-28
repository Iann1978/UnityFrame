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
    /// Moves the "Game Object" in the direction and distance of "Translation".
    /// <seealso cref="BehaviourMachine.MovePosition" />
    /// </summary>
    [NodeInfo(category = "Demo/Action/",
                icon = "Transform",
                description = "Moves the \"Game Object\" in the direction and distance of \"Translation\"",
                url = "http://docs.unity3d.com/Documentation/ScriptReference/Transform.Translate.html")]
    public class CalDistanceToTarget : ActionNode
    {
        Memory memory;
        UnityEngine.AI.NavMeshAgent agent;
        BaseAIParameters param;

        public override void Reset()
        {
        }

        public override void Start()
        {
            memory = self.GetComponent<Memory>();
            agent = self.GetComponent<UnityEngine.AI.NavMeshAgent>();
            param = self.GetComponent<BaseAIParameters>();
        }

        ITarget MemoryTarget()
        {
            ITarget[] allTargets = memory.AllTargets();
            if (allTargets.Length > 0)
                return allTargets[0];
            return null;
        }

        Vector3 MemoryTargetPosition(ITarget target)
        {
            IMemorableItem memItem = memory.Find(target as IMemorable);            
            return memItem.lastOccurPosition;
        }

        public override Status Update()
        {
            if (!memory || !param)
                return Status.Error;

            ITarget target = MemoryTarget();
            if (target == null)
                return Status.Error;

            Vector3 memTargetPos = MemoryTargetPosition(target);
            float disToMemTarget = Vector3.Distance(self.transform.position, memTargetPos);
            param.memTargetPosition = memTargetPos;
            param.disToMemeyTarget = disToMemTarget;
            return Status.Success;
        }
    }
}