using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementManager : MonoBehaviour
{
    public TextMeshProUGUI seed;
    public TextMeshProUGUI drawed;

    public void reset(string newSeed)
    {
        seed.text = newSeed;
        this.Draw();
    }

    public void Draw()
    {
         drawed.text = RandomManager.NextKey(seed.text).ToString();
    }
}
