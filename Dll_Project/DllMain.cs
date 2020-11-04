using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dll_Project
{
    public static class DllMain
    {
        public static List<GameObject> ClickObjs = new List<GameObject>();
        public static List<GameObject> ShowObjs = new List<GameObject>();
        public static void Main()
        {
            UnityEngine.Debug.Log("Dll Run Main !");

            ClickObjs.Clear();
            ShowObjs.Clear();

            foreach (var obj in DllManager.Instance.ExtralDatas)
            {
                if (obj.Target != null)
                {
                    if (obj.OtherData.Equals("0"))
                    {
                        ClickObjs.Add(obj.Target.gameObject);
                    }
                    else if(obj.OtherData.Equals("1"))
                    {
                        ShowObjs.Add(obj.Target.gameObject);
                    }
                }
            }

            var extralData = DllManager.Instance.transform.GetComponent<HFExtralData>();

            foreach (var obj in extralData.ExtralDatas)
            {
                if (obj.Target != null)
                {
                    obj.Target.gameObject.SetActive(true);
                }
            }

            //com.ootii.Messages.MessageDispatcher.SendMessage(DllManager.Instance, "STARTED", "Whoo Hoo!", 0);
        }
    }
}
