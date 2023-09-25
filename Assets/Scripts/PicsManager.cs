using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PicsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject gameField;

    [SerializeField]
    private Sprite[] sprites;

    private Pic[] images;

    public float FillField()
    {
        images = gameField.GetComponentsInChildren<Pic>();

        if (images.Length % 2 == 0 && sprites.Length != images.Length / 2)
        {
            Debug.LogError(
                $"Images count odd or not enought Sprites for this images count {sprites.Length}/{images.Length}"
            );
            return 0;
        }
        sprites.Shuffle();
        int s = 0;
        for (int i = 0; i < images.Length; i++)
        {
            if (s >= sprites.Length)
            {
                s = 0;
            }
            ref Image image = ref images[i].image;
            image.sprite = sprites[s];
            image.SetNativeSize();
            s++;
        }

        return images.Length / 2;
    }
}
