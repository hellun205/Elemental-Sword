using Object.Pool;

namespace Object.Entity.Enemy {
  public class Frog : PoolManagement
  {
    public static PoolType Type => PoolType.Enemy_Frog;
    public override PoolType type => Type;

  }
}