using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Manager;
using UnityEngine;
using Utils;

namespace Dialogue
{
  public class DialogueManager : GameObjectSingleTon<DialogueManager>
  {
    public delegate void DialogueEventListener();

    public event DialogueEventListener OnDialogueStart;
    public event DialogueEventListener OnDialogueEnd;

    [SerializeField]
    private DialogueController leftPanel;

    [SerializeField]
    private DialogueController rightPanel;

    private DialogueController activatedPanel;

    private AvartarDirection curDirection;

    private bool isOpened;
    private bool isWriting;

    private List<DialogueData> leftTexts;

    private Coroutine crt;

    protected override void Awake()
    {
      base.Awake();
      OnDialogueStart += () => Time.timeScale = 0f;
      OnDialogueEnd += () => Time.timeScale = 1f;
    }

    public void Open(IEnumerable<DialogueData> datas)
    {
      OnDialogueStart?.Invoke();
      leftTexts = datas.ToList();
      Next();
    }

    private void Open(DialogueData data)
    {
      if (isOpened && curDirection != data.direction)
        activatedPanel.Close();
      else
      {
        activatedPanel = data.direction == AvartarDirection.Left ? leftPanel : rightPanel;
        activatedPanel.Open();
      }
      
      isOpened = true;
      curDirection = data.direction;
      activatedPanel.img.sprite = data.avartar;


      crt = StartCoroutine(WriteRoutine(data.texts));
    }

    private void Update()
    {
      if (isOpened && Input.GetKeyDown(Managers.Key.dialogue))
      {
        if (crt is not null)
          StopCoroutine(crt);
        Next();
      }
    }

    private void Next()
    {
      if (leftTexts.Any())
      {
        Open(leftTexts[0]);
        leftTexts.RemoveAt(0);
      }
      else
      {
        Close();
      }
    }

    private IEnumerator WriteRoutine(TimingText[] texts)
    {
      var sb = new StringBuilder();
      isWriting = true;
      activatedPanel.tmp.text = sb.ToString();
      foreach (var text in texts)
      {
        yield return new WaitForSecondsRealtime(text.delay);
        foreach (var chr in text.text.ToCharArray())
        {
          sb.Append(chr);
          activatedPanel.tmp.text = sb.ToString();
          // Managers.Audio ...
          yield return new WaitForSecondsRealtime(text.speed);
        }
      }

      isWriting = false;
    }

    private void Close()
    {
      activatedPanel.Close();
      isOpened = false;
      OnDialogueEnd?.Invoke();
    }

    [ContextMenu("TestDialogue")]
    private void Test()
    {
      Open(new []
      {
        Managers.Player.GetDialogueData(new []{new TimingText("으와 안녕하세요")}),
        
        Managers.Player.GetDialogueData(new []{new TimingText("저는입니다")}),
        Managers.Player.GetDialogueData(new []{new TimingText("으와 하세요")})
      });
    }
  }
}