using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Покроковий майстер-вікторина. Машина станів: Question -> Feedback -> (наступне питання) ... -> Result.
public class WizardController : MonoBehaviour
{
    private enum State { Question, Feedback, Result }

    [System.Serializable]
    public class Step
    {
        public string question;
        public string[] answers = new string[3];
        public int correctIndex;
    }

    [SerializeField] private TMP_Text stepText;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private Button[] answerButtons;   // 3 кнопки відповідей
    [SerializeField] private Button nextButton;

    // Питання можна редагувати прямо в Inspector.
    [SerializeField]
    private Step[] steps =
    {
        new Step { question = "How do you open or close this tablet?",
                   answers = new[] { "Press M or Menu", "Clap your hands", "Look at the ceiling" }, correctIndex = 0 },
        new Step { question = "How do you move around the room?",
                   answers = new[] { "Walk through walls", "Teleport with the thumbstick", "Press Reset" }, correctIndex = 1 },
        new Step { question = "Where should you put a vinyl disc?",
                   answers = new[] { "On the TV", "On the pouf", "On the gramophone" }, correctIndex = 2 },
        new Step { question = "What appears when you point at the guitar?",
                   answers = new[] { "An info card", "A new room", "Nothing" }, correctIndex = 0 },
    };

    private State state;
    private int current;
    private int score;

    // Щоразу, коли майстер відкривається, починаємо спочатку.
    private void OnEnable()
    {
        Restart();
    }

    public void Restart()
    {
        current = 0;
        score = 0;
        ShowQuestion();
    }

    private void ShowQuestion()
    {
        state = State.Question;
        Step step = steps[current];

        stepText.text = "Step " + (current + 1) + " of " + steps.Length;
        questionText.text = step.question;
        feedbackText.text = "";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(true);
            answerButtons[i].interactable = true;
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = step.answers[i];
        }
        nextButton.gameObject.SetActive(false);
    }

    // Викликають кнопки відповідей з номером 0, 1 або 2.
    public void SelectAnswer(int index)
    {
        if (state != State.Question)
            return;

        state = State.Feedback;
        Step step = steps[current];

        if (index == step.correctIndex)
        {
            score++;
            feedbackText.text = "Correct!";
        }
        else
        {
            feedbackText.text = "Not quite. Right answer: " + step.answers[step.correctIndex];
        }

        foreach (Button button in answerButtons)
            button.interactable = false;

        nextButton.gameObject.SetActive(true);
        nextButton.GetComponentInChildren<TMP_Text>().text = current + 1 < steps.Length ? "Next" : "Finish";
    }

    public void Next()
    {
        if (state == State.Result)
        {
            Restart();
            return;
        }
        if (state != State.Feedback)
            return;

        current++;
        if (current < steps.Length)
            ShowQuestion();
        else
            ShowResult();
    }

    private void ShowResult()
    {
        state = State.Result;
        string playerName = PlayerPrefs.GetString("PlayerName", RoomSettings.DefaultName);

        stepText.text = "Result";
        questionText.text = playerName + ", your score: " + score + " of " + steps.Length;
        feedbackText.text = score == steps.Length ? "Perfect! You know this room." : "Press Restart to try again.";

        foreach (Button button in answerButtons)
            button.gameObject.SetActive(false);

        nextButton.GetComponentInChildren<TMP_Text>().text = "Restart";
    }
}