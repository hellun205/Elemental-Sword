using System;

namespace Object.Element
{
  [Flags]
  public enum ElementType
  {
    /// <summary>
    /// 없음
    /// </summary>
    None = 0,
    
    /// <summary>
    /// 불
    /// </summary>
    Fire = 1 << 0,

    /// <summary>
    /// 물
    /// </summary>
    Water = 1 << 1,

    /// <summary>
    /// 전기
    /// </summary>
    Electricity = 1 << 2,

    /// <summary>
    /// 풀
    /// </summary>
    Grass = 1 << 3,

    /// <summary>
    /// 땅
    /// </summary>
    Land = 1 << 4,
    
    All = int.MaxValue, 
  }
}
