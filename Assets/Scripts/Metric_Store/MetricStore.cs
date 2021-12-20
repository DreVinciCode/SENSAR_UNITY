using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricStore : MonoBehaviour
{
    static float startTime;
    public List<string> timestamps;
    public bool activated;
    public string metricName;
    // Start is called before the first frame update
    private void Awake()
    {
        startTime = Time.time;
        activated = false;
        metricName = gameObject.name;
        timestamps = new List<string>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void toggleStore()
    {
        activated = !activated;
        float currTime = Time.time - startTime;
        string isActivated() => activated ? "t" : "f";
        timestamps.Add(isActivated() + currTime.ToString("n2"));
    }

}