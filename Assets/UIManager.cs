using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager instance;       
    [SerializeField] Sprite white;
    [SerializeField] Sprite black;
    [SerializeField] Image image;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }
    public static UIManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void setColor(int color)
    {
        if(color == 400)
        {
            image.sprite = black;

        }
        else
        {
            image.sprite = white;
        }

    }
}
