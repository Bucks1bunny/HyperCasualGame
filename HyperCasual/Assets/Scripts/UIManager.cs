using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject endGameUI;
    [SerializeField]
    private TextMeshProUGUI colorPercentage;
    [SerializeField]
    private Button nextLevelButton;

    private void Start()
    {
        gameManager.ColorMixed += OnColorMixed;
        endGameUI.SetActive(false);
    }

    private void OnColorMixed(float percentage)
    {
        colorPercentage.text = string.Format("{0:00}", percentage) + "%";
        endGameUI.SetActive(true);
        if (percentage > 84)
        {
            nextLevelButton.interactable = true;
        }
        else
        {
            nextLevelButton.interactable = false;
        }
    }

    public void OnNextPressed()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex == 2)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
    }

    public void OnRestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
