using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementManager : MonoBehaviour
{
    public TextMeshProUGUI seed;
    public TextMeshProUGUI drawed;

    public void Set(string newSeed)
    {
        seed.text = newSeed;
        drawed.text = RandomManager.Instance.getValue(seed.text).ToString();
    }

    public void Draw()
    {
         drawed.text = RandomManager.Instance.NextKey(seed.text).ToString();
    }
}
