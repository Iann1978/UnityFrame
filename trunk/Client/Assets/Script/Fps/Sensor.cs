using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Fps
{
    /// <summary>
    /// 感知器类别（掩码）
    /// </summary>
    public enum ESensorClass
    {
        Unknown = 0,
        Sight = 1 << 0,
        Sound = 1 << 1,
    }

    /// <summary>
    /// 感知器
    /// </summary>
    public interface ISensor
    {
        /// <summary>
        /// 可探测类型（掩码）
        /// </summary>
        ESensorClass sensorClass { get; }

        void Notify(ISensible sensible);
    }

    /// <summary>
    /// 可探测物
    /// </summary>
    public interface ISensible
    {
        /// <summary>
        /// 可被探测类型（掩码）
        /// </summary>
        ESensorClass sensorClass { get; }

        bool CanBeSense(ISensor sensor);

        void Try(ISensor sensor);
    }

    // 感知管理器
    public interface ISensorManager
    {
        void RegistSensor(ISensor sensor);
        void RegistSensible(ISensible sensible);
        void UnregistSensor(ISensor sensor);
        void UnregistSensible(ISensible sensible);

        void Update();
    }

    /// <summary>
    /// 可记忆体的类别（掩码）
    /// </summary>
    public enum EMemorableClass
    {
        Target = 1 << 0,
        Sound = 1 << 1,
    }

    public interface IMemory
    {
        void Notify(IMemorable memorable, ISensor sensor);
    }

    public interface IMemorable
    {
        Vector3 pos { get; }
    }

    public interface IMemorableItem
    {
        //float lastOccurTime { get; set; }
        //float timeToFoget { get; set; }
        float timeToFoget { get; set; }
        IMemorable target { get; set; }
        Vector3 lastOccurPosition { get; set; }

    }

}