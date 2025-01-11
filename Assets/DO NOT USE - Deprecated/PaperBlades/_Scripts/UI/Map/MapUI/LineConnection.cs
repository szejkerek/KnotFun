using UnityEngine;
using UnityEngine.UI.Extensions;


[System.Serializable]
public class LineConnection
{
    public LineRenderer lr;
    public UILineRenderer uilr;
    public MapNodeUI from;
    public MapNodeUI to;

    public LineConnection(LineRenderer lr, UILineRenderer uilr, MapNodeUI from, MapNodeUI to)
    {
        this.lr = lr;
        this.uilr = uilr;
        this.from = from;
        this.to = to;
    }

    public void SetColor(Color color)
    {
        if (lr != null)
        {
            var gradient = lr.colorGradient;
            var colorKeys = gradient.colorKeys;
            for (var j = 0; j < colorKeys.Length; j++)
            {
                colorKeys[j].color = color;
            }

            gradient.colorKeys = colorKeys;
            lr.colorGradient = gradient;
        }

        if (uilr != null) uilr.color = color;
    }
}
