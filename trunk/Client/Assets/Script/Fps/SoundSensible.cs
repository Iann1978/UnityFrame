using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Fps
{

    class SoundSensible : MonoBehaviour, ISensible
    {
        public float attenuation;
        public float volume;
        public ESensorClass sensorClass { get { return ESensorClass.Sound; } }

        public bool CanBeSense(ISensor sensor)
        {
            if (sensorClass != sensor.sensorClass)
                return false;

            SoundSensor soundSensor = sensor as SoundSensor;
            float distance = Vector3.Distance(transform.position, soundSensor.transform.position);

            if (volume / distance > soundSensor.sensitivity)
                return true;

            return false;
        }

        public void Update()
        {
           
        }

        public void FixedUpdate()
        {
            volume = volume * attenuation;
        }

        public void Try(ISensor sensor)
        {
            if (CanBeSense(sensor))
            {
                sensor.Notify(this);
            }
        }

        public void OnEnable()
        {
            SensorManager.me.RegistSensible(this);
        }

        public void OnDisable()
        {
            SensorManager.me.UnregistSensible(this);
        }
    }
}
