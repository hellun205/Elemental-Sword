using System.Collections;
using Manager;
using UnityEditor;
using UnityEngine;

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

    private SpriteRenderer[] all => new[] { top, left, right, bottom };

    private UnityEngine.Camera mainCamera => UnityEngine.Camera.main;

    private float tempSize;

    [Header("Distances")]
    [SerializeField]
    private float distance = 0.5f;

    [SerializeField]
    private float visibleDistance = 0.5f;

    [SerializeField]
    private float invisibleDistance = 0f;

    private Coroutine colorChange;
    private Coroutine visibilityChange;

    protected override void Awake()
    {
      base.Awake();
      ReSizeFires();
    }

    private void LateUpdate()
    {
      if (Mathf.Approximately(tempSize, mainCamera.aspect)) return;
      tempSize = mainCamera.aspect;
      ReSizeFires();
    }
  
    private void OnValidate()
    {
      EditorApplication.delayCall += ReSizeFires;
    }

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

    private IEnumerator ColorChangeCRT(Color toColor, float smoothing = 3f)
    {
      while (true)
      {
        foreach (var sr in all)
        {
          sr.color = Color.Lerp(sr.color, toColor, Time.deltaTime * smoothing);
          if (sr.color == toColor)
            yield break;
        }
        yield return new WaitForEndOfFrame();
      }
    }

    public void ChangeColor(Color color, float smoothing = 3f)
    {
      if (colorChange is not null) StopCoroutine(colorChange);
      colorChange = StartCoroutine(ColorChangeCRT(color, smoothing));
    }

    private IEnumerator VisibilityChangeCRT(bool visibie, float smoothing = 3f)
    {
      while (true)
      {
        var toDistance = visibie ? visibleDistance : invisibleDistance;
        distance = Mathf.Lerp(distance, toDistance, Time.deltaTime * smoothing);
        ReSizeFires();
        if (Mathf.Approximately(distance, toDistance))
          yield break;
        yield return new WaitForEndOfFrame();
      }
    }

    public void SetVisibility(bool visible, float smoothing = 3f)
    {
      if (visibilityChange is not null) StopCoroutine(visibilityChange);
      visibilityChange = StartCoroutine(VisibilityChangeCRT(visible, smoothing));
    }

    // private void Start()
    // {
    //   StartCoroutine(Test());
    // }
    //
    // private IEnumerator Test()
    // {
    //   while (true)
    //   {
    //     SetVisibility(false);
    //     yield return new WaitForSeconds(2.5f);
    //     SetVisibility(true);
    //     yield return new WaitForSeconds(2.5f);
    //   }
    // }
  }
}
