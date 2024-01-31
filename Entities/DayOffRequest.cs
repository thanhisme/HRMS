using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class DayOffRequest : BaseEntity
    {
        public enum DateOffRequestStatus
        {
            COMFIRMED,
            REJECTED,
            PENDING,
        }

        public enum DateOffRequestType
        {
            DAY_OFF,
            WORK_FROM_HOME,
        }

        public DateTime Date { get; set; }

        public DateOffRequestType Type { get; set; } = DateOffRequestType.DAY_OFF;

        public double Duration { get; set; } = 1;

        public string Reason { get; set; } = string.Empty;

        public int UsedPaidHolidays { get; set; } = 0;

        public DateOffRequestStatus Status { get; set; } = DateOffRequestStatus.PENDING;

        public string Note { get; set; } = string.Empty;

        public List<Attachment>? Attachments { get; set; } = null;

        public DateTime? StatusChangedDate { get; set; } = null;

        public string? ReasonForDecision { get; set; } = null;

        [ForeignKey("DecisionMakerId")]
        public User? DecisionMaker { get; set; } = null;

        [ForeignKey("EmployeeId")]
        public User Employee { get; set; } = new User();
    }
}
