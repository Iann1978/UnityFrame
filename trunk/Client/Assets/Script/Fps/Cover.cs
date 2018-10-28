using UnityEngine;
using System.Collections;
using System;

namespace Fps
{
    public class Cover : MonoBehaviour, ICover, IMemorable
    {
        public Vector3 pos { get { return transform.position; } }
    }
}

