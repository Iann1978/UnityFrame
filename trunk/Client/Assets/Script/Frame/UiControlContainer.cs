// UiControlContainer.cs
// Author: Iann


using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Script.Frame
{
    /// <summary>
    /// 控件容器
    /// </summary>
    public class UiControlContainer : MonoBehaviour
    {
        //各种类型的器皿
        private Dictionary<string, Button> buttons = new Dictionary<string, Button>();
        private Dictionary<string, Image> images = new Dictionary<string, Image>();
        private Dictionary<string, InputField> inputs = new Dictionary<string, InputField>();
        private Dictionary<string, Text> texts = new Dictionary<string, Text>();
        private Dictionary<string, Slider> sliders = new Dictionary<string, Slider>();
        private Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
        public Dictionary<string, PanelBase> panelbases = new Dictionary<string, PanelBase>();

        /// <summary>
        /// 泛型的注册函数
        /// </summary>
        /// <typeparam name="T">注册的类型</typeparam>
        /// <param name="datas">注册用到的器皿</param>
        /// <param name="name">控件名称</param>
        /// <param name="unit">控件Instance</param>
        /// <returns></returns>
        private bool RegistT<T>(Dictionary<string, T> datas, string name, T unit)
        {
            if (datas.ContainsKey(name))
            {
                Debug.LogError("name(" + name + ") already exist");
                return false;
            }
            datas.Add(name, unit);
            return true;
        }

        /// <summary>
        /// 泛型的反注册函数
        /// </summary>
        /// <typeparam name="T">反注册的类型</typeparam>
        /// <param name="datas">反注册用到的器皿</param>
        /// <param name="name">控件名称</param>
        /// <returns></returns>
        private bool UnRegistT<T>(Dictionary<string, T> datas, string name)
        {
            if (!datas.ContainsKey(name))
            {
                Debug.LogError("name not exist");
                return false;
            }
            datas.Remove(name);
            return true;
        }

        /// <summary>
        /// 注册GameObject
        /// </summary>
        /// <param name="name">GameObject的名字</param>
        /// <param name="obj">实例</param>
        /// <returns></returns>
        public bool RegistObject(string name, GameObject obj)
        {
            return RegistT(objects, name, obj);
        }

        /// <summary>
        /// 反注册GameObject
        /// </summary>
        /// <param name="name">GameObject的名字</param>
        /// <returns></returns>
        public bool UnRegistObject(string name)
        {
            return UnRegistT(objects, name);
        }

        /// <summary>
        /// 获得GameObeject
        /// </summary>
        /// <param name="name">GameObject的名字</param>
        /// <returns></returns>
        public GameObject GetObject(string name)
        {
            if (objects.ContainsKey(name))
                return objects[name];
            return null;
        }

        // Button
        public bool RegistButton(string name, Button button)
        {
            return RegistT(buttons, name, button);
        }

        public bool UnRegistButton(string name)
        {
            return UnRegistT(buttons, name);
        }

        public Button GetButton(string name)
        {
            if (buttons.ContainsKey(name))
                return buttons[name];
            return null;
        }


        // Text
        public bool RegistText(string name, Text text)
        {
            return RegistT(texts, name, text);
        }

        public bool UnRegistText(string name)
        {
            return UnRegistT(texts, name);
        }

        public Text GetText(string name)
        {
            if (texts.ContainsKey(name))
                return texts[name];
            return null;
        }

        // Slider
        public bool RegistSlider(string name, Slider slid)
        {
            return RegistT(sliders, name, slid);
        }

        public bool UnRegistSlider(string name)
        {
            return UnRegistT(sliders, name);
        }

        public Slider GetSlider(string name)
        {
            if (sliders.ContainsKey(name))
                return sliders[name];
            return null;
        }

        // Image 
        public bool RegistImage(string name, Image image)
        {
            return RegistT(images, name, image);
        }

        public bool UnRegistImage(string name)
        {
            return UnRegistT(images, name);
        }

        public Image GetImage(string name)
        {
            if (images.ContainsKey(name))
                return images[name];
            return null;
        }


        // ImputFiled 
        public bool RegistInputField(string name, InputField inputFiled)
        {
            return RegistT(inputs, name, inputFiled);
        }

        public bool UnRegistInputField(string name)
        {
            return UnRegistT(inputs, name);
        }

        public InputField GetInputField(string name)
        {
            if (inputs.ContainsKey(name))
                return inputs[name];
            return null;
        }

        // PanelBase
        public bool RegistPanelBase(string name, PanelBase panelBase)
        {
            return RegistT(panelbases, name, panelBase);
        }

        public bool UnRegistPanelBase(string name)
        {
            return UnRegistT(panelbases, name);
        }

        public PanelBase GetPanelBase(string name)
        {
            if (panelbases.ContainsKey(name))
                return panelbases[name];
            return null;
        }
    }

}
