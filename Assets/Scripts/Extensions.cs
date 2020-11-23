namespace FlatMango.Maze
{
    using Random = UnityEngine.Random;


    public static class Extensions
    {
        public static void Randomize<T>(this T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                T item = array[i];
                int random = Random.Range(i, array.Length);
                array[i] = array[random];
                array[random] = item;
            }
        }
    }
}