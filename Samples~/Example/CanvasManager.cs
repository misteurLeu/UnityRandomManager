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
    public Button saveButton;
    public Button loadButton;

    private List<string> saved = null;

    // Start is called before the first frame update
    void Start()
    {
        _clearItems();
        saved = new List<string>();
    }
    // Update is called once per frame
    void Update()
    {
        addButton.interactable = RandomManager.Instance.isKeyAvaible(inputSeed.text);
        loadButton.interactable = saved != null;
    }

    public void newSeededElement()
    {
        GameObject newElement = Instantiate(RandomElement, elementContainer);

        RandomManager.Instance.AddKey(inputSeed.text);
        newElement.GetComponent<ElementManager>().Set(inputSeed.text);
    }
    

    public void newSeededElement(string key)
    {
        GameObject newElement = Instantiate(RandomElement, elementContainer);

        newElement.GetComponent<ElementManager>().Set(key);
    }

    public void Save()
    {
        saved = RandomManager.Instance.Export();
    }

    public void Load()
    {
        _clearItems();
        RandomManager.Instance.Import(saved);
        foreach (string key in RandomManager.Instance.Items.Keys)
            newSeededElement(key);
    }

    private void _clearItems()
    {
        while (elementContainer.childCount > 0)
        {
            DestroyImmediate(elementContainer.GetChild(0).gameObject);
        }
    }
}
