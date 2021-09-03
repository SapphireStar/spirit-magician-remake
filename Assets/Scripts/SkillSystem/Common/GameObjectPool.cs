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

        //key 所需类别对象的类别
        //prefab 所需类别对象的预制体
        //pos 位置
        //rotate 旋转
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
                    go = Cache[key].Find(g => !g.activeInHierarchy);//若对象池中包含该对象的key，则在Hierarchy窗口
                                                                    //中寻找是否有被隐藏的该类型对象(曾经被创建过)

                if (go == null)
                {
                    go = AddObject(key, prefab);//如果未在Hierarchy中存在该类型被使用过的对象，表明该对象未被创建过，
                                                //或者该类型的对象被用完，则需要调用AddObject创建一个新对象
                }   

                UseObject(pos, rotate, go); //若该对象被使用过，及，在字典中存在该对象，且Hierarchy中
                                            //包含被隐藏的此对象，则调用使用对象方法
                return go;
            }
            catch (System.Exception e)
            {
                Debug.Log("调用对象池中对象出现错误:" + e);
                return null;
            }

        }

        private GameObject AddObject(string key, GameObject prefab)
        {
            GameObject go = Instantiate(prefab);//这里直接生成了一个游戏对象，因为该方法被调用时
                                                //默认场景内没有可以被使用的该类型对象
            if (!Cache.ContainsKey(key))//若字典中不包含该类型游戏对象，则将其加入字典中
                Cache.Add(key, new List<GameObject>());
            Cache[key].Add(go);
            return go;//返回该游戏对象引用
        }
        private static void UseObject(Vector3 pos, Quaternion rotate, GameObject go)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;//根据参数设置所需对象的位置信息
            go.SetActive(true);
            for (int i = 0; i < go.transform.childCount; i++)
            {
                go.transform.GetChild(i).gameObject.SetActive(true);

            } 
            /*        go.GetComponent<IResetable>().OnReset();*/

            foreach (var item in go.GetComponents<IResetable>())//一个物体可能有多个脚本需要重置,因此遍历所有需要重置的脚本
            {
                item.OnReset();//重置需要使用的对象
            }
        }

        public void CollectObject(GameObject go, float delay = 0)//默认参数，如果不传入参数，则默认为0
        {
            StartCoroutine(CollectObjectDelay(go, delay));
        }
        private IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);//设定回收对象的延迟时间
            go.SetActive(false);//直接将其隐藏
        }

        public void Clear(string key)//清空某一类对象池
        {
            while (Cache[key].Count > 0)//当某一类型对象还存在时
            {
                Destroy(Cache[key][0]);//摧毁对象
                Cache[key].RemoveAt(0);//将对象从字典的键所对应的集合中移除
            }
            Cache.Remove(key);//将键从字典中移除

        }
        public void ClearAll()//清空所有对象池
        {

            foreach (var item in new List<string>(Cache.Keys))//删除字典记录
            {
                Clear(item);
            }

        }
    }
}