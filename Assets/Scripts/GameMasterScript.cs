using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMasterScript : MonoBehaviour
{
    public int coinsTotal = 0;
    public Animator coinUIAnimator;
    public Text coinText;
    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void addCoin()
    {
        coinsTotal++;
        coinText.text = coinsTotal.ToString();
        coinUIAnimator.Play("BounceState", 0, 0f);
    }
}