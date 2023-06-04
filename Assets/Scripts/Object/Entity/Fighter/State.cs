using System;

namespace Object.Entity.Fighter
{
  [Flags]
  public enum State : byte
  {
    None = 0,
    Burning = 1 << 0,
    Slow = 1 << 1,
    Stun = 1 << 2,
    
  }
}
