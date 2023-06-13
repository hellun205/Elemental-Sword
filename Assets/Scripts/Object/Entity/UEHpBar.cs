using Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Object.Entity
{
  public class UEHpBar : UIEntity
  {
    private const float MiddleFollowSpeed = 4f;

    private SmoothFloat animMiddle;

    [SerializeField]
    private Image topImg;

    [SerializeField]
    private Image middleImg;

    private float value;
    private float maxValue;

    public float Value
    {
      get => value;
      set
      {
        this.value = value;
        topImg.fillAmount = this.value / MaxValue;
        animMiddle.Start(middleImg.fillAmount, topImg.fillAmount, MiddleFollowSpeed);
      }
    }

    public float MaxValue
    {
      get => maxValue;
      set
      {
        maxValue = value;
        this.Value = this.Value;
      }
    }

    protected override void Awake()
    {
      base.Awake();
      animMiddle = new(this, new(() => middleImg.fillAmount, value => middleImg.fillAmount = value));
    }

    public void Init(float value, float maxValue)
    {
      Value = value;
      MaxValue = maxValue;
    }
  }
}
