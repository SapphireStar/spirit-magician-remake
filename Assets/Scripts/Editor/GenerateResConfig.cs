using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace ElfWizard
{
    public class GenerateResConfig : Editor
    {
        /*
         StreamingAssets:Unity特殊目录，该目录中的资源不会被压缩，适合在移动端读取资源（在PC端写入）
        Application.persistentDataPath可以在运行时进行读写操作,unity外部目录（安装程序时才产生）
        */

        [MenuItem("Tools/Resource/Generate ResConfig File")]//在编辑器顶部工具栏中加入一个选项用于调用此方法
        public static void Generate()
        {
            //生成资源配置文件
            //查找Resources下所有预制体路径
            File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", new string[] { });
            string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" });
            for (int i = 0; i < resFiles.Length; i++)
            {
                resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
                //生成对应关系 
                string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
                string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
                resFiles[i] = fileName + "=" + filePath;//资源映射表的格式
            }
            AppendLines("Sound", ".wav");
            //写入文件
            File.AppendAllLines("Assets/StreamingAssets/ConfigMap.txt", resFiles);//会将数组中每一个元素独占一行写入文件中
            AssetDatabase.Refresh();//刷新project界面
        }
        public static void AppendLines(string label,string extension)
        {
            string[] Files = AssetDatabase.FindAssets("l:"+ label, new string[] { "Assets/Resources" });
            for (int i = 0; i < Files.Length; i++)
            {
                Files[i] = AssetDatabase.GUIDToAssetPath(Files[i]);
                //生成对应关系 
                string fileName = Path.GetFileNameWithoutExtension(Files[i]);
                string filePath = Files[i].Replace("Assets/Resources/", string.Empty).Replace(extension, string.Empty);
                Files[i] = fileName + "=" + filePath;//资源映射表的格式
            }
            File.AppendAllLines("Assets/StreamingAssets/ConfigMap.txt", Files);
        }
    }
}