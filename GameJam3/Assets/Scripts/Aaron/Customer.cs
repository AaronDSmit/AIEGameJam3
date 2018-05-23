using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private GameObject smokeEffect;

    private SpriteRenderer customerRenderer;
    private SpriteRenderer wingsRenderer;
    private SpriteRenderer thrusterRenderer;
    private SpriteRenderer gemRenderer;

    private void Awake()
    {
        customerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        wingsRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        thrusterRenderer = transform.GetChild(2).GetComponent<SpriteRenderer>();
        gemRenderer = transform.GetChild(3).GetComponent<SpriteRenderer>();
    }

    public void PlaySmokeEffect()
    {
        smokeEffect.SetActive(true);
    }

    public void StopSmokeEffect()
    {
        smokeEffect.SetActive(false);
    }

    public void SetCustomer(Sprite sprite)
    {
        customerRenderer.sprite = sprite;
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