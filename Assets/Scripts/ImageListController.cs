using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ImageListController : MonoBehaviour
{
    private string pathToDirectory;
    private readonly List<ListObjectData> listObjectDatas = new List<ListObjectData>();
    public RectTransform imageListContent;
    public ListObject listObjectPrefab;

    public void Start()
    {
        FirstInit();
        UpdateImageList();
    }

    public void UpdateImageList()
    {
        LoadAllPNGFilesFromDirectory(pathToDirectory);
        FillImageList();
    }

    //Creates directory with one test image
    private void FirstInit()
    {
        pathToDirectory = Application.persistentDataPath + "/PNG";
        if (!Directory.Exists(pathToDirectory))
        {
            Directory.CreateDirectory(pathToDirectory);
            var testImage = Resources.Load<Texture2D>("testImage");
            File.WriteAllBytes(pathToDirectory + "/testImage.png", testImage.EncodeToPNG());
        }
    }

    private void LoadAllPNGFilesFromDirectory(string _pathToDirectory)
    {
        if (!Directory.Exists(pathToDirectory))
        {
            Directory.CreateDirectory(pathToDirectory);
        }
        listObjectDatas.Clear();
        DirectoryInfo dir = new DirectoryInfo(_pathToDirectory);
        FileInfo[] info = dir.GetFiles("*.png");
        info = new List<FileInfo>(info).OrderBy(x => x.CreationTime).ToArray();
        foreach (FileInfo file in info)
        {
            ListObjectData data = new ListObjectData();
            data.fileName = file.Name.Split(".")[0];
            data.creationTime = file.CreationTime;
            data.sprite = LoadSprite(file);
            listObjectDatas.Add(data);
        }
    }

    private void FillImageList()
    {
        ListObject[] listObjects = imageListContent.GetComponentsInChildren<ListObject>();
        if (listObjectDatas.Count <= listObjects.Length)
        {
            for (int i = 0; i < listObjects.Length; i++)
            {
                if (i < listObjectDatas.Count)
                {
                    listObjects[i].gameObject.SetActive(true);
                    listObjects[i].FillImageObject(listObjectDatas[i]);
                }
                else
                {
                    listObjects[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < listObjectDatas.Count; i++)
            {
                if (i < listObjects.Length)
                {
                    listObjects[i].gameObject.SetActive(true);
                    listObjects[i].FillImageObject(listObjectDatas[i]);
                }
                else
                {
                    ListObject imageObject = Instantiate(listObjectPrefab, imageListContent);
                    imageObject.FillImageObject(listObjectDatas[i]);
                }
            }
        }
    }

    private Sprite LoadSprite(FileInfo _file)
    {
        var rawData = System.IO.File.ReadAllBytes(_file.ToString());
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(rawData);
        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

}
