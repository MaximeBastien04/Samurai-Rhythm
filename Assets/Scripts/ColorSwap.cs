using UnityEngine;

public class ColorSwap : MonoBehaviour
{
    public Color blue;
    public Color pink;
    public Color green;
    private GameObject[] blueObjects;
    private GameObject[] pinkObjects;
    private GameObject[] greenObjects;
    public float alpha = 0.5f;
    enum CurrentColor { Blue, Pink, Green };
    CurrentColor currentColor;

    void Start()
    {
        blueObjects = GameObject.FindGameObjectsWithTag("Blue");
        pinkObjects = GameObject.FindGameObjectsWithTag("Pink");
        greenObjects = GameObject.FindGameObjectsWithTag("Green");

        DisableColor(blueObjects);
        DisableColor(pinkObjects);
        DisableColor(greenObjects);
    }

    public void ChangeToBlue()
    {
        gameObject.GetComponent<SpriteRenderer>().color = blue;
        SetColor(blueObjects, CurrentColor.Blue);
    }

    public void ChangeToPink()
    {
        gameObject.GetComponent<SpriteRenderer>().color = pink;
        SetColor(pinkObjects, CurrentColor.Pink);
    }

    public void ChangeToGreen()
    {
        gameObject.GetComponent<SpriteRenderer>().color = green;
        SetColor(greenObjects, CurrentColor.Green);
    }

    private void SetColor(GameObject[] objects, CurrentColor newColor)
    {
        DisableColor(blueObjects);
        DisableColor(pinkObjects);
        DisableColor(greenObjects);

        foreach (GameObject obj in objects)
        {
            //Visual
            Color objColor = obj.GetComponent<SpriteRenderer>().color;
            objColor = new Color(objColor.r, objColor.g, objColor.b, 1f);
            obj.GetComponent<SpriteRenderer>().color = objColor;
            currentColor = newColor;

            //Collision
            obj.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void DisableColor(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            //Visual
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;

            //Collision
            obj.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
