using Animation.Combined;
using UnityEngine;

namespace Animation.Preset
{
  public sealed class PanelVisibler : CombinedAnimationPreset<SmoothSizeAndFade>
  {
    private CanvasGroup canvasGroup { get; }
    private Transform transform { get; }
    
    public PanelVisibler(MonoBehaviour sender) : base(sender)
    {
      canvasGroup = sender.GetComponent<CanvasGroup>();
      transform = sender.transform;
      InitAnimation();
    }

    public PanelVisibler(MonoBehaviour sender, Transform transform, CanvasGroup canvasGroup) : base(sender)
    {
      this.transform = transform;
      this.canvasGroup = canvasGroup;
      InitAnimation();
    }

    private void InitAnimation()
    {
      animation = new(sender,
        new(() => transform.localScale, value => transform.localScale = value),
        new(() => canvasGroup.alpha, value => canvasGroup.alpha = value))
      {
        minSize = new(0.8f, 0.8f),
        maxSize = transform.localScale,
        minAlpha = 0f,
        maxAlpha = 1f,
        fadeShowAnimSpeed = 6f,
        fadeHideAnimSpeed = 6f,
        sizeShowAnimSpeed = 8f,
        sizeHideAnimSpeed = 8f
      };

      animation.onStarted += _ => canvasGroup.blocksRaycasts = true;
      animation.onHid += _ => canvasGroup.blocksRaycasts = false;
      canvasGroup.alpha = 0f;
      canvasGroup.blocksRaycasts = false;
    }

    public override void Start() => Show();

    public override void Stop() => Hide();

    public void Show() => animation.Show();

    public void Hide() => animation.Hide();
  }
}
