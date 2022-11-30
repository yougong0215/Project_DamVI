using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Reset : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SaveReset();
        }
    }
    public void SaveReset()
    {
        List<string> paths = new List<string>();
        paths.Add(Application.dataPath + "/Graphic.json");
        paths.Add(Application.dataPath + "/Audio.json");
        paths.Add(Application.dataPath + "/Shop.json");


        for (int i = 0; i < paths.Count; i++)
        {
            File.Delete(paths[i]);
        }



    }
}
