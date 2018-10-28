using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Fps
{
    class SoundSensor : MonoBehaviour, ISensor
    {
        public float sensitivity = 1.0f;

        public ESensorClass sensorClass { get { return ESensorClass.Sound; } }

        public void Notify(ISensible sensible)
        {
            SoundSensible soundSensible= sensible as SoundSensible;
            IMemorable memObj = soundSensible.GetComponent<IMemorable>();
            IMemory memory = GetComponent<Memory>();

            if (memObj != null && memory !=null)
            {
                memory.Notify(memObj, this);
            }

            Debug.Log(name + "hear " + soundSensible.name);            
        }

        //public void OnDrawGizmos()
        //{
        //    Debug.DrawRay(transform.position, transform.forward);
        //}

        public void OnDrawGizmosSelected()
        {            
            Vector3 forward = transform.forward;
            Vector3 leftEarge = Quaternion.AngleAxis(90, Vector3.up) * forward;
            Vector3 rightEarge = Quaternion.AngleAxis(-90, Vector3.up) * forward;

            Debug.DrawRay(transform.position, forward * sensitivity);
            Debug.DrawRay(transform.position, -forward * sensitivity);
            Debug.DrawRay(transform.position, leftEarge * sensitivity);
            Debug.DrawRay(transform.position, rightEarge * sensitivity);
            Vector3 vf = transform.position + forward * sensitivity;
            Vector3 vb = transform.position - forward * sensitivity;
            Vector3 vl = transform.position + leftEarge * sensitivity;
            Vector3 vr = transform.position + rightEarge * sensitivity;
            Debug.DrawLine(vf, vl);
            Debug.DrawLine(vf, vr);
            Debug.DrawLine(vb, vr);
            Debug.DrawLine(vb, vl);
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
