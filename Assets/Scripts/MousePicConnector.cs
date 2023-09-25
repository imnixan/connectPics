using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePicConnector : MonoBehaviour
{
    private bool mouseHold;

    [SerializeField]
    private List<LineElement> line = new List<LineElement>();

    private AnimationManager animMan;
    private GameManager gm;
    private RaycastHit2D rayHit;
    private LineRenderer lr;
    private Camera camera;

    private void Start()
    {
        animMan = GetComponentInParent<AnimationManager>();
        gm = GetComponentInParent<GameManager>();
        camera = Camera.main;
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LineElement firstElement = GetClickElement();
            if (firstElement && firstElement is Pic)
            {
                mouseHold = true;
                line.Add(firstElement);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            line.Clear();
            mouseHold = false;
            lr.positionCount = 0;
        }

        if (mouseHold)
        {
            MakeLine();
            UpdateGraphicLine();
        }
    }

    private void MakeLine()
    {
        LineElement lineElement = GetClickElement();
        if (lineElement)
        {
            if (lineElement && CanAddElement(lineElement))
            {
                line.Add(lineElement);
                if (lineElement is Pic && line.Count > 1)
                {
                    CheckConnect(lineElement as Pic);
                }
            }
        }
    }

    private void CheckConnect(Pic pic)
    {
        mouseHold = false;
        Sprite firstPic = line[0].image.sprite;
        Sprite lastPic = pic.image.sprite;
        if (firstPic == lastPic)
        {
            OnCorrectConnect();
        }
        line.Clear();
    }

    private void OnCorrectConnect()
    {
        gm.CorrectConnect();
        animMan.DelCorrectConnect(line[0] as Pic, new Vector3(20, 20, 0));
        animMan.DelCorrectConnect(line[line.Count - 1] as Pic, new Vector3(10, 0, 0));
    }

    private LineElement GetClickElement()
    {
        rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (
            rayHit.collider
            && (
                rayHit.collider.gameObject.CompareTag("Pics")
                || rayHit.collider.gameObject.CompareTag("Border")
            )
        )
        {
            return rayHit.collider.gameObject.GetComponent<LineElement>();
        }
        return null;
    }

    private bool CanAddElement(LineElement newElement)
    {
        Vector2 elementPos = newElement.Position;
        Vector2 lastlementPos = line[line.Count - 1].Position;
        bool horChangeed = elementPos.x != lastlementPos.x;
        bool vertChanged = elementPos.y != lastlementPos.y;
        bool notDiagonal = horChangeed ^ vertChanged;
        bool neighbor = Vector2.Distance(elementPos, lastlementPos) <= newElement.MaxSide;
        return notDiagonal && neighbor && !line.Contains(newElement);
    }

    private void UpdateGraphicLine()
    {
        lr.positionCount = line.Count + 1;
        for (int i = 0; i < line.Count; i++)
        {
            lr.SetPosition(i, line[i].Position);
        }
        lr.SetPosition(lr.positionCount - 1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
