using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour
{
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
    [SerializeField] float firstTimeStarted;
    void Start()
    {
        if(Instance){Destroy(Instance.gameObject);}
        Instance = this;
        textBox.text = "C:\\Users\\bob>";
        showCursor = true;
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        textBox.text = textBox.text.Replace("_", "");
    }
    void LateUpdate()
    {
        if(typing) {textBox.text += "_"; return; }
        if(!showCursor){ return; }
        if((Time.unscaledTime % CursorBlinkInterval)/CursorBlinkInterval > 0.5f){textBox.text += "_";}
    }
    public IEnumerator cutscene1()//load venv & start world 1
    {
        firstTimeStarted = Time.time;
        SceneManager.LoadScene(1);
        showCursor = true;
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"cd python\\hackerbot
");
        textBox.text += @"
C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"venv\Scripts\activate
");
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"python train.py
");
        textBox.text += "TARGET: ";
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"192.168.38.217:1
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt;
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text += @"
TRAINING STARTED.
";
        yield return new WaitForSecondsRealtime(1.5f);
        yield return FadeOut();
        Time.timeScale = 1;
        timeStarted = Time.time;
    }
    public IEnumerator cutscene2()//show results for world 1 & load world 2
    {
        int completionTime = Mathf.RoundToInt(Time.time - timeStarted);
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"TRAINING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"RETURNED PASSCODE: password
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"EXPECTED PASSCODE: password
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"TRAINING SUCCESSFUL.
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"cls
");
        textBox.text = "(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"python train.py
");
        textBox.text += "TARGET: ";
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"192.168.38.217:2
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt;
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text += @"
TRAINING STARTED.
";
        yield return new WaitForSecondsRealtime(1.5f);
        yield return FadeOut();
        Time.timeScale = 1;
        timeStarted = Time.time;
    }
    public IEnumerator cutscene3()//show results for world 2 & load world 3
    {
        int completionTime = Mathf.RoundToInt(Time.time - timeStarted);
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"TRAINING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"RETURNED PASSCODE: PassW0rD
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"EXPECTED PASSCODE: PassW0rD
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"TRAINING SUCCESSFUL.
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"cls
");
        textBox.text = "(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"python train.py
");
        textBox.text += "TARGET: ";
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"192.168.38.217:3
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt;
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text += @"
TRAINING STARTED.
";
        yield return FadeOut();
        Time.timeScale = 1;
        timeStarted = Time.time;
    }
    public IEnumerator cutscene4()//show results for world 3 & load world 4
    {
        int completionTime = Mathf.RoundToInt(Time.time - timeStarted);
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"TRAINING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"RETURNED PASSCODE: B37GAnb1
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"EXPECTED PASSCODE: B37GAnb1
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"TRAINING SUCCESSFUL.
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"cls
");
        textBox.text = "(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"python hack.py
");
        textBox.text += "TARGET: ";
        yield return new WaitForSecondsRealtime(ReactionTime);
        yield return TypeText(@"142.250.191.46:80
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt;
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSecondsRealtime(0.75f);
        textBox.text += @"
HACKING STARTED.
";
        yield return FadeOut();
        Time.timeScale = 1;
        timeStarted = Time.time;
    }
    public IEnumerator cutscene5()//show results for world 4
    {
        int completionTime = Mathf.RoundToInt(Time.time - timeStarted);
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"HACKING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text += @"RETURNED PASSCODE: Os+L53\czsN(7?kOA7\Y>""_>,t8vU8oP
";
        yield return new WaitForSecondsRealtime(2f);
        showCursor = true;
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSecondsRealtime(ReactionTime);
    }
    IEnumerator TypeText(string text)
    {
        typing = true;
        foreach(char c in text)
        {
            if (c == "\n"[0])
            {
                yield return new WaitForSecondsRealtime(TypeEnterInterval + (Random.value*2-1)*TypeRandomOffset);
                textBox.text += c;
                SoundPlayer.TypingSoundsPlayer.Enter();
                continue;
            }
            yield return new WaitForSecondsRealtime(TypeCharacterInterval + (Random.value*2-1)*TypeRandomOffset);
            textBox.text += c;
            if(c == ' '){SoundPlayer.TypingSoundsPlayer.Space();}
            else{SoundPlayer.TypingSoundsPlayer.Character();}
        }
        typing = false;
        Debug.Log($"Finished Typing {text}");
    }
    IEnumerator FadeOut()
    {
        while(canvasGroup.alpha > 0){
            canvasGroup.alpha -= Time.unscaledDeltaTime*0.5f;
            yield return 0;
        }
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