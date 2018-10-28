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
    public class StandShoot : ActionNode {

        public float timer = 0;
        Memory memory;
        UnityEngine.AI.NavMeshAgent agent;

        public override void Reset()
        {
        }

        public override void Start()
        {
            memory = self.GetComponent<Memory>();
            agent = self.GetComponent<UnityEngine.AI.NavMeshAgent>();
            timer = 2.0f;
        }



        public override Status Update()
        {
            if (memory == null)
                return Status.Error;

            BaseAIParameters param = self.GetComponent<BaseAIParameters>();
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                param.shotting = !param.shotting;
                if (param.shotting)
                    param.fire = true;
                timer = 2.0f;
            }

            return Status.Running;
        }
    }
}