using B3dmCore;
using NUnit.Framework;

namespace B3dm.Tile.Tests;

public class BufferPaddingTests
{
    [Test]
    public void Initial()
    {
        var featureTableJson = "{\"INSTANCES_LENGTH\":2,\"POSITION\":{\"byteOffset\":0},\"EAST_NORTH_UP\":false,\"RTC_CENTER\":{\"byteOffset\":24}}";
        var paddedJson = BufferPadding.AddPadding(featureTableJson, 2);
        Assert.That(paddedJson == "{\"INSTANCES_LENGTH\":2,\"POSITION\":{\"byteOffset\":0},\"EAST_NORTH_UP\":false,\"RTC_CENTER\":{\"byteOffset\":24}}       ");
    }
}
