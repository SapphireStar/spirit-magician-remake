using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Framework;

namespace ElfWizard {
    public class SwitchSidePanel :BasePanel
    {
        public override void OnEnter()
        {
            base.OnEnter();
            gameObject.SetActive(true);
            //SetText();
            transform.localPosition = new Vector3(801.69f, 0, 0);
            Tweener tweener = transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f, false);
            tweener.OnComplete(() => transform.DOLocalMove(transform.localPosition, 0.5f, false).
                    OnComplete(() => ElfWizardArch.Instance.GetSystem<IUISystem>().PopPanel()));

        }

        public override void OnExit()
        {
            base.OnExit();
            transform.DOLocalMove(new Vector3(-801.69f, 0, 0), 0.5f, false);

        }

        public void SetText(string txt)
        {
            Text text = GetComponentInChildren<Text>();
            text.text = txt;

        }
    }

}