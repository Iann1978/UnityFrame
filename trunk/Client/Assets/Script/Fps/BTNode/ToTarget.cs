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
    public class ToTarget : ActionNode
    {

        Memory memory;
        UnityEngine.AI.NavMeshAgent agent;
        public float disAsArrive = 15.0f;

        public override void Reset()
        {
        }

        public override void Start()
        {
            memory = self.GetComponent<Memory>();
            agent = self.GetComponent<UnityEngine.AI.NavMeshAgent>();
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
            if (!memory)
                return Status.Error;

            ITarget target = MemoryTarget();
            if (target == null)
            {
                return Status.Failure;
            }

            Vector3 targetPos = MemoryTargetPosition(target);

            float dis = Vector3.Distance(self.transform.position, targetPos);
            if (dis < disAsArrive)
            {
                agent.Stop();
                return Status.Success;
            }

            agent.SetDestination(targetPos);
            agent.Resume();
            return Status.Running;
        }
    }
}