using UnityEngine;
using UnityEngine.UI;

public class TextScore : MonoBehaviour
{
    [SerializeField] Text scoreNow;
    [SerializeField] Text bestScore;
    [SerializeField] GameObject restartMenu;
    public int score;
    int bScore;
    private void Start()
    {
        score = 0;
        bScore = PlayerPrefs.GetInt("BestScore");
    }
    void Update()
    {
        if (score > bScore)
            bScore = score;
        scoreNow.text = "Your Score:" + $"\n{score}";
        bestScore.text = "Best Score:" + $"\n{bScore}";
    }
    public void ChangeScore(int cScore)
    {
        score += cScore;
    }
    public void GameEnd()
    {
        PlayerPrefs.SetInt("BestScore", bScore);
        PlayerPrefs.Save();
        restartMenu.SetActive(true);
    }
}
