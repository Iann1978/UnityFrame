using UnityEngine;
using System.Collections;
using System;

namespace Fps
{
    public class Target : MonoBehaviour, ITarget, IMemorable
    {
        public int _camp;
        public int camp { get { return _camp; } }
        public Vector3 pos { get { return transform.position; } }


        void OnEnable()
        {
            TargetManager.me.AddTarget(this);
        }

        void OnDisable()
        {
            TargetManager.me.RemoveTarget(this);
        }
    }
}

