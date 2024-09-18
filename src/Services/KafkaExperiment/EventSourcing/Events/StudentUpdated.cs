namespace EventSourcing.Events;

public class StudentUpdated: Event
{
    public required Guid StudentId { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public override Guid StreamId => StudentId;
}
