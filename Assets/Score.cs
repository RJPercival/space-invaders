using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int Value
    {
        get { return value; }
        set
        {
            this.value = value;
            textElement.text = value.ToString();

        }
    }

    private int value = 0;
    private TMP_Text textElement;

    private void Start()
    {
        textElement = GetComponent<TMP_Text>();
    }
}
