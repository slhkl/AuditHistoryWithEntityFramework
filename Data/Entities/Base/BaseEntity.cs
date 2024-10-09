namespace Data.Entities.Base
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DeactivatedDate { get; set; }
    }
}
