using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public TMP_Text UserstatusText;

    public enum State 
    {
        SuperUser,
        OrdinaryUser,
        Locked,
    }

    public static State Userstatus;

    private void Awake()
    {
        Userstatus = State.OrdinaryUser;
    }

    private void Start()
    {
        SetSpriteColorAndText();
    }

    public void SuperUserStatus()
    {
        Userstatus = State.SuperUser;
        SetSpriteColorAndText();
    }

    public void OrdinaryUserStatus()
    {
        Userstatus = State.OrdinaryUser;
        SetSpriteColorAndText();
    }

    public void LockedStatus()
    {
        Userstatus = State.Locked;
        SetSpriteColorAndText();
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void SetSpriteColorAndText()
    {
        if (Userstatus == GameManager.State.Locked)
        {
            spriteRenderer.color = Color.red;
            UserstatusText.text = Userstatus.ToString();
        }
        else if (Userstatus == GameManager.State.OrdinaryUser)
        {
            spriteRenderer.color = Color.green;
            UserstatusText.text = Userstatus.ToString();
        }
        else if (Userstatus == GameManager.State.SuperUser)
        {
            spriteRenderer.color = Color.blue;
            UserstatusText.text = Userstatus.ToString();
        }
    }
}
