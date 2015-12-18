using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour
{
    /// <summary>
    /// The color of the text.
    /// </summary>
    private Color textColor;
    /// <summary>
    /// The text mesh.
    /// </summary>
    private TextMesh tm;


    private string text;

    void Start()
    {
        tm = GetComponent<TextMesh>();
        tm.text = "";
    }

    void Update()
    {
        // if you only use "transform.LookAt(Camera.main.transform.position)"  the textMesh will be displayed backwards
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        transform.Translate(0, 0.1f, 0, Space.World);
        tm.text = text;
        Fade();
    }

    /// <summary>
    /// Fades the score text.
    /// </summary>
    void Fade()
    {

        if (tm.color.a > 0)
        {
            textColor.a -= 0.1f * Time.deltaTime * 8;
            tm.color = textColor;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SetText(string text)
    {
        this.text = text;
    }

    public void SetTextColor(Color textColor)
    {
        this.textColor = textColor;
    }
}
