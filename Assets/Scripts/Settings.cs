using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private Image soundSphere,
        vibroSphere;

    [SerializeField]
    private Sprite OnSpehere,
        offSphere;

    private void Start()
    {
        SetSpheres();
    }

    private void SetSpheres()
    {
        soundSphere.sprite = PlayerPrefs.GetInt("Sound", 1) == 1 ? OnSpehere : offSphere;

        vibroSphere.sprite = PlayerPrefs.GetInt("Vibro", 1) == 1 ? OnSpehere : offSphere;
    }

    public void ChangeSound()
    {
        PlayerPrefs.SetInt("Sound", PlayerPrefs.GetInt("Sound", 1) == 1 ? 0 : 1);
        PlayerPrefs.Save();
        SetSpheres();
    }

    public void ChangeVibro()
    {
        PlayerPrefs.SetInt("Vibro", PlayerPrefs.GetInt("Vibro", 1) == 1 ? 0 : 1);
        PlayerPrefs.Save();
        SetSpheres();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu();
        }
    }
}
