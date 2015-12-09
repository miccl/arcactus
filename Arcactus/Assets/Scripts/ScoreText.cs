using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour
{

    private Color textColor;
    private TextMesh tm;

    // Use this for initialization
    void Start()
    {
        tm = GetComponent<TextMesh>();
        textColor = transform.parent.GetComponent<MeshRenderer>().material.color;
        //textColor = tm.color;
    }

    // Update is called once per frame
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
