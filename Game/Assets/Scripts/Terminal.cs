using System.Collections;
using TMPro;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    public static Terminal Instance;
    [SerializeField] float TypeCharacterInterval;
    [SerializeField] float TypeEnterInterval;
    [SerializeField] float TypeRandomOffset;
    [SerializeField] float ReactionTime;
    [SerializeField] float CursorBlinkInterval;
    [SerializeField] TextMeshProUGUI textBox;
    [SerializeField] bool showCursor;
    [SerializeField] bool typing;
    void Start()
    {
        Instance = this;
        textBox.text = "C:\\Users\\bob>";
        showCursor = true;
    }
    void Update()
    {
        textBox.text = textBox.text.Replace("_", "");
    }
    void LateUpdate()
    {
        if(typing) {textBox.text += "_"; return; }
        if(!showCursor){ return; }
        if((Time.time % CursorBlinkInterval)/CursorBlinkInterval > 0.5f){textBox.text += "_"; Debug.Log("yee");}
        else{Debug.Log((Time.time % CursorBlinkInterval)/CursorBlinkInterval);}
    }
    public IEnumerator cutscene1()//load venv & start world 1
    {
        showCursor = true;
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"cd python\\hackerbot
");
        textBox.text += @"
C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"venv\Scripts\activate
");
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"python train.py
");
        textBox.text += "TARGET: ";
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"192.168.38.217:1
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt;
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSeconds(0.75f);
        textBox.text += @"
TRAINING STARTED.
";
    }
    public IEnumerator cutscene2(int completionTime)//show results for world 1 & load world 2
    {
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"TRAINING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"RETURNED PASSCODE: password
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"EXPECTED PASSCODE: password
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"TRAINING SUCCESSFUL.
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"cls
");
        textBox.text = "(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"python train.py
");
        textBox.text += "TARGET: ";
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"192.168.38.217:2
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt;
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSeconds(0.75f);
        textBox.text += @"
TRAINING STARTED.
";
    }
    public IEnumerator cutscene3(int completionTime)//show results for world 2 & load world 3
    {
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"TRAINING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"RETURNED PASSCODE: PassW0rD
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"EXPECTED PASSCODE: PassW0rD
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"TRAINING SUCCESSFUL.
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"cls
");
        textBox.text = "(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"python train.py
");
        textBox.text += "TARGET: ";
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"192.168.38.217:3
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt;
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSeconds(0.75f);
        textBox.text += @"
TRAINING STARTED.
";
    }
    public IEnumerator cutscene4(int completionTime)//show results for world 3 & load world 4
    {
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"TRAINING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"RETURNED PASSCODE: B37GAnb1
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"EXPECTED PASSCODE: B37GAnb1
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"TRAINING SUCCESSFUL.
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        showCursor = true;
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"cls
");
        textBox.text = "(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"python hack.py
");
        textBox.text += "TARGET: ";
        yield return new WaitForSeconds(ReactionTime);
        yield return TypeText(@"142.250.191.46:80
");
        showCursor = false;
        string txt = textBox.text + @"INITIALIZING";
        textBox.text = txt;
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt;
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+".";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"..";
        yield return new WaitForSeconds(0.75f);
        textBox.text = txt+"...";
        yield return new WaitForSeconds(0.75f);
        textBox.text += @"
HACKING STARTED.
";
    }
    public IEnumerator cutscene5(int completionTime)//show results for world 4
    {
        int seconds = completionTime % 60;
        int minutes = completionTime / 60;
        textBox.text += @$"HACKING COMPLETED IN {minutes}:{seconds.ToString("D2")}.
";
        yield return new WaitForSeconds(2f);
        textBox.text += @"RETURNED PASSCODE: Os+L53\czsN(7?kOA7\Y>""_>,t8vU8oP
";
        yield return new WaitForSeconds(2f);
        showCursor = true;
        textBox.text += @"
(venv) C:\\Users\\bob\\python\\hackerbot>";
        yield return new WaitForSeconds(ReactionTime);
    }
    IEnumerator TypeText(string text)
    {
        typing = true;
        foreach(char c in text)
        {
            if (c == "\n"[0])
            {
                yield return new WaitForSeconds(TypeEnterInterval + (Random.value*2-1)*TypeRandomOffset);
                textBox.text += c;
                continue;
            }
            yield return new WaitForSeconds(TypeCharacterInterval + (Random.value*2-1)*TypeRandomOffset);
            textBox.text += c;
        }
        typing = false;
        Debug.Log($"Finished Typing {text}");
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
    public static void PlayCutscene(Cutscene cutscene, int completionTime = 0)
    {
        switch (cutscene)
        {
            case Cutscene.Cutscene1:
                Instance.StartCoroutine(Instance.cutscene1());
                break;
            case Cutscene.Cutscene2:
                Instance.StartCoroutine(Instance.cutscene2(completionTime));
                break;
            case Cutscene.Cutscene3:
                Instance.StartCoroutine(Instance.cutscene3(completionTime));
                break;
            case Cutscene.Cutscene4:
                Instance.StartCoroutine(Instance.cutscene4(completionTime));
                break;
            case Cutscene.Cutscene5:
                Instance.StartCoroutine(Instance.cutscene5(completionTime));
                break;
        }
    }
#if UNITY_EDITOR
    [SerializeField] Cutscene _playCutscene;
    void OnValidate()
    {
        if(Application.isPlaying)
        {
            if(_playCutscene == Cutscene.Cutscene1){textBox.text = "C:\\Users\\bob>";}
            StopAllCoroutines();
            PlayCutscene(_playCutscene, 367);
        }
        _playCutscene = Cutscene.Select;
    }
#endif
}