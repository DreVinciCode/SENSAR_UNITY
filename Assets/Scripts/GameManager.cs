using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State 
    {
        SuperUser,
        OrdinaryUser,
        Locked,
    }

    public static State Userstatus;

    private void Awake()
    {
        Userstatus = State.SuperUser;
    }

    public void SuperUserStatus()
    {
        Userstatus = State.OrdinaryUser;
    }

    public void OrdinaryUserStatus()
    {
        Userstatus = State.OrdinaryUser;
    }

    public void LockedStatus()
    {
        Userstatus = State.Locked;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
