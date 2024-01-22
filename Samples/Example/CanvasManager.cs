using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject RandomElement;
    public Transform elementContainer;
    public TMP_InputField inputSeed;
    public Button addButton;

    // Start is called before the first frame update
    void Start()
    {
        while (elementContainer.childCount > 0)
        {
            DestroyImmediate(elementContainer.GetChild(0).gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        addButton.interactable = RandomManager.isKeyAvaible(inputSeed.text);
    }

    public void newSeededElement()
    {
        GameObject newElement = Instantiate(RandomElement, elementContainer);

        RandomManager.AddKey(inputSeed.text);
        newElement.GetComponent<ElementManager>().reset(inputSeed.text);
    }
}
