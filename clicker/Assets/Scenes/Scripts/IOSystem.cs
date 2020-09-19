using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class IOSystem : MonoBehaviour
{
    public List<IClickable> buttons;
    public string fileName = "data";
    private string path;

    public void Load()
    {
        path = Application.dataPath + "/" + fileName + ".txt";
        if (!File.Exists(path)) return;
        if (File.ReadAllLines(path) == null) return;
        string[] temp = File.ReadAllLines(path);
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp.Length - 1 == i)
            {
                FindObjectOfType<GameManager>()._score = int.Parse(temp[i]);
                continue;

            }
            buttons[i].Initialization(int.Parse(temp[i]));
        }
    }
    public void Initialization(List<Button> receivedButtons)
    {
        IClickable[] temp = new IClickable[receivedButtons.Count];

        buttons = new List<IClickable>(receivedButtons.Capacity);
        for (int i = 0; i < receivedButtons.Count; i++)
        {
            temp[i] = receivedButtons[i].GetComponent<IClickable>();
        }

        buttons.AddRange(temp);
    }

    public void Save()
    {
        string[] temp = new string[buttons.Count + 1];
        for (int i = 0; i < temp.Length - 1; i++)
        {
            if (buttons[i] is ClickBooster)
            {
                // temp[i]= buttons[i].GetComponent<ClickBooster>().numberOfClick.ToString();
                temp[i] = ((ClickBooster)buttons[i]).numberOfClick.ToString();
            }
            else
            {
                temp[i] = ((TimeBooster)buttons[i]).numberOfClicks.ToString();
            }
        }

        temp[temp.Length - 1] = FindObjectOfType<GameManager>()._score.ToString();

        File.WriteAllLines(path, temp);
    }
}
