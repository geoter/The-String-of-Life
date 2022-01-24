using UnityEngine;
using UnityEngine.UI;
using static DialogueObject;
using TMPro;

[RequireComponent(typeof(DialogueController))]
public class DialogueViewer : MonoBehaviour
{
    [SerializeField] private SoundsManager _soundsManager;
    [SerializeField] private string twineFileNameNoExtension;
    [SerializeField] private TMP_Text textView;
    [SerializeField] private Button firstButton;
    [SerializeField] private Button secondButton;
    [SerializeField] private TMP_Text zeusScoreLabel;
    [SerializeField] private string correctTag = "Correct";
    private int score = 0;
    private int totalQuestionsAnswered = 0;

    private TMP_Text firstButtonTitle;
    private TMP_Text secondButtonTitle;
    
    private DialogueController controller;

    private void Awake()
    {
        firstButtonTitle = firstButton.GetComponentInChildren<TMP_Text>();
        secondButtonTitle = secondButton.GetComponentInChildren<TMP_Text>();
        zeusScoreLabel.gameObject.SetActive(false);
    }

    private void Start() {
       LoadDialog();
    }
    
    private void LoadDialog()
    {
        TextAsset twineTxtRaw = Resources.Load<TextAsset>(twineFileNameNoExtension);
        controller = GetComponent<DialogueController>();
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue(twineTxtRaw.text);
    }

    private void OnNodeSelected( int indexChosen ) {
        controller.ChooseResponse( indexChosen );
    }

    private void OnNodeEntered( Node newNode ) {
        UpdateScore(newNode);
        textView.text = newNode.text;
     if (newNode.IsEndNode())
     {
         firstButton.gameObject.SetActive(false);
         secondButton.gameObject.SetActive(false);
         return;
     }
     if (newNode.responses.Count == 1)
     {
         secondButton.gameObject.SetActive(false);
     }
     for (int index = newNode.responses.Count-1; index >=0; index--)
     {
         Response currentResponse = newNode.responses[index];
         if (index == 1 || newNode.responses.Count == 1) //last in array is first in txt file
         {
             firstButton.gameObject.SetActive(true);
             firstButtonTitle.text = currentResponse.displayText;
             firstButton.onClick.RemoveAllListeners();
             int selectedIndexCopy = index;
             firstButton.onClick.AddListener(delegate
             {
                 _soundsManager.cardSwiped();
                 OnNodeSelected(selectedIndexCopy);
             });
         }
         else
         {
             secondButton.gameObject.SetActive(true);
             secondButtonTitle.text = currentResponse.displayText;
             secondButton.onClick.RemoveAllListeners();
             int selectedIndexCopy = index;
             secondButton.onClick.AddListener(delegate
             {
                 _soundsManager.cardSwiped();
                 OnNodeSelected(selectedIndexCopy);
             });
         }
     }
    }

    private void UpdateScore(Node node)
    {
        bool isDecisionNode = node.tags.Count == 2;
        if (isDecisionNode == false) { return;}
        
        totalQuestionsAnswered += 1;
        string decisionResult = node.tags[node.tags.Count - 1];
        if (decisionResult == correctTag) {
            score += 1;
        }
        zeusScoreLabel.gameObject.SetActive(true);
        zeusScoreLabel.text = score + "/" + totalQuestionsAnswered+" correct";
    }
}
