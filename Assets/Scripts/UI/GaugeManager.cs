using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Tamana
{
    public class GaugeManager : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Gauge _hp;
        [SerializeField] private Gauge _mp;
        [SerializeField] private Gauge _st;
        
        public Gauge HP { get { return _hp; } }
        public Gauge MP { get { return _mp; } }
        public Gauge ST { get { return _st; } }
    }

    [System. Serializable]
    public class Gauge
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image frame;
        [SerializeField] private Image fill;

        public Vector2 position
        {
            get { return rectTransform.localPosition; }
            set { rectTransform.localPosition = value; }
        }

        public Color frameColor
        {
            get { return frame.color; }
            set { frame.color = value; }
        }

        public Color fillColor
        {
            get { return fill.color; }
            set { fill.color = value; }
        }

        public float fillRate
        {
            get { return fill.fillAmount; }
            set { fill.fillAmount = value; }
        }
    }
}