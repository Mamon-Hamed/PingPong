namespace PingPong.Domain.Repositories;

public interface IAuditQuery:ISortQuery,IPagedQuery
{
    DateTime? MinCreatedAt { get; set; }
    DateTime? MaxCreatedAt { get; set; }
    DateTime? MinUpdatedAt { get; set; }
    DateTime? MaxUpdatedAt { get; set; }
    string? CreatedBy { get; set; }
    string? UpdatedBy { get; set; }
    string? CreatedByName { get; set; }
    string? UpdatedByName { get; set; }
}