using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElfWizard {
    public class Loading : MonoBehaviour
    {
        AsyncOperation async;
        void Start()
        {
            StartCoroutine(LoadScene());
            DontDestroyOnLoad(this);
        }
        IEnumerator LoadScene()
        {
            async = SceneManager.LoadSceneAsync(Constants.SceneToLoad);
            yield return async;
            GameFacade.Instance.Init();
            Destroy(gameObject);
        }

    }
}