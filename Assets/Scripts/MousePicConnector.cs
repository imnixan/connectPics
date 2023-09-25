using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePicConnector : MonoBehaviour
{
    private bool mouseHold;

    [SerializeField]
    private List<LineElement> line = new List<LineElement>();

    [SerializeField]
    private GameObject circlePrefab;

    [SerializeField]
    private Transform circlesParent;

    [SerializeField]
    private Sprite normal,
        choosed;

    private List<GameObject> circles = new List<GameObject>();
    private GameAnimManager animMan;
    private GameManager gm;
    private RaycastHit2D rayHit;
    private LineRenderer lr;
    private Camera camera;

    private void Start()
    {
        animMan = GetComponentInParent<GameAnimManager>();
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
                firstElement.rectangle.sprite = choosed;
                mouseHold = true;
                line.Add(firstElement);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ClearLine();
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
        pic.rectangle.sprite = choosed;
        Sprite firstPic = line[0].image.sprite;
        Sprite lastPic = pic.image.sprite;
        if (firstPic == lastPic)
        {
            OnCorrectConnect();
        }
        ClearLine();
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
            CheckTurn(i);
        }
        Vector2 onFingerPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lr.SetPosition(lr.positionCount - 1, onFingerPoint);
    }

    private void CheckTurn(int pointIndex)
    {
        if (pointIndex >= 2)
        {
            Vector2 lastPoint = lr.GetPosition(pointIndex);
            Vector2 potentialTurn = lr.GetPosition(pointIndex - 1);
            Vector2 prevPoint = lr.GetPosition(pointIndex - 2);

            if (
                (lastPoint.x != potentialTurn.x && potentialTurn.x == prevPoint.x)
                || (lastPoint.y != potentialTurn.y && potentialTurn.y == prevPoint.y)
            )
            {
                circles.Add(
                    Instantiate(circlePrefab, potentialTurn, new Quaternion(), circlesParent)
                );
            }
        }
    }

    private void ClearLine()
    {
        foreach (LineElement pic in line)
        {
            if (pic is Pic)
            {
                pic.rectangle.sprite = normal;
            }
        }
        line.Clear();
        foreach (var circle in circles)
        {
            Destroy(circle);
        }
        lr.positionCount = 0;
        circles.Clear();
    }
}
