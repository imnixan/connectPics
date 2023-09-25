using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int fieldSize;
    private PicsManager pics;
    private TimeManager timeManager;
    private GameAnimManager gameAnim;

    [SerializeField]
    private AudioClip cardSound;

    public void Start()
    {
        timeManager = GetComponent<TimeManager>();
        gameAnim = GetComponent<GameAnimManager>();
        gameAnim.Init();
        pics = GetComponent<PicsManager>();
        fieldSize = (int)pics.FillField();
        if (fieldSize == 0)
        {
            Utils.CloseApp();
        }
        gameAnim.StartGame();
    }

    public void StartGame()
    {
        timeManager = GetComponent<TimeManager>();
        timeManager.StartTime();
    }

    public void CorrectConnect()
    {
        fieldSize--;
        if (PlayerPrefs.GetInt("Vibro", 1) == 1)
        {
            Handheld.Vibrate();
        }

        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            AudioSource.PlayClipAtPoint(cardSound, Vector2.zero);
        }

        if (fieldSize == 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        float levelTime = timeManager.GetFinishTime();
        if (levelTime < PlayerPrefs.GetFloat("TimeRecord", Mathf.Infinity))
        {
            PlayerPrefs.GetFloat("TimeRecord", levelTime);
            PlayerPrefs.Save();
        }
        gameAnim.EndGame();
    }

    public void Exit()
    {
        gameAnim.ExitScene("");
    }

    public void Menu()
    {
        gameAnim.ExitScene("Menu");
    }

    public void Restart()
    {
        gameAnim.ExitScene("Game");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
        }
    }
}
