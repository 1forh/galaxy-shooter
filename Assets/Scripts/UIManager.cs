using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // handle to text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage; 
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private bool _gameIsOver = false;


    // Start is called before the first frame update
    void Start()
    {
        // assign text component to the handle
        _scoreText.text = "Score: " + 0;
    }

    void Update()
    {
        if (_gameIsOver == true)
        {
            bool _pressingResetKey = Input.GetKeyDown(KeyCode.R);

            if (_pressingResetKey)
            {
                ResetGame();
            }
        }
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];

        if (currentLives == 0)
        {
            _gameIsOver = true;
            StartCoroutine(ToggleGameoverText(currentLives));
            _restartText.gameObject.SetActive(true);
        }
    }

    IEnumerator ToggleGameoverText(int currentLives)
    {
        while (currentLives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
