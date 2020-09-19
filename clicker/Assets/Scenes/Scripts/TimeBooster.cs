using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeBooster : MonoBehaviour,IClickable
{
    public Text text;

    public int cost;
    public int bonus;
    public int costMult;
    public int timeIncrement;

    public float time;
    public int numberOfClicks;
    public Image BarImage;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
        text = GetComponentInChildren<Text>();
        text.text = "Bonus" + bonus + "\nPrice:" + cost;
    }
    public void Initialization(int numberOfClicks)
    {
        text = GetComponent<Text>();
        this.numberOfClicks = numberOfClicks;
        cost *= (int)Mathf.Pow(costMult, numberOfClicks);
        timeIncrement += bonus * numberOfClicks;
        text.text = "Bonus" + bonus + "\nPrice:" + cost;
        if (numberOfClicks >= 1)
        {
            StartCoroutine(TimeIncrement());
        }

    }
   public void Click()
    {
        if (FindObjectOfType<GameManager>()._score >= cost)
        {
            FindObjectOfType<GameManager>().AddScore(-cost);
            timeIncrement += bonus;
            cost *= costMult;
            text.text = "Bonus" + bonus + "\nPrice:" + cost;
            numberOfClicks++;
            if (numberOfClicks == 1)
            {
                StartCoroutine(TimeIncrement());
            }
        }

    }

    

    IEnumerator TimeIncrement()
    {
        while (true)
        {
            for (int i = 0; i < time; i++)
            {
                if (Math.Abs(BarImage.fillAmount - 1) > 0.02f)
                {
                    BarImage.fillAmount +=(1/(Mathf.Pow(time,3))) ;
                }

                else
                {
                    FindObjectOfType<GameManager>().AddScore(timeIncrement);
                    BarImage.fillAmount = 0;
                }

                yield return new WaitForSeconds(1/Mathf.Pow(time,3));
            }
        }
    }
}
