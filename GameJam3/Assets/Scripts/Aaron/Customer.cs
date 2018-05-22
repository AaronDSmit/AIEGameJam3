using UnityEngine;

public class Customer : MonoBehaviour
{
    private SpriteRenderer wingsRenderer;
    private SpriteRenderer thrusterRenderer;
    private SpriteRenderer gemRenderer;


    private void Awake()
    {
        wingsRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        thrusterRenderer = transform.GetChild(2).GetComponent<SpriteRenderer>();
        gemRenderer = transform.GetChild(3).GetComponent<SpriteRenderer>();
    }

    public void SetThruster(Sprite sprite)
    {
        thrusterRenderer.sprite = sprite;
    }

    public void SetGem(Sprite sprite)
    {
        gemRenderer.sprite = sprite;
    }

    public void SetWings(Sprite sprite)
    {
        wingsRenderer.sprite = sprite;
    }
}