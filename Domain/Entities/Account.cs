namespace Domain.Entities;

public class Account: BaseEntity
{
    public string? AvatarUrl { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public string Phone { get; set; }
    public string? RelationshipGoal { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly Birthdate { get; set; }
    public int Gender { get; set; }
    public int? YearsOfExperience { get; set; }
    public string? Bio { get; set; }
    public decimal Balance { get; set; }
    public int Status { get; set; }
    public int Role { get; set; }
    
    public ICollection<Availability>? Availabilities { get; set; }
    public ICollection<Blog>? Blogs { get; set; }
    public ICollection<Quiz>? Quizzes { get; set; }
    public ICollection<Qualification>? Qualifications { get; set; }
    public ICollection<Transaction>? SenderTransactions { get; set; }
    public ICollection<Transaction>? ReceiverTransactions { get; set; }
    public ICollection<Request> Requests { get; set; }
    public ICollection<Session>? MemberSessions { get; set; }
    public ICollection<Session>? TherapistSessions { get; set; }
    public ICollection<Package> Packages { get; set; }
    public ICollection<Feedback> Feedbacks { get; set; }
    public ICollection<Report> Reports { get; set; }
}