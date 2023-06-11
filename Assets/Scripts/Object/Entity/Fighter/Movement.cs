using UnityEngine;
using UnityEngine.Serialization;

namespace Object.Entity.Fighter
{
  public class Movement : MonoBehaviour
  {
    [SerializeField]
    protected FighterController controller;

    protected new Rigidbody2D rigidbody;

    [Header("Movement - basic")]
    [SerializeField]
    protected new Collider2D collider;

    protected bool canJump;

    [HideInInspector]
    public float direction;

    public Vector2 dirVector => currentDirection switch
    {
      Direction.Left  => Vector2.left,
      Direction.None  => Vector2.zero,
      Direction.Right => Vector2.right,
      _               => Vector2.zero
    };

    public float moveSpeed = 1f;

    public float jumpPower = 3f;

    [HideInInspector]
    public bool canFlip = true;

    [HideInInspector]
    public Direction currentDirection;

    [FormerlySerializedAs("layerMask")]
    [Header("Check Ground")]
    [SerializeField]
    private LayerMask groundLayer;

    private float checkDistanceX;

    private float checkDistanceY;

    public bool isCheckGround = true;

    [Header("Animation Parameters")]
    [SerializeField]
    private string walkingAnim;

    [SerializeField]
    private string jumpingAnim;

    protected Animator animator;

    protected virtual void Awake()
    {
      rigidbody = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();

      rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

      if (!isCheckGround) return;
      var bounds = collider.bounds;
      checkDistanceX = bounds.extents.x;
      checkDistanceY = bounds.extents.y + 0.1f;
    }

    protected virtual void FixedUpdate()
    {
      if (controller.HasState(State.Stun))
        return;
      transform.Translate(direction * moveSpeed * Time.fixedDeltaTime, 0f, 0f);
    }

    protected virtual void Update()
    {
      animator.SetBool(walkingAnim, Mathf.Abs(direction) > 0);
      CheckGround();
      Flip();
    }

    protected virtual void Jump()
    {
      if (!canJump || controller.HasState(State.Stun))
        return;

      SetJump(true);
      rigidbody.velocity = Vector2.up * jumpPower;
    }

    public void Move(float amount) => direction = amount;

    public void Move(Direction dir) => direction = (float) dir;

    protected void CheckGround()
    {
      if (!isCheckGround) return;
      
      const float distance = 0.3f;
      var pos = GetColliderCenter();
      var leftVector = new Vector2(pos.x - checkDistanceX, pos.y - checkDistanceY);
      var rightVector = new Vector2(pos.x + checkDistanceX, pos.y - checkDistanceY);

      var hitLeft = Physics2D.Raycast(leftVector, Vector2.down, distance, groundLayer);
      var hitRight = Physics2D.Raycast(rightVector, Vector2.down, distance, groundLayer);

      Debug.DrawRay(leftVector, Vector3.down * distance, Color.green);
      Debug.DrawRay(rightVector, Vector3.down * distance, Color.green);

      var check = (hitLeft || hitRight);
      canJump = check;
      SetJump(!check);
    }

    protected void SetJump(bool value) => animator.SetBool(jumpingAnim, value);

    protected void Flip()
    {
      if (!canFlip)
        return;

      var scale = transform.localScale;
      scale.x = direction switch
      {
        > 0 => Mathf.Abs(scale.x),
        < 0 => -Mathf.Abs(scale.x),
        _   => scale.x
      };
      transform.localScale = scale;

      currentDirection = scale.x > 0 ? Direction.Right : Direction.Left;
    }

    protected Vector2 GetColliderCenter()
    {
      var position = (Vector2) transform.position;
      var offset = collider.offset;

      return position + offset;
    }
  }
}
