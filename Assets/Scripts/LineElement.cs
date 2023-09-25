using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LineElement : MonoBehaviour
{
    public RectTransform rt;
    public Image rectangle,
        image;
    public Vector2 AnchPosition
    {
        get { return rt.anchoredPosition; }
        set { rt.anchoredPosition = value; }
    }

    public float MaxSide
    {
        get { return Mathf.Max(rt.sizeDelta.x, rt.sizeDelta.y); }
    }

    public Vector2 Position
    {
        get { return rt.position; }
        set { rt.position = value; }
    }

    protected virtual void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
}
