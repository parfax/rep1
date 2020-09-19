using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickBooster : MonoBehaviour,IClickable
{
    public Text text;
    public int cost;
    public int bonus;
    public int costMult;

    public int numberOfClick; 
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
        text = GetComponentInChildren<Text>();
        text.text = "Bonus " + bonus + "\nPrice " + cost;

    }
    public void Initialization(int numberOfClicks)
    {
        text = GetComponentInChildren<Text>();
        this.numberOfClick = numberOfClicks;
        cost *=(int) Mathf.Pow(costMult, numberOfClicks);
        FindObjectOfType<GameManager>().increment += numberOfClicks * bonus;
        text.text = "Bonus " + bonus + "\nPrice " + cost;
    }
    public void Click()
    {
        if (FindObjectOfType<GameManager>()._score >= cost)
        {
            FindObjectOfType<GameManager>().AddScore(-cost);
            FindObjectOfType<GameManager>().increment += bonus;
            Text fadedText = Instantiate(FindObjectOfType<GameManager>().prefabText, transform.parent.parent);
            fadedText.transform.localPosition = transform.localPosition;
            fadedText.text = "+" + bonus;
            StartCoroutine(Fade(fadedText));
            cost *= costMult;
            text.text = "Bonus " + bonus + "\nPrice " + cost;
            numberOfClick++;
        }
    }

    IEnumerator Fade(Text fadeText)
    {
        Color temp = fadeText.color;
        for (int i = 10; i > 0; i--)
        {
            temp.a = i * 0.1f;
            fadeText.transform.localPosition+= Vector3.up*5;
            fadeText.color = temp;
          yield return new WaitForSeconds(.1f);
        }
        Destroy(fadeText.gameObject);
    }
}
