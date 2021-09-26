using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
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
            async = SceneManager.LoadSceneAsync(ElfWizardArch.Instance.GetModel<IBattleModel>().MapName);
            yield return async;
            //GameFacade.Instance.Init();
/*            Constants.action();
            Constants.action = () => { };*/
            Destroy(gameObject);
        }


    }
}