using TMPro;
using UnityEngine;

public class ToggleSprite : MonoBehaviour
{
    public TMP_Text UserStatusText;

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
            UserStatusText.text = status.ToString();
        }
        else if (status == GameManager.State.OrdinaryUser)
        {
            spriteRenderer.color = Color.green;
            UserStatusText.text = status.ToString();
        }
        else if(status == GameManager.State.SuperUser)
        {
            spriteRenderer.color = Color.blue;
            UserStatusText.text = status.ToString();
        }
    }
}
