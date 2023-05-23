using UnityEngine;

namespace Object.Entity {
  public class Movement : MonoBehaviour {
    protected new Rigidbody2D rigidbody;
    
    [SerializeField]
    protected new Collider2D collider;

    protected virtual void Awake() {
      rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() {
      
    }
  }
}