using System.IO.MemoryMappedFiles;
namespace Hex
{
    public class Class1
    {
        public void Foo()
        {
            MemoryMappedFile mapped = MemoryMappedFile.CreateFromFile("E:\\Download\\Bones_Tales_The_Manor_0.30.3\\Game.rgss3a");
            var tmp = mapped.CreateViewStream();
        }

    }
}
