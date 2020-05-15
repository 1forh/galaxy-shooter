using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // handle to text
    [SerializeField]
    private Text _scoreText;


    // Start is called before the first frame update
    void Start()
    {
        // assign text component to the handle
        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }
}
