namespace EventSourcing.Events;

public class StudentEnrolled: Event
{
    public required Guid StudentId { get; init; }
    public required string CourseName { get; set; }
    public override Guid StreamId => StudentId;
}
