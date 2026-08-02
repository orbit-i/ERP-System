namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Enums;

public class JobApplication : BaseEntity
{
    public Guid JobPostingId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? ResumeUrl { get; set; }
    public string? CoverLetter { get; set; }
    public decimal? CurrentSalary { get; set; }
    public decimal? ExpectedSalary { get; set; }
    public int? NoticePeriod { get; set; }
    public JobApplicationStatus Status { get; set; } = JobApplicationStatus.Applied;
    public string? Notes { get; set; }
    public JobPosting JobPosting { get; set; } = null!;
    public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
}