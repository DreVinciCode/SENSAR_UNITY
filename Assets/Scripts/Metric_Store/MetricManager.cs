using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MetricManager : MonoBehaviour
{
    // Start is called before the first frame update
    const string FMT = ".csv";
    public int trialIter;
    public MetricStore[] metrics;
    string filePath;
    FileStream fs;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Latest Trial"))
        {
            PlayerPrefs.SetInt("Latest Trial", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Latest Trial", PlayerPrefs.GetInt("Latest Trial") + 1);
        }
        trialIter = PlayerPrefs.GetInt("Latest Trial");
        PlayerPrefs.Save();
        filePath = getPath("SENSAR_TRIAL_" + trialIter.ToString() + FMT);
        if(!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }
    }
    private void Start()
    {
        // Get all metrics
        metrics = Object.FindObjectsOfType<MetricStore>();
        // Opens a file for the new trial
        fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnApplicationQuit()
    {
        Debug.Log("App Quit");
        Save();
    }

    private string getPath(string localFile)
    {
#if UNITY_EDITOR
        return Application.persistentDataPath + "/CSV/" + localFile;
#elif UNITY_ANDROID
        return Application.persistentDataPath+localFile;
#else
        return Application.persistentDataPath+"/"+localFile;
#endif
    }

    private string csvify()
    {
        string toCSV = "";
        List<string> names = new List<string>();
        List<string> ranges = new List<string>();
        foreach (MetricStore metric in metrics)
        {
            names.Add(metric.name);
            ranges.Add(string.Join(",", metric.timestamps));
        }
        toCSV = string.Join(",", names) + "\n" + string.Join(",", ranges);
        return toCSV;
    }

    private void Save()
    {
        string csvd = csvify();
        Debug.Log(csvd);
        //fs.Write
    }

}