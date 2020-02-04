using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CalculatorOperationChain
{
    //stolen from https://stackoverflow.com/a/1213649
    public static class CloneExtension
    {
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T) formatter.Deserialize(stream);
            }
        }
    }
}