using UnityEngine;
using System.Collections;
using LuaInterface;

namespace Assets.Script.Frame
{

    public class LuaSystem : Singleton<LuaSystem>
    {
        public LuaState lua = null;
        public void Init()
        {
            lua = new LuaState();
            lua.Start();
            LuaBinder.Bind(lua);
        }
    }
}
