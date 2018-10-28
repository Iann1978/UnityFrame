using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.Frame;

namespace Fps
{
    class SensorManager : SingletonMonoBehaviourNoCreate<SensorManager>, ISensorManager
    {
        List<ISensible> sensibles = new List<ISensible>();
        List<ISensor> sensors = new List<ISensor>();
        List<ISensible> sensiblesToRemove = new List<ISensible>();
        List<ISensor> sensorsToRemove = new List<ISensor>();

        public void RegistSensible(ISensible sensible)
        {
            if (!sensibles.Contains(sensible))
                sensibles.Add(sensible);
        }

        public void RegistSensor(ISensor sensor)
        {
            if (!sensors.Contains(sensor))
                sensors.Add(sensor);
        }
      

        public void Update()
        {
            foreach (ISensor s in sensors)
            {
                foreach (ISensible t in sensibles)
                {
                    t.Try(s);
                }
            }

            foreach (ISensible t in sensiblesToRemove)
            {
                sensibles.Remove(t);
            }
            sensiblesToRemove.Clear();

            foreach (ISensor s in sensorsToRemove)
            {
                sensors.Remove(s);
            }
            sensorsToRemove.Clear();
        }

        public void UnregistSensor(ISensor sensor)
        {
            sensorsToRemove.Add(sensor);
        }

        public void UnregistSensible(ISensible sensible)
        {
            sensiblesToRemove.Add(sensible);
        }
    }
}
