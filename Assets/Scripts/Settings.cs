using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private Image sliderSound,
        slideVibro;

    [SerializeField]
    private RectTransform vibroKnob,
        soundKnob;

    [SerializeField]
    private float yOn,
        yOff;

    [SerializeField]
    private Sprite onSlider,
        offSlider;

    private void Start()
    {
        SetSliders();
    }

    private void SetSliders()
    {
        sliderSound.sprite = PlayerPrefs.GetInt("Sound", 1) == 1 ? onSlider : offSlider;
        soundKnob.anchoredPosition = new Vector2(
            soundKnob.anchoredPosition.x,
            sliderSound.sprite == onSlider ? yOn : yOff
        );

        slideVibro.sprite = PlayerPrefs.GetInt("Vibro", 1) == 1 ? onSlider : offSlider;
        vibroKnob.anchoredPosition = new Vector2(
            vibroKnob.anchoredPosition.x,
            slideVibro.sprite == onSlider ? yOn : yOff
        );
    }

    public void ChangeSound()
    {
        PlayerPrefs.SetInt("Sound", PlayerPrefs.GetInt("Sound", 1) == 1 ? 0 : 1);
        PlayerPrefs.Save();
        SetSliders();
    }

    public void ChangeVibro()
    {
        PlayerPrefs.SetInt("Vibro", PlayerPrefs.GetInt("Vibro", 1) == 1 ? 0 : 1);
        PlayerPrefs.Save();
        SetSliders();
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
