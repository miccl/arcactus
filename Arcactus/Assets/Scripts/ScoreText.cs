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

    void Start()
    {
        tm = GetComponent<TextMesh>();
        textColor = transform.parent.GetComponent<MeshRenderer>().material.color;
        //textColor = tm.color;
    }

    void Update()
    {
        // if you only use "transform.LookAt(Camera.main.transform.position)"  the textMesh will be displayed backwards
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        if (tm.text != "")
        {
            transform.Translate(0, 0.1f, 0, Space.World);
            Fade();
        }
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
}
