using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Fps
{
    class SightSensor : MonoBehaviour, ISensor
    {
        public float fieldOfView = 45;
        public float viewDistance = 100.0f;

        public ESensorClass sensorClass { get { return ESensorClass.Sight; } }

        public void Notify(ISensible sensible)
        {
            SightSensible sightSensible = sensible as SightSensible;
            IMemorable memObj = sightSensible.GetComponent<IMemorable>();
            IMemory memory = GetComponent<Memory>();

            if (memObj != null && memory !=null)
            {
                memory.Notify(memObj, this);
            }

            Debug.Log(name + "find " + sightSensible.name);            
        }

        //public void OnDrawGizmos()
        //{
        //    Debug.DrawRay(transform.position, transform.forward);
        //}

        public void OnDrawGizmosSelected()
        {
            Vector3 forward = transform.forward;
            Vector3 leftEarge = Quaternion.AngleAxis(fieldOfView, Vector3.up) * forward;
            Vector3 rightEarge = Quaternion.AngleAxis(-fieldOfView, Vector3.up) * forward;

            Debug.DrawRay(transform.position, forward * viewDistance);
            Debug.DrawRay(transform.position, leftEarge * viewDistance);
            Debug.DrawRay(transform.position, rightEarge * viewDistance);
            Vector3 vf = transform.position + forward * viewDistance;
            Vector3 vl = transform.position + leftEarge * viewDistance;
            Vector3 vr = transform.position + rightEarge * viewDistance;
            Debug.DrawLine(vf, vl);
            Debug.DrawLine(vf, vr);
        }

        public void OnEnable()
        {
            SensorManager.me.RegistSensor(this);
        }

        public void OnDisable()
        {
            SensorManager.me.UnregistSensor(this);
        }
    }
    
}
