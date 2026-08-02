namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class Interview : BaseEntity
{
    public Guid ApplicationId { get; set; }
    public InterviewType InterviewType { get; set; } = InterviewType.InPerson;
    public DateTime ScheduledAt { get; set; }
    public string? Location { get; set; }
    public string? MeetingLink { get; set; }
    public Guid? InterviewerId { get; set; }
    public InterviewStatus Status { get; set; } = InterviewStatus.Scheduled;
    public string? Feedback { get; set; }
    public int? Rating { get; set; } // 1-5
    public JobApplication Application { get; set; } = null!;
    public User? Interviewer { get; set; }
}