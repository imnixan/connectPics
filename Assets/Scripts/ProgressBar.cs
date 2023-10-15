using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image filler;
    private float total;
    private float current;

    public void Init(float total)
    {
        this.total = total;
    }

    public void UpBar()
    {
        current++;
        filler.fillAmount = current / total;
    }
}
