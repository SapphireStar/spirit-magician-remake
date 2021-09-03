using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Text;

public class ResourceManager
{
    //作用：初始化类的静态数据成员
    //时机：类被加载时执行
    private static Dictionary<string, string> configMap;
    static ResourceManager()
    {
        string fileContent = GetConfigFile("ConfigMap.txt");
        BuildMap(fileContent);
        
    }

    public static string GetConfigFile(string fileName)
    {

       string url;
       #region 分平台判断StreamingAssets路径
#if UNITY_EDITOR||UNITY_STANDALONE//判断平台，并分配不同的资源读取路径
        url = "file://" + Application.dataPath + "/StreamingAssets/"+fileName;
#elif UNITY_IPHONE
       url = "file://" + Application.dataPath + "/Raw/"+fileName;
#elif UNITY_ANDROID
       url = "jar:file://" + Application.dataPath + "!/assets/"+fileName;
#endif
        #endregion
        #region obsolete
        /*        WWW www = new WWW(url);
                while (true)
                {
                    if (www.isDone)
                    {
                        return www.text;
                    }
                }*/
        #endregion
        UnityWebRequest request = UnityWebRequest.Get(url);
        AsyncOperation getFile = request.SendWebRequest();
        while (true)
        {
            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.Log(request.error);
                return null;
            }

            if (getFile.isDone)
            {
                return request.downloadHandler.text;
            }
        }
        
    }


    public static void getFile()
    {

#if UNITY_EDITOR
        UnityWebRequest request = UnityWebRequest.Get("file://" + Application.streamingAssetsPath + "/ConfigMap.txt");
#endif
        request.SendWebRequest();
        string s = request.downloadHandler.text;
        Debug.Log(request.error);
    }

    private static void BuildMap(string fileContent)
    {


        configMap = new Dictionary<string, string>();
        #region obsolete
        /*        string[] s = fileContent.Split('=', '\r', '\n');
                for (int i = 0; i < s.Length;i++)
                {
                    if (s[i] != "")
                    {
                        configMap.Add(s[i], s[i + 1]);
                        i++;
                    }
                }*/
        #endregion
        using (StringReader reader = new StringReader(fileContent))//在C#中，使用using调用流处理方法，方法执行后
                                                                   //会自动关闭流，不需要手动关闭
        {
            string line;
            while ((line=reader.ReadLine())!=null)
            {
                string[] keyValue = line.Split('=');
                configMap.Add(keyValue[0], keyValue[1]);
            }
        }//当程序退出using，将自动调用reader.Dispose;
       // reader.Dispose();

    }

    public static T Load<T>(string prefabName) where T:Object//泛型方法需要加此声明
    {
        //prefabName=>prefabPath
        string prefabPath = configMap[prefabName];
        return Resources.Load<T>(prefabPath);
    }
}
