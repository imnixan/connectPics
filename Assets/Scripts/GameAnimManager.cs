using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameAnimManager : MonoBehaviour
{
    [SerializeField]
    private float AnimTime;

    [SerializeField]
    private RectTransform gameField,
        timeWindow,
        replayRope,
        replayBtn,
        menuRope,
        menuBtn,
        exitBtn;

    [SerializeField]
    private Vector2 gamefieldShowPos,
        gameFieldHidePos,
        timeWindowShow,
        timeWindowHide,
        replayRopeShow,
        replayRopeHide,
        replayButtonShow,
        replayButtonHide,
        menuRopeShow,
        menuRopeHide;

    private Sequence startScene,
        endScene,
        endGame;

    private string nextScene;

    public void Init()
    {
        startScene = DOTween
            .Sequence()
            .Append(gameField.DOAnchorPos(gamefieldShowPos, AnimTime))
            .Join(timeWindow.DOAnchorPos(timeWindowShow, AnimTime))
            .AppendCallback(() =>
            {
                GetComponent<GameManager>().StartGame();
            });

        endScene = DOTween
            .Sequence()
            .Append(timeWindow.DOAnchorPos(timeWindowHide, AnimTime))
            .Join(exitBtn.DOAnchorPosX(-110, AnimTime))
            .Join(gameField.DOAnchorPos(gameFieldHidePos, AnimTime))
            .Append(replayBtn.DOAnchorPos(replayButtonHide, AnimTime))
            .Join(replayRope.DOAnchorPos(replayRopeHide, AnimTime))
            .Append(menuBtn.DOAnchorPos(replayButtonHide, AnimTime))
            .Join(menuRope.DOAnchorPos(menuRopeHide, AnimTime))
            .AppendCallback(() =>
            {
                if (string.IsNullOrEmpty(nextScene))
                {
                    Utils.CloseApp();
                }
                else
                {
                    SceneManager.LoadScene(nextScene);
                }
            });

        endGame = DOTween
            .Sequence()
            .AppendCallback(() =>
            {
                timeWindow.anchorMax = gameField.anchorMax;
                timeWindow.anchorMin = gameField.anchorMin;
                timeWindow.pivot = gameField.pivot;
            })
            .Append(timeWindow.DOAnchorPos(new Vector2(0, 800), AnimTime))
            .Join(gameField.DOAnchorPos(gameFieldHidePos, AnimTime))
            .Join(exitBtn.DOAnchorPosX(-110, AnimTime))
            .Append(replayRope.DOAnchorPos(replayRopeShow, AnimTime))
            .Append(replayBtn.DOAnchorPos(replayButtonShow, AnimTime))
            .Append(menuRope.DOAnchorPos(menuRopeShow, AnimTime))
            .Append(menuBtn.DOAnchorPos(replayButtonShow, AnimTime));
    }

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

    public void ExitScene(string sceneName)
    {
        this.nextScene = sceneName;
        endScene.Restart();
    }

    public void EndGame()
    {
        endGame.Restart();
    }

    public void StartGame()
    {
        startScene.Restart();
    }
}
