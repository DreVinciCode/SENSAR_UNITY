using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSprite : MonoBehaviour
{
    public Sprite Locked;
    public Sprite UnLocked;
    public Image ConnectRing;


    private float _duration = 3f;
    private float _currentTime;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SwapSprite(Sprite spriteImage)
    {
        spriteRenderer.sprite = spriteImage;
    }

    public void Unlocked()
    {
        spriteRenderer.sprite = UnLocked;
        Debug.Log("Switched sprite unlock");
    }

    public void OnButtonPressed()
    {
        StartCoroutine(Timer(_duration));

        _currentTime += Time.deltaTime;
    }

    public IEnumerator Timer(float duration)
    {
        var startTime = Time.time;
        var value = 0f;

        while (Time.time - startTime < duration)
        {
            value = _currentTime / duration;
            ConnectRing.fillAmount = value;
            yield return null;
        }
    }

}
