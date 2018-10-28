using UnityEditor;
using UnityEngine;
using System.Reflection;


namespace Assets.Script.Frame
{
    //自定义Tset脚本
    [CustomEditor(typeof(FrameLogConfig))]
    //在编辑模式下执行脚本，这里用处不大可以删除。
    [ExecuteInEditMode]
    //请继承Editor
    public class FrameLogConfigEditor : UnityEditor.Editor
    {

        //在这里方法中就可以绘制面板。
        public override void OnInspectorGUI()
        {
            FrameLogConfig tar = (FrameLogConfig)target;
            ////绘制一个窗口
            //test.mRectValue = EditorGUILayout.RectField("窗口坐标",
            //        test.mRectValue);
            string defines = "";
            MemberInfo[] mems = tar.GetType().GetMembers();
            foreach (MemberInfo m in mems)
            {
                if (m.MemberType == MemberTypes.Field)
                {
                    FieldInfo f = m as FieldInfo;
                    bool tog = (bool)f.GetValue(tar);
                    tog = EditorGUILayout.Toggle(f.Name, tog);
                    f.SetValue(tar, tog);
                    if (tog)
                    {
                        defines += f.Name + ";";
                    }
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, defines);




        }
    }

}

