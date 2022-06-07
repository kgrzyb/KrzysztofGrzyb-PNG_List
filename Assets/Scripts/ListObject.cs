using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListObject : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI fileName;
    public TextMeshProUGUI timeSinceCreated;
    public ListObjectData data;

    public void FillImageObject(ListObjectData _data)
    {
        fileName.text = _data.fileName;
        icon.sprite = _data.sprite;
        icon.preserveAspect = true;
        data = _data;
        timeSinceCreated.text = CalculateTimeSinceCreated(_data);
    }

    public string CalculateTimeSinceCreated(ListObjectData _data)
    {
        double totalSeconds = (DateTime.Now - _data.creationTime).TotalSeconds;
        int totalMinutes = (int)(totalSeconds / 60);
        int totalHours = totalMinutes / 60;
        int totalDays = totalHours / 24;

        string days = totalDays.ToString();
        string hours = (totalHours - (totalDays * 24)).ToString();
        string minutes = (totalMinutes - (totalHours * 60)).ToString();
        string seconds = ((int)(totalSeconds - (totalMinutes * 60))).ToString();

        return string.Format("{0} days {1} hours {2} min {3} sec", days, hours, minutes, seconds);
    }


}
