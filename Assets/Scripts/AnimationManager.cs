using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private float AnimTime;

    public void DelCorrectConnect(Pic pic, Vector3 pos)
    {
        Sequence newAnim = DOTween.Sequence();
        newAnim
            .AppendCallback(() =>
            {
                pic.SaveOldPos();
            })
            .Append(pic.transform.DOMove(pos, AnimTime))
            .AppendCallback(() =>
            {
                GameObject picObj = pic.gameObject;
                Vector2 oldpos = pic.oldPos;
                Destroy(pic.image);
                Destroy(picObj.GetComponent<Image>());
                Destroy(pic);
                picObj.AddComponent<Border>();
                picObj.transform.position = oldpos;
                newAnim.Kill();
            });
        newAnim.Restart();
    }
}
