using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isPlay;
    private int score;
    public GameObject startingPlatform, zombies;
    public StartingMotion startingMotion;
    public Animator anim;
    public UIManager uiManager;
    public AudioSource audioSource;
    public AudioClip bgSound, zombieSound, gameOverSound;
    private void Start()
    {
        Time.timeScale = 1;
    }
    public void Play()
    {
        isPlay = true;
        audioSource.PlayOneShot(zombieSound);
        audioSource.PlayOneShot(bgSound);
        uiManager.GamePanel();
        anim.SetBool("start", true);
        startingMotion.enabled = true;
        for (int i = 0; i < zombies.transform.childCount; i++)
        {
            zombies.transform.GetChild(i).GetComponent<Animator>().SetBool("start", true);
        }
        Destroy(startingPlatform, 25f);
        Destroy(zombies, 3f);
        InvokeRepeating(nameof(Score), 0, 0.05f);
    }
    public void Restart()
    {
        SceneManager.LoadScene("game");
    }
    public void GameOver()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(gameOverSound);
        isPlay=false;
        CancelInvoke();
        StartCoroutine(uiManager.ScorePanel(score));
    }
    private void Score()
    {
        uiManager.ScoreText(++score);
    }
}
