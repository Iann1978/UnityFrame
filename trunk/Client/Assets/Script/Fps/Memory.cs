using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Fps
{

    public class MemorableItem : IMemorableItem 
    {
        public MemorableItem(IMemorable target)
        {
            this.target = target;
            lastOccurTime = Time.time;
            timeToFoget = 5.0f;
            sensorClass = ESensorClass.Unknown;
            lastOccurPosition = target.pos;
        }
        //public MemorableItem<T>(T target)
        //{
        //    this.target = Target;
        //}

        public IMemorable target { get; set; }
        public float lastOccurTime { get; set; }
        public float timeToFoget { get; set; }
        public Vector3 lastOccurPosition { get; set; }
        public ESensorClass sensorClass;
    }

    public class Memory : MonoBehaviour, IMemory
    {
        public List<IMemorableItem> objs = new List<IMemorableItem>();
        public List<IMemorableItem> fogets = new List<IMemorableItem>();


        public void Notify(IMemorable memorable, ISensor sensor)
        {
            IMemorableItem item = Find(memorable);
            if (item != null)
            {
                item.timeToFoget = 5.0f;
                if (fogets.Contains(item))
                    fogets.Remove(item);
            }
            else
            {
                objs.Add(new MemorableItem(memorable));
            }
        }

        public IMemorableItem Find(IMemorable memable)
        {
            foreach (IMemorableItem item in objs)
            {
                if (item.target == memable)
                {
                    return item;
                }
            }
            return null;
        }

        


        public void Update()
        {
            foreach (IMemorableItem item in objs)
            {
                item.timeToFoget -= Time.deltaTime;
                if (item.timeToFoget < 0)
                {
                    fogets.Add(item);
                }
            }

            foreach (IMemorableItem item in fogets)
            {
                objs.Remove(item);
            }

            fogets.Clear();


        }


        public IMemorable GetLast()
        {
            if (objs.Count != 0)
                return objs[0].target;
            return null;
        }

        public ITarget[] AllTargets()
        {
            List<ITarget> targets = new List<ITarget>();
            foreach (IMemorableItem item in objs)
            {
                ITarget target = item.target as ITarget;
                if (target != null)
                {
                    targets.Add(target);
                }
            }
            return targets.ToArray();
        }


        public ICover[] AllCovers()
        {
            List<ICover> covers = new List<ICover>();
            foreach (IMemorableItem item in objs)
            {
                ICover cover = item.target as ICover;
                if (cover != null)
                {
                    covers.Add(cover);
                }
            }
            return covers.ToArray();
        }

    }

}
