using UnityEngine;

namespace Object.Entity.Fighter {
  public class Movement : MonoBehaviour {
    protected new Rigidbody2D rigidbody;

    [Header("Movement - basic")]
    [SerializeField]
    protected new Collider2D collider;

    protected bool canJump;

    [HideInInspector]
    public float direction;

    public float moveSpeed = 1f;
    public float jumpPower = 3f;

    [HideInInspector]
    public bool canFlip = true;

    [HideInInspector]
    public Direction currentDirection;

    [Header("Check Ground")]
    [SerializeField]
    private string groundTag = "Ground";

    private float checkDistanceX;

    private float checkDistanceY;

    // [SerializeField]
    // private LayerMask layerMask;

    [Header("Animation Parameters")]
    [SerializeField]
    private string walkingAnim;

    [SerializeField]
    private string jumpingAnim;

    private Animator animator;

    protected virtual void Awake() {
      rigidbody = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();

      rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

      var bounds = collider.bounds;
      checkDistanceX = bounds.extents.x + 0.01f;
      checkDistanceY = bounds.extents.y + 0.05f;
    }

    protected virtual void Update() {
      var velocity = rigidbody.velocity;
      velocity.x = direction * moveSpeed;
      rigidbody.velocity = velocity;

      animator.SetBool(walkingAnim, Mathf.Abs(direction) > 0);
      CheckGround();
      Flip();
    }

    protected virtual void Jump() {
      if (!canJump) return;
      SetJump(true);
      rigidbody.velocity = Vector2.up * jumpPower;
    }

    protected void Move(float amount) => direction = amount;

    protected void CheckGround() {
      var pos = GetColliderCenter();
      const float distance = 0.1f;
      pos.y -= checkDistanceY;
      // left
      pos.x -= checkDistanceX;
      var hitLeft = Physics2D.Raycast(pos, Vector2.down, distance);
      Debug.DrawRay(pos, Vector3.down * distance, Color.green);
      // right
      pos.x += checkDistanceX * 2;
      var hitRight = Physics2D.Raycast(pos, Vector2.down, distance);
      Debug.DrawRay(pos, Vector3.down * distance, Color.green);

      var check = (hitLeft || hitRight) &&
                  (hitLeft.transform.CompareTag(groundTag) || hitRight.transform.CompareTag(groundTag));
      canJump = check;
      SetJump(!check);
      // Debug.Log(check);
    }

    protected void SetJump(bool value) => animator.SetBool(jumpingAnim, value);

    protected void Flip() {
      if (!canFlip) return;
      var scale = transform.localScale;
      scale.x = direction switch {
        > 0 => Mathf.Abs(scale.x),
        < 0 => -Mathf.Abs(scale.x),
        _ => scale.x
      };
      transform.localScale = scale;

      currentDirection = scale.x > 0 ? Direction.Right : Direction.Left;
    }

    protected Vector2 GetColliderCenter() {
      var position = (Vector2)transform.position;
      var offset = collider.offset;

      return position + offset;
    }
  }
}