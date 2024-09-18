using EventSourcing.Events;

namespace EventSourcing;

public class Student
{
    public  Guid StudentId { get; set; }
    public  string Name { get; set; }
    public  string Email { get; set; }
    public  DateTime DateOfBirth { get; set; }
    public List<string> EnrolledCourses { get; set; } = new();

    private void Apply(StudentCreated studentCreated)
    {
        StudentId = studentCreated.StudentId;
        Name = studentCreated.Name;
        Email = studentCreated.Email;
        DateOfBirth = studentCreated.DateOfBirth;
    }

    private void Apply(StudentUpdated studentUpdated)
    {
        Name = studentUpdated.Name;
        Email = studentUpdated.Email;
    }

    private void Apply(StudentEnrolled enrolled)
    {
        if (!EnrolledCourses.Contains(enrolled.CourseName))
        {
            EnrolledCourses.Add(enrolled.CourseName);
        }
    }

    private void Apply(StudentUnEnrolled unEnrolled)
    {
        if (EnrolledCourses.Contains(unEnrolled.CourseName))
        {
            EnrolledCourses.Remove(unEnrolled.CourseName);
        }
    }

    public void Apply(Event @event)
    {
        switch(@event)
        {
            case StudentCreated studentCreated:
                Apply(studentCreated);
                break;
            case StudentUpdated studentUpdated:
                Apply(studentUpdated);
                break;
            case StudentEnrolled enrolled:
                Apply(enrolled);
                break;
            case StudentUnEnrolled unenrolled:
                Apply(unenrolled);
                break;
        }
    }
}
