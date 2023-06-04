using System;

namespace Element
{
  [Flags]
  public enum CombinedElement : uint
  {
    None = 0,
    
    Fire1 = 1 << 0,
    Water1 = 1 << 1,
    Grass1 = 1 << 2,
    Land1 = 1 << 3,
    Electricity1 = 1 << 4,
    
    Fire2 = 1 << 5,
    Water2 = 1 << 6,
    Grass2 = 1 << 7,
    Land2 = 1 << 8,
    Electricity2 = 1 << 9,
  }
}
