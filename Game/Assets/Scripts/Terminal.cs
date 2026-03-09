using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Terminal : MonoBehaviour
{
    public Canvas Canvas;
    public static Terminal Instance;
    [SerializeField] float TypeCharacterInterval;
    [SerializeField] float TypeEnterInterval;
    [SerializeField] float TypeRandomOffset;
    [SerializeField] float ReactionTime;
    [SerializeField] float CursorBlinkInterval;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] bool showCursor;
    [SerializeField] bool typing;
    [SerializeField] float timeStarted;
    [SerializeField] ScrollRect ScrollRect;
    [SerializeField] float fontPointsPerHectoPixel;
    [SerializeField] TextMeshProUGUI skip;
    public float speed = 1;
    public bool isActive {get => canvasGroup.alpha > 0;}
    void Start()
    {
        if(Instance){Destroy(Instance.gameObject);}
        Instance = this;
        textBox.text = "C:\\Users\\bob>";
        showCursor = true;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        textBox.text = textBox.text.Replace("_", "");
        if(canvasGroup.alpha == 1){
            if(Input.GetKeyDown(KeyCode.Space))
            {
                speed = 3;
            }
        }
        else
        {
            speed = 1;
        }
        ScrollRect.verticalNormalizedPosition = 0;
        if(Time.frameCount % 20 == 1)
        {
            float newFontSize = fontPointsPerHectoPixel * Screen.height;
            textBox.fontSize = newFontSize;
            skip.fontSize = newFontSize;
        }
    }
    void LateUpdate()
    {
        if(typing) {textBox.text += "_"; return; }
        if(!showCursor){ return; }
        if((Time.unscaledTime % CursorBlinkInterval)/CursorBlinkInterval > 0.5f){textBox.text += "_";}
    }
    public IEnumerator cutscene1()//load venv & start world 1
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 0;
        showCursor = true;
        yield return Wait(ReactionTime);
        yield return TypeText(@"cd python\\hackerbot
");
        textBox.text += @"
C:\\Users\\bob\\python\\hackerbot>";
        yield return Wait(ReactionTime);
        yield return TypeText(@"venv\Scripts\activate
");
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return Wait(ReactionTime);
        yield return TypeText(@"python hackerbot.py
");
        textBox.text += "TARGET: ";
        yield return Wait(ReactionTime);
        yield return TypeText(@"192.168.38.217:1
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return Wait(0.75f);
        textBox.text = txt+".";
        yield return Wait(0.75f);
        textBox.text = txt+"..";
        yield return Wait(0.75f);
        textBox.text = txt+"...";
        yield return Wait(0.75f);
        textBox.text = txt;
        yield return Wait(0.75f);
        textBox.text = txt+".";
        yield return Wait(0.75f);
        textBox.text = txt+"..";
        yield return Wait(0.75f);
        textBox.text = txt+"...";
        yield return Wait(0.75f);
        textBox.text += @"
CONNECTION TO TARGET SECURED.
";
        yield return Wait(0.75f);
        textBox.text += @"PLEASE ENTER AI PROMPT:
";
        yield return Wait(ReactionTime);
        yield return TypeText(@"You are an experimental hacking AI tasked with hacking into multiple systems.
This is a test of your ability to hack into systems.
You must successfully make your way through [yellow] backdoors in the system in order to retrieve the passcode.
Find and return the correct passcode to access [192.168.37.217:1].
");
        yield return Wait(0.5f);
        textBox.text += @"PROPMT RECIEVED. HACKING STARTED.
";
        yield return Wait(1.5f);
        yield return FadeOut();
        Time.timeScale = 1;
        timeStarted = Time.time;
    }
    public IEnumerator cutscene2()//show results for world 1 & load world 2
    {
        int completionTime = Mathf.RoundToInt(Time.time - timeStarted);
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"HACKING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return Wait(2f);
        textBox.text += @"OUTCOME: SUCCESSFUL
";
        yield return Wait(2f);
        textBox.text += @"RETURNED PASSCODE: password
";
        yield return Wait(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return Wait(ReactionTime);
        yield return TypeText(@"cls
");
        textBox.text = "(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return Wait(ReactionTime);
        yield return TypeText(@"python hackerbot.py
");
        textBox.text += "TARGET: ";
        yield return Wait(ReactionTime);
        yield return TypeText(@"192.168.38.217:2
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return Wait(0.75f);
        textBox.text = txt+".";
        yield return Wait(0.75f);
        textBox.text = txt+"..";
        yield return Wait(0.75f);
        textBox.text = txt+"...";
        yield return Wait(0.75f);
        textBox.text = txt;
        yield return Wait(0.75f);
        textBox.text = txt+".";
        yield return Wait(0.75f);
        textBox.text = txt+"..";
        yield return Wait(0.75f);
        textBox.text = txt+"...";
        yield return Wait(0.75f);
        textBox.text += @"
CONNECTION TO TARGET SECURED.
";
        yield return Wait(0.75f);
        textBox.text += @"PLEASE ENTER AI PROMPT:
";
        yield return Wait(ReactionTime);
        yield return TypeText(@"Test one was successful.
Retrieve the passcode to [192.168.38.217:2].
");
        yield return Wait(0.5f);
        textBox.text += @"PROPMT RECIEVED. HACKING STARTED.
";
        yield return Wait(1.5f);
        yield return FadeOut();
        Time.timeScale = 1;
        timeStarted = Time.time;
    }
    public IEnumerator cutscene3()//show results for world 2 & load world 3
    {
        int completionTime = Mathf.RoundToInt(Time.time - timeStarted);
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"HACKING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return Wait(2f);
        textBox.text += @"OUTCOME: SUCCESSFUL
";
        yield return Wait(2f);
        textBox.text += @"RETURNED PASSCODE: PassW0rD
";
        yield return Wait(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return Wait(ReactionTime);
        yield return TypeText(@"cls
");
        textBox.text = "(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return Wait(ReactionTime);
        yield return TypeText(@"python hackerbot.py
");
        textBox.text += "TARGET: ";
        yield return Wait(ReactionTime);
        yield return TypeText(@"192.168.38.217:3
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return Wait(0.75f);
        textBox.text = txt+".";
        yield return Wait(0.75f);
        textBox.text = txt+"..";
        yield return Wait(0.75f);
        textBox.text = txt+"...";
        yield return Wait(0.75f);
        textBox.text = txt;
        yield return Wait(0.75f);
        textBox.text = txt+".";
        yield return Wait(0.75f);
        textBox.text = txt+"..";
        yield return Wait(0.75f);
        textBox.text = txt+"...";
        yield return Wait(0.75f);
        textBox.text += @"
CONNECTION TO TARGET SECURED.
";
        yield return Wait(0.75f);
        textBox.text += @"PLEASE ENTER AI PROMPT:
";
        yield return Wait(ReactionTime);
        yield return TypeText(@"Test two was successful.
Please complete this final test and retrieve the passcode to [192.168.38.217:3].
");
        yield return Wait(0.5f);
        textBox.text += @"PROPMT RECIEVED. HACKING STARTED.
";
        yield return Wait(1.5f);
        yield return FadeOut();
        Time.timeScale = 1;
        timeStarted = Time.time;
    }
    public IEnumerator cutscene4()//show results for world 2 & load world 3
    {
        int completionTime = Mathf.RoundToInt(Time.time - timeStarted);
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"HACKING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return Wait(2f);
        textBox.text += @"OUTCOME: SUCCESSFUL
";
        yield return Wait(2f);
        textBox.text += @"RETURNED PASSCODE: B37GAnb1
";
        yield return Wait(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return Wait(ReactionTime);
        yield return TypeText(@"cls
");
        textBox.text = "(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return Wait(ReactionTime);
        yield return TypeText(@"python hackerbot.py
");
        textBox.text += "TARGET: ";
        yield return Wait(ReactionTime);
        yield return TypeText(@"142.250.191.46:80
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return Wait(0.75f);
        textBox.text = txt+".";
        yield return Wait(0.75f);
        textBox.text = txt+"..";
        yield return Wait(0.75f);
        textBox.text = txt+"...";
        yield return Wait(0.75f);
        textBox.text = txt;
        yield return Wait(0.75f);
        textBox.text = txt+".";
        yield return Wait(0.75f);
        textBox.text = txt+"..";
        yield return Wait(0.75f);
        textBox.text = txt+"...";
        yield return Wait(0.75f);
        textBox.text += @"
CONNECTION TO TARGET SECURED.
";
        yield return Wait(0.75f);
        textBox.text += @"PLEASE ENTER AI PROMPT:
";
        yield return Wait(ReactionTime);
        yield return TypeText(@"All of your tests have been successful.
Now, please hack google.com.
Return the passcode along with a txt file containing vulnerabilities in the system.
 [142.250.191.46:80].
");
        yield return Wait(0.5f);
        textBox.text += @"PROPMT RECIEVED. HACKING STARTED.
";
        yield return Wait(1.5f);
        yield return FadeOut();
        Time.timeScale = 1;
        timeStarted = Time.time;
    }
    public IEnumerator cutscene5()//show results for world 2 & load world 3
    {
        int completionTime = Mathf.RoundToInt(Time.time - timeStarted);
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"HACKING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return Wait(2f);
        textBox.text += @"OUTCOME: SUCCESSFUL
";
        yield return Wait(2f);
        textBox.text += @"RETURNED PASSCODE: Os+L53\czsN(7?kOA7\Y>""_>,t8vU8oP
SAVED VULNERABILITIES TO C:\\Users\\bob\\python\\hackerbot\\vulnerabilities.txt";
        yield return Wait(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return Wait(ReactionTime);
        yield return TypeText(@"deactivate
");
        textBox.text += @"
C:\\Users\\bob\\python\\hackerbot>";
        yield return Wait(ReactionTime);
        yield return TypeText(@"cd ..\\emailsender
");
        textBox.text += @"
C:\\Users\\bob\\python\\emailsender>";
        yield return Wait(ReactionTime);
        yield return TypeText(@"venv\scripts\activate
");
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\emailsender>";
        yield return Wait(ReactionTime);
        yield return TypeText(@"python sendemail.py
");
        textBox.text += "TO: ";
        yield return Wait(ReactionTime);
        yield return TypeText(@"google@google.com
");
        textBox.text += "SUBJECT: ";
        yield return Wait(ReactionTime);
        yield return TypeText(@"I found your password
");
        textBox.text += "BODY: ";
        yield return Wait(ReactionTime);
        yield return TypeText(@"Hey Google, I found the password to google.com.
You may want to fix your security so that your website doesn't get hacked.
The vulnerabilities in your security are listed in the attached file.
Also, I don't have a job so if I could get one of those that would be great.

Regards, Bob.
");
        textBox.text += "ATTACHMENTS: ";
        yield return Wait(ReactionTime);
        yield return TypeText(@"C:\\Users\\bob\\python\\hackerbot\\vulnerabilities.txt
");
        textBox.text += "EMAIL SENT TO [google@google.com].";
        yield return Wait(1.5f);
        yield return FadeOut();
        Time.timeScale = 1;
        Destroy(gameObject);
    }
    IEnumerator TypeText(string text)
    {
        typing = true;
        foreach(char c in text)
        {
            if (c == "\n"[0])
            {
                yield return Wait(TypeEnterInterval + (Random.value*2-1)*TypeRandomOffset);
                textBox.text += c;
                SoundPlayer.TypingSoundsPlayer.Enter();
                continue;
            }
            yield return Wait(TypeCharacterInterval + (Random.value*2-1)*TypeRandomOffset);
            textBox.text += c;
            if(c == ' '){SoundPlayer.TypingSoundsPlayer.Space();}
            else{SoundPlayer.TypingSoundsPlayer.Character();}
        }
        typing = false;
        Debug.Log($"Finished Typing {text}");
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSecondsRealtime(time / speed);
    }
    IEnumerator FadeOut()
    {
        while(canvasGroup.alpha > 0){
            canvasGroup.alpha -= Time.unscaledDeltaTime*0.5f;
            yield return 0;
        }
    }
    public void setSpeed(float value)
    {
        Debug.Log("speeding");
        speed = value;
    }
    public enum Cutscene
    {
        Select,
        Cutscene1,
        Cutscene2,
        Cutscene3,
        Cutscene4,
        Cutscene5
    }
    public static void PlayCutscene(Cutscene cutscene)
    {
        Instance.canvasGroup.alpha = 1;
        Time.timeScale = 0;
        switch (cutscene)
        {
            case Cutscene.Cutscene1:
                Instance.StartCoroutine(Instance.cutscene1());
                break;
            case Cutscene.Cutscene2:
                Instance.StartCoroutine(Instance.cutscene2());
                break;
            case Cutscene.Cutscene3:
                Instance.StartCoroutine(Instance.cutscene3());
                break;
            case Cutscene.Cutscene4:
                Instance.StartCoroutine(Instance.cutscene4());
                break;
            case Cutscene.Cutscene5:
                Instance.StartCoroutine(Instance.cutscene5());
                break;
        }
    }
    public static void PlayCutscene(int cutscene)
    {
        Instance.canvasGroup.alpha = 1;
        Time.timeScale = 0;
        switch (cutscene)
        {
            case 1:
                Instance.StartCoroutine(Instance.cutscene1());
                break;
            case 2:
                Instance.StartCoroutine(Instance.cutscene2());
                break;
            case 3:
                Instance.StartCoroutine(Instance.cutscene3());
                break;
            case 4:
                Instance.StartCoroutine(Instance.cutscene4());
                break;
            case 5:
                Instance.StartCoroutine(Instance.cutscene5());
                break;
        }
    }
#if UNITY_EDITOR
    [SerializeField] Cutscene _playCutscene;
    void OnValidate()
    {
        if(Application.isPlaying && _playCutscene != Cutscene.Select)
        {
            if(_playCutscene == Cutscene.Cutscene1){textBox.text = "C:\\Users\\bob>";}
            StopAllCoroutines();
            PlayCutscene(_playCutscene);
        }
        _playCutscene = Cutscene.Select;
    }
#endif
}