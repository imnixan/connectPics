using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pic : LineElement
{
    public Vector3 oldPos;

    protected override void Awake()
    {
        base.Awake();
        image = transform.GetChild(0).GetComponent<Image>();
        rectangle = GetComponent<Image>();
    }

    public void SaveOldPos()
    {
        oldPos = transform.position;
    }
}
