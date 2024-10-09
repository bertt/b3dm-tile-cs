using NUnit.Framework;

namespace B3dmCore.Tests;

public class B3dmHeaderTests
{
    [Test]
    public void HeaderToBinaryTests()
    {
        var b3dmHeader = new B3dmHeader {
            ByteLength = 2952
        };
        var binary = b3dmHeader.AsBinary();

        var errors = b3dmHeader.Validate();
        Assert.That(binary.Length == 28);
    }
}
