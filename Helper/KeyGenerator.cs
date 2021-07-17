namespace AlgorithmsServer.Helper
{
    public class KeyGenerator
    {
        public static string Generator(string key)
        {
            key = key.Replace(" ", string.Empty);

            if (key.Length <= 16)
            {
                key = key.PadRight(16, '0');
            }
            else
            {
                key = key.Remove(16);
            }

            var arr = key.ToCharArray();

            return string.Join(' ', arr);
        }
    }
}