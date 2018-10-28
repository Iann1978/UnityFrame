using UnityEngine;
using System.Collections;
using Assets.Script.Frame;
using System.Collections.Generic;

namespace Fps
{
    public class TargetManager : Singleton<TargetManager>
    {
        public List<ITarget> targets = new List<ITarget>();
        public void AddTarget(ITarget target)
        {
            targets.Add(target);
        }

        public void RemoveTarget(ITarget target)
        {
            targets.Remove(target);
        }
    }

    public class CoverManager : Singleton<CoverManager>
    {
        public List<ICover> covers = new List<ICover>();
    }
}
