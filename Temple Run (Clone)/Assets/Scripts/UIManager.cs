using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel, gamePanel, scorePanel;
    public TextMeshProUGUI gameScoreText, scorePanelScoreText, bestScoreText;

    private void Start()
    {
        bestScoreText.text = PlayerPrefs.GetInt("bestScore", 0).ToString();
    }
    public void GamePanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    public IEnumerator ScorePanel(int score)
    {
        yield return new WaitForSeconds(2f);
        scorePanel.SetActive(true);
        gamePanel.SetActive(false);
        int angle = 0;
        DOTween.To(() => angle, x => angle = x, score, 2f)
            .OnUpdate(() => {
                scorePanelScoreText.text = angle.ToString();
            });
        if(score >= PlayerPrefs.GetInt("bestScore", 0))
        {
            PlayerPrefs.SetInt("bestScore", score);
        }
    }
    public void ScoreText(int score)
    {
        gameScoreText.text = score.ToString();
    }
   
}
