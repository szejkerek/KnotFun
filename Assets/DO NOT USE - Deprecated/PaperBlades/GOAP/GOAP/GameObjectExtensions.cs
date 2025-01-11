public static class GameObjectExtensions
{
    /// Returns the object itself if it exists, null otherwise.
    public static T OrNull<T>(this T obj) where T : UnityEngine.Object => obj ? obj : null;
}
