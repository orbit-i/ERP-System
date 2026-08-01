namespace OrbitERP.Domain.Entities.HR;

using OrbitERP.Domain.Common;
using OrbitERP.Domain.Entities.Administration;
using OrbitERP.Domain.Enums;

public class JobPosting : BaseEntity
{
    public Guid CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? DesignationId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string? Requirements { get; set; }
    public EmploymentType JobType { get; set; } = EmploymentType.FullTime;
    public string? Location { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public int Vacancies { get; set; } = 1;
    public DateOnly PostedDate { get; set; }
    public DateOnly? ClosingDate { get; set; }
    public JobPostingStatus Status { get; set; } = JobPostingStatus.Draft;
    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public Department? Department { get; set; }
    public Designation? Designation { get; set; }
    public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
}