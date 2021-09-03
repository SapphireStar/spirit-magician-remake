using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard
{

    public class GameObjectPool : MonoBehaviour
    {
        public static GameObjectPool _instance;
        public static GameObjectPool Instance { get { return _instance; } }
        private Dictionary<string, List<GameObject>> Cache;

        //key ��������������
        //prefab �����������Ԥ����
        //pos λ��
        //rotate ��ת
        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (_instance == null)
            {
                _instance = this;

            }
            else Destroy(gameObject);
            Cache = new Dictionary<string, List<GameObject>>();
        }

        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rotate)
        {
            GameObject go = null;
            try
            {
                if (Cache.ContainsKey(key))
                    go = Cache[key].Find(g => !g.activeInHierarchy);//��������а����ö����key������Hierarchy����
                                                                    //��Ѱ���Ƿ��б����صĸ����Ͷ���(������������)

                if (go == null)
                {
                    go = AddObject(key, prefab);//���δ��Hierarchy�д��ڸ����ͱ�ʹ�ù��Ķ��󣬱����ö���δ����������
                                                //���߸����͵Ķ������꣬����Ҫ����AddObject����һ���¶���
                }   

                UseObject(pos, rotate, go); //���ö���ʹ�ù����������ֵ��д��ڸö�����Hierarchy��
                                            //���������صĴ˶��������ʹ�ö��󷽷�
                return go;
            }
            catch (System.Exception e)
            {
                Debug.Log("���ö�����ж�����ִ���:" + e);
                return null;
            }

        }

        private GameObject AddObject(string key, GameObject prefab)
        {
            GameObject go = Instantiate(prefab);//����ֱ��������һ����Ϸ������Ϊ�÷���������ʱ
                                                //Ĭ�ϳ�����û�п��Ա�ʹ�õĸ����Ͷ���
            if (!Cache.ContainsKey(key))//���ֵ��в�������������Ϸ������������ֵ���
                Cache.Add(key, new List<GameObject>());
            Cache[key].Add(go);
            return go;//���ظ���Ϸ��������
        }
        private static void UseObject(Vector3 pos, Quaternion rotate, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;//���ݲ���������������λ����Ϣ
            go.SetActive(true);
            for (int i = 0; i < go.transform.childCount; i++)
            {
                go.transform.GetChild(i).gameObject.SetActive(true);

            } 
            /*        go.GetComponent<IResetable>().OnReset();*/

            foreach (var item in go.GetComponents<IResetable>())//һ����������ж���ű���Ҫ����,��˱���������Ҫ���õĽű�
            {
                item.OnReset();//������Ҫʹ�õĶ���
            }
        }

        public void CollectObject(GameObject go, float delay = 0)//Ĭ�ϲ���������������������Ĭ��Ϊ0
        {
            StartCoroutine(CollectObjectDelay(go, delay));
        }
        private IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);//�趨���ն�����ӳ�ʱ��
            go.SetActive(false);//ֱ�ӽ�������
        }

        public void Clear(string key)//���ĳһ������
        {
            while (Cache[key].Count > 0)//��ĳһ���Ͷ��󻹴���ʱ
            {
                Destroy(Cache[key][0]);//�ݻٶ���
                Cache[key].RemoveAt(0);//��������ֵ�ļ�����Ӧ�ļ������Ƴ�
            }
            Cache.Remove(key);//�������ֵ����Ƴ�

        }
        public void ClearAll()//������ж����
        {

            foreach (var item in new List<string>(Cache.Keys))//ɾ���ֵ��¼
            {
                Clear(item);
            }

        }
    }
}