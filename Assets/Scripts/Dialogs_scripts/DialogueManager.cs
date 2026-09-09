using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public enum Mode { Intro, QA }

    [Header("UI References")]
    public Text nameText;
    public Text dialogueText;
    public Transform optionsContainer;
    public Button optionButtonPrefab;

    [Header("Intro Lines")]
    public DialogueLine[] introLines;  // первые две мысли

    [Header("QA Stage")]
    public DialogueQuestion[] questions; 

    int introIndex = 0;
    Mode mode;
    
    void Awake()
    {
        // при загрузке сцены мы прячем окно диалога
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Этот метод инициализирует диалог и показывает окно.
    /// Нужно **вызывать** его из вашего кода (или добавить автозапуск для теста).
    /// </summary>
    public void StartDialogue()
    {
        mode = Mode.Intro;
        introIndex = 0;
        gameObject.SetActive(true);
        ShowIntroLine();
    }


    void ShowIntroLine()
    {
        ClearOptions();
        var line = introLines[introIndex];
        nameText.text = line.speakerName;
        dialogueText.text = line.dialogueText;

        CreateContinueButton(() => {
            introIndex++;
            if (introIndex < introLines.Length) ShowIntroLine();
            else StartQA();
        });
    }

    void StartQA()
    {
        mode = Mode.QA;
        ShowQAOptions();
    }

    void ShowQAOptions()
    {
        ClearOptions();
        nameText.text = "M";         // или «Boy»
        dialogueText.text = "Masz pytania?";

        // собираем не заданные ещё
        var remaining = new List<DialogueQuestion>();
        foreach (var q in questions)
            if (!q.asked) remaining.Add(q);

        if (remaining.Count > 0)
        {
            // кнопки для каждого не заданного вопроса
            foreach (var q in remaining)
            {
                var btn = Instantiate(optionButtonPrefab, optionsContainer);
                btn.gameObject.SetActive(true);
                btn.GetComponentInChildren<Text>().text = q.questionText;
                btn.onClick.AddListener(() => AskQuestion(q));
            }
        }
        else
        {
            // все вопросы заданы — одна кнопка «Tak, zaczynamy!!»
            var btn = Instantiate(optionButtonPrefab, optionsContainer);
            btn.gameObject.SetActive(true);
            btn.GetComponentInChildren<Text>().text = "Tak, zaczynamy!!";
            btn.onClick.AddListener(EndDialogue);
        }
    }

    void AskQuestion(DialogueQuestion q)
    {
        q.asked = true;
        ClearOptions();
        nameText.text = "Gwidon";
        dialogueText.text = q.answerText;
        CreateContinueButton(ShowQAOptions);
    }

    void EndDialogue()
    {
        gameObject.SetActive(false);
        // тут запускайте сбор костей, переход в следующий гейм-стейт и т.п.
    }

    // хелперы
    void ClearOptions()
    {
        foreach (Transform c in optionsContainer) Destroy(c.gameObject);
    }

    void CreateContinueButton(UnityEngine.Events.UnityAction onClick)
    {
        var btn = Instantiate(optionButtonPrefab, optionsContainer);
        btn.gameObject.SetActive(true);
        btn.GetComponentInChildren<Text>().text = "Dalej";
        btn.onClick.AddListener(onClick);
    }
}
