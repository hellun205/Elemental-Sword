using System;
using UnityEngine;

namespace Camera
{
  public class ScreenFireEffect : MonoBehaviour
  {
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

    [SerializeField]
    private float distance = 0.5f;

    private void Awake()
    {
      ReSizeFires();
    }

    private void LateUpdate()
    {
      if (!Mathf.Approximately(tempSize, mainCamera.aspect))
      {
        tempSize = mainCamera.aspect;
        ReSizeFires();
      }
    }

    private void OnValidate()
    {
      ReSizeFires();
    }

    private void ReSizeFires()
    {
      var height = 2 * mainCamera.orthographicSize;
      var width = height * mainCamera.aspect;

      top.size = new Vector2(width, top.size.y);
      bottom.size = new Vector2(width, top.size.y);
      left.size = new Vector2(height, top.size.y);
      right.size = new Vector2(height, top.size.y);

      top.transform.localPosition = new Vector3(0f, height / 2 - distance, 1f);
      bottom.transform.localPosition = new Vector3(0f, -height / 2 + distance, 1f);
      left.transform.localPosition = new Vector3(-width / 2 + distance, 0f, 1f);
      right.transform.localPosition = new Vector3(width / 2 - distance, 0f, 1f);
    }
  }
}
