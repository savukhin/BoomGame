using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    public GameObject handler;
    [SerializeField]
    private float m_max = 10;
    [SerializeField]
    private float m_min = 0;
    [SerializeField]
    private float m_current = 10;

    public float max {
        get {
            return m_max;
        }

        set {
            m_max = value;
            Refresh();
        }
    }

    public float min {
        get {
            return m_max;
        }

        set {
            m_min = value;
            Refresh();
        }
    }

    public float current {
        get {
            return m_current;
        }

        set {
            m_current = value;
            Refresh();
        }
    }

    private float width;

    void Refresh() {
        handler.transform.localScale = new Vector3(m_current / m_max, 1f, 1f);
        handler.transform.localPosition = new Vector3(-width / 2f + m_current / m_max / 2f * width, 0f, 0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width * GetComponent<RectTransform>().localScale.x;
        handler.GetComponent<RectTransform>().sizeDelta = new Vector2(
                                handler.GetComponent<RectTransform>().rect.width,
                                handler.GetComponent<RectTransform>().rect.height);
        Refresh();
    }
}
