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

    [SerializeField]
    private TutorManager tutorManager;

    private AudioSource sound;

    public void Start()
    {
        sound = GetComponent<AudioSource>();
        timeManager = GetComponent<TimeManager>();
        gameAnim = GetComponent<GameAnimManager>();
        gameAnim.Init();
        pics = GetComponent<PicsManager>();
        fieldSize = (int)pics.FillField();
        if (fieldSize == 0)
        {
            Utils.CloseApp();
        }
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            sound.Play();
        }
        if (!PlayerPrefs.HasKey("TimeRecord"))
        {
            tutorManager.Show(this);
        }
        else
        {
            StartCallback();
        }
    }

    public void StartCallback()
    {
        gameAnim.StartGame();
    }

    public void StartGame()
    {
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
            sound.PlayOneShot(cardSound);
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
            PlayerPrefs.SetFloat("TimeRecord", levelTime);
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
