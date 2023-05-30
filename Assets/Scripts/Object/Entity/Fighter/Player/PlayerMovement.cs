using UnityEngine;

namespace Object.Entity.Fighter.Player {
  public class PlayerMovement : Movement {
    [Header("Movement - player")]
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    
    protected override void Update() {
      base.Update();
      var horizontal = Input.GetAxisRaw("Horizontal");
      var jump = Input.GetKeyDown(jumpKey);
      Move(horizontal);
      if (jump) Jump();
    }
  }
}