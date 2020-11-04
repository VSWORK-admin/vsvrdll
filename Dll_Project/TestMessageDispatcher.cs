using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMessageDispatcher : DllGenerateBase
{
    public string ShowbStr = string.Empty;
    public override void Init()
    {
        base.Init();

        Debug.Log("HoffixTestMono Init !");
    }
    public override void Awake()
    {
        base.Awake();

        Debug.Log("HoffixTestMono Awake !");
    }

    public override void Start()
    {
        base.Start();

        if (mStaticThings.I != null && mStaticThings.I.LeftHand != null)
        {
            Debug.Log(mStaticThings.I.LeftHand.position.ToString());
        }

        Debug.Log("HoffixTestMono Start !");
    }

    public override void OnEnable()
    {
        base.OnEnable();

        MessageDispatcher.AddListener(WsMessageType.RecieveCChangeObj.ToString(), RecieveCChangeObj);

        Debug.Log("HoffixTestMono OnEnable !");

        //var extralData = DllManager.Instance.transform.GetDllComponent<TestMessageDispatcher>().BaseMono.ExtralDatas;
        //Debug.LogWarning(BaseMono.ExtralDatas);

        if (BaseMono.ExtralDatas != null && BaseMono.ExtralDatas.Length > 0)
        {
            BaseMono.ExtralDatas[0].Target.gameObject.SetActive(true);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        MessageDispatcher.RemoveListener(WsMessageType.RecieveCChangeObj.ToString(), RecieveCChangeObj);
        Debug.Log("HoffixTestMono OnDisable !");
    }

    public float time = 0;

    public override void Update()
    {
        base.Update();

        time += Time.deltaTime;

        if(time >= 2)
        {
            WsCChangeInfo rinfo = new WsCChangeInfo();

            rinfo.a = string.Empty;
            rinfo.b = "TestShow -> " + time.ToString();
            rinfo.c = string.Empty;
            rinfo.d = string.Empty;
            rinfo.e = string.Empty;
            rinfo.f = string.Empty;
            rinfo.g = string.Empty;

            MessageDispatcher.SendMessage(this, WsMessageType.RecieveCChangeObj.ToString(), rinfo, 0);

            time = 0;
        }
    }

    void RecieveCChangeObj(IMessage msg)
    {
        WsCChangeInfo rinfo = msg.Data as WsCChangeInfo;

        //a.Value = rinfo.a;
        //b.Value = rinfo.b;
        //c.Value = rinfo.c;
        //d.Value = rinfo.d;
        //e.Value = rinfo.e;
        //f.Value = rinfo.f;
        //g.Value = rinfo.g;

        ShowbStr = rinfo.b;
        if(mStaticThings.I != null) { 
            WsChangeInfo wsinfo = new WsChangeInfo()
            {
                id = mStaticThings.I.mAvatarID,
                name = "InfoLog",
                a = "ssssssssssssssss",
                b =  InfoColor.green.ToString(),
                c = (1f).ToString()
            };
            MessageDispatcher.SendMessage(this, VrDispMessageType.SendInfolog.ToString(), wsinfo, 0);
        }
    }

    public override void OnGUI()
    {
        base.OnGUI();

        GUI.skin.label.fontSize = 20;

        GUI.Label(new Rect(10, 10, Screen.height, Screen.width), ShowbStr);
    }
}