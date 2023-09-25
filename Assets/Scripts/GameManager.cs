using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int fieldSize;
    private PicsManager pics;

    private void Start()
    {
        pics = GetComponent<PicsManager>();
        fieldSize = (int)pics.FillField();
        if (fieldSize == 0)
        {
            Utils.CloseApp();
        }
    }

    public void CorrectConnect()
    {
        fieldSize--;
        if (fieldSize == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("EndGame");
    }
}
