using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Fps
{

    class SightSensible : MonoBehaviour, ISensible
    {
        public ESensorClass sensorClass { get { return ESensorClass.Sight; } }

        public bool CanBeSense(ISensor sensor)
        {
            if (sensorClass != sensor.sensorClass)
                return false;

            SightSensor sightSensor = sensor as SightSensor;
            float distance = Vector3.Distance(transform.position, sightSensor.transform.position);
            if (distance > sightSensor.viewDistance)
                return false;

            Vector3 rayDirection = transform.position - sightSensor.transform.position;
            if (Vector3.Angle(rayDirection, sightSensor.transform.forward) > sightSensor.fieldOfView)
                return false;

            return true;
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
