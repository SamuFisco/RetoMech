using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetButton : MonoBehaviour
{
    [SerializeField]
    EventSystem eventSystem;
    [SerializeField]
    public GameObject firstButton;
    // Start is called before the first frame update
    void OnEnable()
    {
        SetFirstButton();
    }

    public void SetFirstButton()
    {
        if (firstButton != null && eventSystem != null)
        {
            eventSystem.firstSelectedGameObject = firstButton;
            firstButton.GetComponent<Button>().Select();
        }
    }
}