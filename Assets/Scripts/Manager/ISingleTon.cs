namespace Manager
{
  public interface ISingleTon<T>
  {
    public static T Instance { get; protected set; }
  }
}
