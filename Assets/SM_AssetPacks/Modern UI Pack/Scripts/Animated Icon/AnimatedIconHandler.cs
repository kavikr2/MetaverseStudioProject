using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Michsky.UI.ModernUIPack
{
    public class AnimatedIconHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Settings")]
        public PlayType playType;
        public Animator iconAnimator;
        public Button eventButton;

        bool isClicked;

        public enum PlayType
        {
            CLICK,
            ON_POINTER_ENTER,
            MadhanMade
        }

        void Start()
        {
            if (iconAnimator == null)
                iconAnimator = gameObject.GetComponent<Animator>();

            if (playType == PlayType.CLICK)
            {
                eventButton = gameObject.GetComponent<Button>();
                eventButton.onClick.AddListener(ClickEvent);
            }
            if(playType == PlayType.MadhanMade) {
                eventButton = gameObject.GetComponent<Button>();
                eventButton.onClick.AddListener(MadhanAnimate);
            }
        }

        public void ClickEvent()
        {
            if (isClicked == true)
            {
                iconAnimator.Play("Out");
                isClicked = false;
            }

            else
            {
                iconAnimator.Play("In");
                isClicked = true;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (playType == PlayType.ON_POINTER_ENTER)
                iconAnimator.Play("In");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (playType == PlayType.ON_POINTER_ENTER)
                iconAnimator.Play("Out");
        }

        public void MadhanAnimate()
        {
            StartCoroutine(HopefullyWork());
        }

        IEnumerator HopefullyWork()
        {
            iconAnimator.Play("In");
            eventButton.interactable = false;
            yield return new WaitForSeconds(1);
            iconAnimator.Play("Out");
            eventButton.interactable = true;
        }
    }
}