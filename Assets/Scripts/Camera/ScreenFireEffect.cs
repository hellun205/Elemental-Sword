using System.Collections;
using System.Collections.Generic;
using Animation;
using Manager;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Camera
{
  public class ScreenFireEffect : SingleTon<ScreenFireEffect>, IDontDestroy
  {
    [Header("Fire Objects")]
    [SerializeField]
    private SpriteRenderer top;

    [SerializeField]
    private SpriteRenderer left;

    [SerializeField]
    private SpriteRenderer bottom;

    [SerializeField]
    private SpriteRenderer right;

    private List<SpriteRenderer> all => new() { top, left, right, bottom };

    private UnityEngine.Camera mainCamera => UnityEngine.Camera.main;

    private float tempSize;

    [Header("Distances")]
    [SerializeField]
    private float distance = 0.5f;

    [SerializeField]
    private float visibleDistance = 0.5f;

    [SerializeField]
    private float invisibleDistance = 0f;

    // Animation
    private Changef visibilityAnim;

    private ChangeColor colorAnim;

    protected override void Awake()
    {
      base.Awake();
      ReSizeFires();
      visibilityAnim = new Changef(this, value =>
      {
        distance = value;
        ReSizeFires();
      }, distance);

      colorAnim = new ChangeColor(this, color => all.ForEach(sr => sr.color = color), top.color);
    }

    private void LateUpdate()
    {
      if (Mathf.Approximately(tempSize, mainCamera.aspect)) return;
      tempSize = mainCamera.aspect;
      ReSizeFires();
    }

    // private void OnValidate()
    // {
    //    EditorApplication.delayCall += ReSizeFires;
    // }

    private void ReSizeFires()
    {
      var height = 2 * mainCamera.orthographicSize;
      var width = height * mainCamera.aspect;

      top.size = new Vector2(width, top.size.y);
      bottom.size = new Vector2(width, bottom.size.y);
      left.size = new Vector2(height, left.size.y);
      right.size = new Vector2(height, right.size.y);

      top.transform.localPosition = new Vector3(0f, height / 2 - distance, 1f);
      bottom.transform.localPosition = new Vector3(0f, -height / 2 + distance, 1f);
      left.transform.localPosition = new Vector3(-width / 2 + distance, 0f, 1f);
      right.transform.localPosition = new Vector3(width / 2 - distance, 0f, 1f);
    }


    public void ChangeColor(Color color, float speed = 3f)
      => colorAnim.Start(top.color, color, speed);

    public void SetVisibility(bool visible, float speed = 3f)
      => visibilityAnim.Start(distance, visible ? visibleDistance : invisibleDistance, speed);

    // private void Start()
    // {
    //   StartCoroutine(Test());
    // }

    // private IEnumerator Test()
    // {
    //   while (true)
    //   {
    //     yield return new WaitForSeconds(2.5f);
    //     ChangeColor(Color.blue, 1f);
    //     yield return new WaitForSeconds(2.5f);
    //     ChangeColor(Color.red, 1f);
    //   }
    // }
  }
}