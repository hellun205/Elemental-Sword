using UnityEngine;

namespace Dialogue
{
  public interface ITalker
  {
    public Sprite avartar { get; }
    
    public AvartarDirection avartarDirection { get; }
  }

  public static class TalkerExtensions
  {
    public static DialogueData GetDialogueData(this ITalker talker) 
      => new DialogueData(talker.avartarDirection, talker.avartar);
    
    
    public static DialogueData GetDialogueData(this ITalker talker, TimingText[] texts) 
      => new DialogueData(talker.avartarDirection, talker.avartar, texts);
  }
}