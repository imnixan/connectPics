using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tutorText;

    [SerializeField]
    private Button tutorBtn;

    [SerializeField]
    private RectTransform tutorRT;

    [SerializeField]
    private Image tutorImage;

    [SerializeField]
    private Sprite incorrect,
        play;

    private GameManager gm;

    public void Show(GameManager gameManager)
    {
        this.gm = gameManager;

        tutorRT.DOAnchorPosX(0, 0.5f).Play();
        tutorBtn.onClick.AddListener(ShowNextTutor);
    }

    private void ShowNextTutor()
    {
        tutorBtn.onClick.AddListener(Hide);
        tutorImage.sprite = incorrect;
        tutorText.text = "You cannot connect cards through others";
        tutorBtn.image.sprite = play;
    }

    public void Hide()
    {
        tutorRT.DOAnchorPosX(-1000, 0.3f).Play();
        gm.StartCallback();
    }
}
