using Amazon.DynamoDBv2.DataModel;

namespace Report.Processor
{
    [DynamoDBTable("users")]
    public class User
    {
        [DynamoDBHashKey]
        public string? Id { get; set; }

        [DynamoDBProperty]
        public string? Name { get; set; }

        [DynamoDBProperty]
        public string? Mobile { get; set; }

        [DynamoDBProperty]
        public string? Email { get; set; }

    }
}
