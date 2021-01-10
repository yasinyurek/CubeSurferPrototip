using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreUI : MonoBehaviour
{
    public GameObject Score;
    public GameObject Lose;
    public Text point, coin;
    public Transform Playerpos, finishpos;
    public Scrollbar scrollbar;
    private void Update()
    {
       

      
        scrollbar.size = Playerpos.position.z / finishpos.position.z;


    }
    public void ShowScore(int p ,int c)
    {
        Score.SetActive(true);
        point.text = p.ToString()+"X";
        coin.text = c.ToString();
    }
    public void ShowLose()
    {
        Lose.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
