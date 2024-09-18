//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();



//app.Run();


using EventSourcing;
using EventSourcing.Events;

var studentDatabase = new StudentDatabase();

var studentCreated = new StudentCreated
{
    StudentId = Guid.NewGuid(),
    Email = "nika@gmail.com",
    Name = "Nika",
    DateOfBirth = new DateTime(2003, 10, 16)
};

studentDatabase.Append(studentCreated);

var studentEbrolled = new StudentEnrolled
{
    StudentId = studentCreated.StudentId,
    CourseName = "Asp.Net core"
};
studentDatabase.Append(studentEbrolled);

var studentUpdated = new StudentUpdated
{
    StudentId = studentCreated.StudentId,
    Name = "Veronika",
    Email = "nika@gmail.com"
};
studentDatabase.Append(studentUpdated);



var student = studentDatabase.GetStudent(studentCreated.StudentId);

var studentFromView = studentDatabase.GetStudentView(studentCreated.StudentId);

Console.WriteLine();