using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dll_Project
{
    public class TestNewClass
    {
        public string showInfo = "showInfo: HoffixTestMono Init !";
        public string showInfo1 = "showInfo1: HoffixTestMono Awake !";
    }

    public class HoffixTestMono : DllGenerateBase
    {
        TestNewClass testNewClass = new TestNewClass();

        public override void Init()
        {
            base.Init();

            Debug.Log(testNewClass.showInfo);
        }
        public override void Awake()
        {
            base.Awake();

            if(BaseMono.ExtralDatas.Length > 0)
            {
                BaseMono.ExtralDatas[0].Target.gameObject.SetActive(false);
            }

            Debug.Log(testNewClass.showInfo1);
        }

        public override void Start()
        {
            base.Start();

            Debug.Log("HoffixTestMono Start !");
        }

        public override void OnEnable()
        {
            base.OnEnable();

            Debug.Log("HoffixTestMono OnEnable !");
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Debug.Log("HoffixTestMono OnDisable !");
        }
    }
}