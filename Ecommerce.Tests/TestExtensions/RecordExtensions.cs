using Ecommerce.Services.Records;
using Shouldly;
using Xunit;

namespace Ecommerce.Tests.TestExtensions;

public static class RecordExtensions
{
    public static void CompareRecord(this BaseRecord actualRecord, BaseRecord expectedRecord, bool isCreateRecord = false)
    {
        expectedRecord.created_on = expectedRecord.created_on.ClearMilliseconds();
        expectedRecord.updated_on = expectedRecord.created_on.ClearMilliseconds();
        actualRecord.created_on = actualRecord.created_on.ClearMilliseconds();
        actualRecord.updated_on = actualRecord.updated_on.ClearMilliseconds();
        
        if (isCreateRecord)
        {
            expectedRecord.id = actualRecord.id;
        }
        
        expectedRecord.ShouldBeEquivalentTo(actualRecord);
    }
    
    public static void CompareRecords<T>(this List<T> actualRecords, List<T> expectedRecords) where T : BaseRecord
    {
        if (actualRecords.Count != expectedRecords.Count)
        {
            throw new ShouldAssertException($"Expected {expectedRecords.Count} records but found {actualRecords.Count} records");
        }

        foreach(var record in expectedRecords)
        {
            record.created_on = record.created_on.ClearMilliseconds();
            record.updated_on = record.updated_on.ClearMilliseconds();
        }

        foreach(var record in actualRecords)
        {
            record.created_on = record.created_on.ClearMilliseconds();
            record.updated_on = record.updated_on.ClearMilliseconds();
        }
        
        expectedRecords.ShouldBeEquivalentTo(actualRecords);
    }
}