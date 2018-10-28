using UnityEngine;
using System.Collections;

namespace Fps
{


    /// <summary>
    /// 目标
    /// </summary>
    public interface ITarget
    {
        /// <summary>
        /// 目标的阵营
        /// </summary>
        int camp { get; }

        /// <summary>
        /// 目标的位置
        /// </summary>
        Vector3 pos { get; }
    }

    public interface ISet<T>
    {
        void Add(T t);
        void Remove(T t);
    }

    public interface ITargetSet : ISet<ITarget>
    {

    }

    /// <summary>
    /// 掩体
    /// </summary>
    public interface ICover
    {
        Vector3 pos { get; }

    }

    /// <summary>
    /// 生命体
    /// </summary>
    public interface IHealth
    {
        float hp { get; }
    }

    /// <summary>
    /// 子弹
    /// </summary>
    public interface IBullet
    {

    }







}
