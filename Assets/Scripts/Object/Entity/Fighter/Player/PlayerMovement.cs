using Manager;
using UnityEngine;

namespace Object.Entity.Fighter.Player
{
  public class PlayerMovement : Movement
  {
    public bool isInputCooldown => curInputCooldown < inputCooldown;
    public float inputCooldown = 0.5f;
    private float curInputCooldown;

    private Status status => controller.status;

    private global::Player.Player player;

    protected override void Awake()
    {
      base.Awake();
      animator = GetComponent<Animator>();
      // controller = GetComponent<PlayerController>();
      player = FindObjectOfType<global::Player.Player>();
      
      curInputCooldown = inputCooldown;
    }

    protected override void Update()
    {
      base.Update();
      if (isInputCooldown)
        curInputCooldown += Time.deltaTime;
      else
      {
        var interactable = player.interactable;
        var horizontal = Input.GetAxisRaw("Horizontal");
        moveSpeed = status.moveSpeed;
        jumpPower = status.jumpPower;
        // animator.SetFloat("moveSpeed", Mathf.Max(1f, status.moveSpeed));
        Move(interactable ? horizontal : 0f);
        if (interactable && Input.GetKeyDown(Managers.Key.jump) && canJump)
        {
          // Managers.Audio.PlaySFX("jump");
          Jump();
        }
      }
    }

    public void EnableInputCooldown() => curInputCooldown = 0f;
  }
}
