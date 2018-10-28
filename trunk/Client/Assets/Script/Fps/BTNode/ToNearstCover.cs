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
    public class ToNearstCover : ActionNode
    {

        Memory memory;
        UnityEngine.AI.NavMeshAgent agent;

        public override void Reset()
        {
        }

        public override void Start()
        {
            memory = self.GetComponent<Memory>();
            agent = self.GetComponent<UnityEngine.AI.NavMeshAgent>();
        }


        ICover NearstCover()
        {
            ICover[] covers = memory.AllCovers();
            if (covers.Length != 0)
                return covers[0];
            return null;
        }

        public override Status Update()
        {
            if (!memory)
                return Status.Error;

            ICover nearstCover = NearstCover();

            if (nearstCover == null)
                return Status.Failure;

            agent.SetDestination(nearstCover.pos);
            agent.Resume();
            return Status.Running;            
        }
    }
}