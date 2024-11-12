namespace Data.Entities.Audit
{
    public class AuditHistory
    {
        public long Id { get; set; }
        public string? Action { get; set; }
        public string TableName { get; set; }
        public DateTime ChangedTime { get; set; }
        public string? ChangingHistory { get; set; }
    }
}
