using Ecommerce.Services.Records;
using Tynamix.ObjectFiller;

namespace Ecommerce.Tests.Utilities;

public static class RecordSamples
{
    public static CategoryRecord Category => new Filler<CategoryRecord>().Create();
    
    public static List<CategoryRecord> Categories(int count = 3)
        => new Filler<CategoryRecord>().Create(count).ToList();
}