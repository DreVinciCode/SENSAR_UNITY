using UnityEngine;

public class ToggleSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SwapSprite(Sprite spriteImage)
    {
        spriteRenderer.sprite = spriteImage;
    }

    public void deactivateSprite()
    {
        spriteRenderer.enabled = false;
    }

    public void unlockedSprite()
    {
        spriteRenderer.color = Color.white;
    }
}
