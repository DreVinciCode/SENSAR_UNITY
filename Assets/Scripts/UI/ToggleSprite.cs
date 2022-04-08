using UnityEngine;

public class ToggleSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSpriteColor();
    }

    public void SwapSprite(Sprite spriteImage)
    {
        spriteRenderer.sprite = spriteImage;
    }

    public void deactivateSprite()
    {
        spriteRenderer.enabled = false;
    }

    public void activateSprite()
    {
        spriteRenderer.enabled = true;
    }

    public void SetSpriteColor()
    {
        var status = GameManager.Userstatus;
        if (status == GameManager.State.Locked)
        {
            spriteRenderer.color = Color.red;
        }
        else if (status == GameManager.State.OrdinaryUser)
        {
            spriteRenderer.color = Color.green;
        }
        else if(status == GameManager.State.SuperUser)
        {
            spriteRenderer.color = Color.black;
        }
    }
}
