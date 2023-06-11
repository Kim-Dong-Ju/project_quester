using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.InteropServices;

public enum Size
{
    POT64,
    POT128,
    POT256,
    POT512,
    POT1024,
}

public class Capture : MonoBehaviour
{
    public Camera cam;
    public RenderTexture rt;
    public Image bg;
    public Size size;

    public GameObject[] obj;
    int nowCnt = 0;

    private void Start()
    {
        cam = Camera.main;
        SettingSize();
    }

    public void Create()
    {
        StartCoroutine(captureImage());
    }

    public void AllCreate()
    {
        StartCoroutine(AllCaptureImage());
    }

    IEnumerator captureImage()
    {
        yield return null;

        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

        yield return null;

        var data = tex.EncodeToPNG();
        string name = "Thumbnail";
        string extension = ".png";
        string path = Application.persistentDataPath + "/Thumbnail/";

        Debug.Log(path);

        if(!Directory.Exists(path)) Directory.CreateDirectory(path);

        File.WriteAllBytes(path + name + extension, data);

        yield return null;
    }

    IEnumerator AllCaptureImage()
    {
        while(nowCnt < obj.Length)
        {
            var nowObj = Instantiate(obj[nowCnt].gameObject);

            Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false, true);
            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);

            yield return null;

            var data = tex.EncodeToPNG();
            string name = $"Thumbnail_{obj[nowCnt].gameObject.name}";
            string extension = ".png";
            string path = Application.persistentDataPath + "/Thumbnail/";

            Debug.Log(path);

            if(!Directory.Exists(path)) Directory.CreateDirectory(path);

            File.WriteAllBytes(path + name + extension, data);

            yield return null;

            DestroyImmediate(nowObj);
            nowCnt++;

            yield return null;
        }
    }

    void SettingSize()
    {
        switch(size)
        {
            case Size.POT64:
                rt.width = 64;
                rt.height = 64;
                break;
            case Size.POT128:
                rt.width = 128;
                rt.height = 128;
                break;
            case Size.POT256:
                rt.width = 256;
                rt.height = 256;
                break;
            case Size.POT512:
                rt.width = 512;
                rt.height = 512;
                break;
            case Size.POT1024:
                rt.width = 1024;
                rt.height = 1024;
                break;
            default:
                break;
        }
    }
}
