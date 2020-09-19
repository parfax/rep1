using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int _score;
    public Text textScore;
    public int increment = 1;
    public Text prefabText;
    public IOSystem loader;
    public List<Button> buttons = new List<Button>();
    private void Start()
    {
        
        loader = GetComponent<IOSystem>();
        loader.Initialization(buttons);
        loader.Load();
        _score = PlayerPrefs.GetInt("Score", 0);
        textScore.text = _score.ToString();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            CloseGame();
    }
    public void Click()
    {
        AddScore(increment);
    }
    public void AddScore(int added)
    {
        _score += added;
        textScore.text = _score.ToString();
    }
    public void CloseGame()
    {
        loader.Save();
        Application.Quit();
    }

}
