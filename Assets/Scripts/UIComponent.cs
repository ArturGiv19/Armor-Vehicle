using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
    public GameObject GetUIComponent()
    {
        return gameObject;
    }
    public Button GetButtonComponent()
    {
        return gameObject.GetComponent<Button>();
    }
    public Image GetImageComponent()
    {
        return gameObject.GetComponent<Image>();
    }
    public Text GetTextComponent()
    {
        return gameObject.GetComponent<Text>();
    }
    public Slider GetSlider()
    {
        return gameObject.GetComponent<Slider>();
    }
    public void Off()
    {
        gameObject.SetActive(false);
    }
    public void On()
    {
        gameObject.SetActive(true);
    }
}