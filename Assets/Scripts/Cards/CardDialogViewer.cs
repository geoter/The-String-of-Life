using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using static DialogueObject;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DialogueController))]
public class CardDialogViewer : MonoBehaviour
{
    
    public string twineFileNameWithExtension;
    public Text textView;

    public DialogueController controller;


    private void Start()
    {
        LoadDialog();
        DontDestroyOnLoad(this.gameObject);
    }

    private void LoadDialog()
    {
        string twineTxtRaw = Resources.Load<TextAsset>(twineFileNameWithExtension).text;
        controller = GetComponent<DialogueController>();
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue(twineTxtRaw);
    }

    public void OnNodeSelected(int indexChosen)
    {
        if (controller.GetCurrentNode().tags.Contains("END"))
        {
            SceneManager.MoveGameObjectToScene(this.gameObject, SceneManager.GetActiveScene());
            SceneManager.LoadScene(2);
        }
    }

    public void OnNodeEntered(Node newNode)
    {
        textView.text = newNode.text;
        for (int index = newNode.responses.Count - 1; index >= 0; index--)
        {
            Response currentResponse = newNode.responses[index];
            if (index == 1 || newNode.responses.Count == 1) //last in array is first in txt file
            {

                int selectedIndexCopy = index;
                OnNodeSelected(selectedIndexCopy);
            }
            else
            {
                int selectedIndexCopy = index;
                OnNodeSelected(selectedIndexCopy);
            }
        }
    }
}
