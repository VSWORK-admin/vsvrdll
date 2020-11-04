﻿using com.ootii.Messages;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_show_hideDemo : DllGenerateBase
{
    public VRPointObjEventType PointEventType = VRPointObjEventType.VRPointClick;

    public GameObject ClickedObj;
    public GameObject showItem;
    public GameObject Recieve_ShowItem;

    private Tweener tween;

    public string clickedName = string.Empty;
    public string showItemName = string.Empty;

    public string Recieve_A = string.Empty;
    public string Recieve_B = string.Empty;
    public override void OnEnable()
    {
        base.OnEnable();

        MessageDispatcher.AddListener(PointEventType.ToString(), GetPointEventType);
        MessageDispatcher.AddListener(WsMessageType.RecieveCChangeObj.ToString(), RecieveCChangeObj);
    }

    public override void OnDisable()
    {
        base.OnDisable();

        MessageDispatcher.RemoveListener(PointEventType.ToString(), GetPointEventType);
        MessageDispatcher.RemoveListener(WsMessageType.RecieveCChangeObj.ToString(), RecieveCChangeObj);
    }

    void GetPointEventType(IMessage msg)
    {
        GameObject pobj = msg.Data as GameObject;
        ClickedObj = pobj;

        HandleGetPointedObj();
    }

    void HandleGetPointedObj()
    {
        clickedName = ClickedObj.name + " has clicked !";

        if (mStaticThings.I == null) { return; }
        WsChangeInfo wsinfo = new WsChangeInfo()
        {
            id = mStaticThings.I.mAvatarID,
            name = "InfoLog",
            a = clickedName,
            b = InfoColor.green.ToString(),
            c = 5.ToString(),
        };

        MessageDispatcher.SendMessage(this, VrDispMessageType.SendInfolog.ToString(), wsinfo, 0);

        int _id = -1;

        if (ClickedObj == null || ClickedObj.Equals(null))
        {
            _id = Dll_Project.DllMain.ClickObjs.FindIndex(x => x == null || x.Equals(null));
        }
        else
        {
            _id = Dll_Project.DllMain.ClickObjs.IndexOf(ClickedObj);
        }

        var _iscontained = _id != -1;

        if (!_iscontained) return;

        if (_id >= Dll_Project.DllMain.ShowObjs.Count) return;

        showItem = Dll_Project.DllMain.ShowObjs[_id];

        showItemName = showItem.name;

        if (showItem.gameObject.activeInHierarchy)
        {
            WsCChangeInfo wsinfo1 = new WsCChangeInfo()
            {
                a = "showitem",
                b = showItemName,
                c = string.Empty,
                d = string.Empty,
                e = string.Empty,
                f = string.Empty,
                g = string.Empty,
            };

            MessageDispatcher.SendMessage(this, WsMessageType.SendCChangeObj.ToString(), wsinfo1, 0);
        }
        else
        {
            WsCChangeInfo wsinfo1 = new WsCChangeInfo()
            {
                a = "hideitem",
                b = showItemName,
                c = string.Empty,
                d = string.Empty,
                e = string.Empty,
                f = string.Empty,
                g = string.Empty,
            };

            MessageDispatcher.SendMessage(this, WsMessageType.SendCChangeObj.ToString(), wsinfo1, 0);
        }

    }


    void RecieveCChangeObj(IMessage msg)
    {
        WsCChangeInfo rinfo = msg.Data as WsCChangeInfo;

        Recieve_A = rinfo.a;
        Recieve_B = rinfo.b;
        //c.Value = rinfo.c;
        //d.Value = rinfo.d;
        //e.Value = rinfo.e;
        //f.Value = rinfo.f;
        //g.Value = rinfo.g;

        HandleCChangeObj();
    }
    
    void HandleCChangeObj()
    {
        foreach (var obj in Dll_Project.DllMain.ShowObjs)
        {
            if (obj != null && obj.name.Equals(Recieve_B))
            {
                Recieve_ShowItem = obj;
                break;
            }
        }

        if (Recieve_ShowItem == null) return;

        if (Recieve_A.Equals("showitem"))
        {
            Recieve_ShowItem.SetActive(true);
            tween = Recieve_ShowItem.GetComponent<Transform>().DOScale(Vector3.one, 0.5f);

            tween.SetAutoKill(true);
        }
        else if (Recieve_A.Equals("hideitem"))
        {
            tween = Recieve_ShowItem.GetComponent<Transform>().DOScale(Vector3.zero, 0.5f);

            tween.SetAutoKill(true);

            tween.OnComplete(() => { Recieve_ShowItem.SetActive(false); });
        }
    }
}