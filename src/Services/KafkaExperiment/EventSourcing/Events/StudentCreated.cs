namespace EventSourcing.Events;

public class StudentCreated: Event
{
    public required Guid StudentId { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required DateTime DateOfBirth { get; init; }
    public override Guid StreamId => StudentId;
}
