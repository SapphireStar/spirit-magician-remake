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
         StreamingAssets:Unity����Ŀ¼����Ŀ¼�е���Դ���ᱻѹ�����ʺ����ƶ��˶�ȡ��Դ����PC��д�룩
        Application.persistentDataPath����������ʱ���ж�д����,unity�ⲿĿ¼����װ����ʱ�Ų�����
        */

        [MenuItem("Tools/Resource/Generate ResConfig File")]//�ڱ༭�������������м���һ��ѡ�����ڵ��ô˷���
        public static void Generate()
        {
            //������Դ�����ļ�
            //����Resources������Ԥ����·��
            File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", new string[] { });
            string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" });
            for (int i = 0; i < resFiles.Length; i++)
            {
                resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
                //���ɶ�Ӧ��ϵ 
                string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
                string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
                resFiles[i] = fileName + "=" + filePath;//��Դӳ���ĸ�ʽ
            }
            AppendLines("Sound", ".wav");
            //д���ļ�
            File.AppendAllLines("Assets/StreamingAssets/ConfigMap.txt", resFiles);//�Ὣ������ÿһ��Ԫ�ض�ռһ��д���ļ���
            AssetDatabase.Refresh();//ˢ��project����
        }
        public static void AppendLines(string label,string extension)
        {
            string[] Files = AssetDatabase.FindAssets("l:"+ label, new string[] { "Assets/Resources" });
            for (int i = 0; i < Files.Length; i++)
            {
                Files[i] = AssetDatabase.GUIDToAssetPath(Files[i]);
                //���ɶ�Ӧ��ϵ 
                string fileName = Path.GetFileNameWithoutExtension(Files[i]);
                string filePath = Files[i].Replace("Assets/Resources/", string.Empty).Replace(extension, string.Empty);
                Files[i] = fileName + "=" + filePath;//��Դӳ���ĸ�ʽ
            }
            File.AppendAllLines("Assets/StreamingAssets/ConfigMap.txt", Files);
        }
    }
}