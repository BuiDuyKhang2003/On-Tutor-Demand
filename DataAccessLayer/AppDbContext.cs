using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;


namespace DataAccessLayer;

public partial class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<RentalService> RentalServices { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<TutorArea> TutorAreas { get; set; }
    public DbSet<TutorGrade> TutorGrades { get; set; }
    public DbSet<TutorSubject> TutorSubjects { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<TutorRegistration> TutorRegistrations { get; set; }
    public DbSet<BookingSchedule> Bookings { get; set; }


    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", true, true)
                  .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Account Configuration
        modelBuilder.Entity<Account>()
            .HasOne(a => a.Tutor)
            .WithOne(t => t.Account)
            .HasForeignKey<Tutor>(t => t.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Account>()
            .HasMany(a => a.InitiatedConversations)
            .WithOne(c => c.Initiator)
            .HasForeignKey(c => c.InitiatorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Account>()
            .HasMany(a => a.ReceivedConversations)
            .WithOne(c => c.Receiver)
            .HasForeignKey(c => c.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        // ChatMessage Configuration
        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.Conversation)
            .WithMany(c => c.ChatMessages)
            .HasForeignKey(cm => cm.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.Sender)
            .WithMany(a => a.SentMessages)
            .HasForeignKey(cm => cm.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        // Feedback Configuration
        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.Account)
            .WithMany(a => a.Feedbacks)
            .HasForeignKey(f => f.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.RentalService)
            .WithMany(rs => rs.Feedbacks)
            .HasForeignKey(f => f.RentalServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        // RentalService Configuration
        modelBuilder.Entity<RentalService>()
            .HasOne(rs => rs.Tutor)
            .WithMany(t => t.RentalServices)
            .HasForeignKey(rs => rs.TutorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RentalService>()
            .HasMany(rs => rs.Schedules)
            .WithOne(s => s.RentalService)
            .HasForeignKey(s => s.RentalServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Schedule Configuration
        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.RentalService)
            .WithMany(rs => rs.Schedules)
            .HasForeignKey(s => s.RentalServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        // BookingSchedule Configuration
        modelBuilder.Entity<BookingSchedule>()
            .HasOne(b => b.Account)
            .WithMany(a => a.Bookings)
            .HasForeignKey(b => b.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BookingSchedule>()
            .HasOne(bs => bs.Schedule)
            .WithOne(s => s.Booking)
            .HasForeignKey<BookingSchedule>(bs => bs.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);

        // Tutor Configuration
        modelBuilder.Entity<Tutor>()
            .HasOne(t => t.Account)
            .WithOne(a => a.Tutor)
            .HasForeignKey<Tutor>(t => t.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tutor>()
            .HasMany(t => t.Videos)
            .WithOne(v => v.Tutor)
            .HasForeignKey(v => v.TutorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tutor>()
            .HasMany(t => t.TutorSubjects)
            .WithOne(ts => ts.Tutor)
            .HasForeignKey(ts => ts.TutorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tutor>()
            .HasMany(t => t.TutorGrades)
            .WithOne(tg => tg.Tutor)
            .HasForeignKey(tg => tg.TutorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Tutor>()
            .HasMany(t => t.TutorAreas)
            .WithOne(ta => ta.Tutor)
            .HasForeignKey(ta => ta.TutorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Video Configuration
        modelBuilder.Entity<Video>()
            .HasOne(v => v.Tutor)
            .WithMany(t => t.Videos)
            .HasForeignKey(v => v.TutorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Subject Configuration
        modelBuilder.Entity<Subject>()
            .HasMany(s => s.TutorSubjects)
            .WithOne(ts => ts.Subject)
            .HasForeignKey(ts => ts.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        // TutorSubject Configuration
        modelBuilder.Entity<TutorSubject>()
            .HasOne(ts => ts.Subject)
            .WithMany(s => s.TutorSubjects)
            .HasForeignKey(ts => ts.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TutorSubject>()
            .HasOne(ts => ts.Tutor)
            .WithMany(t => t.TutorSubjects)
            .HasForeignKey(ts => ts.TutorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Grade Configuration
        modelBuilder.Entity<Grade>()
            .HasMany(g => g.TutorGrades)
            .WithOne(tg => tg.Grade)
            .HasForeignKey(tg => tg.GradeId)
            .OnDelete(DeleteBehavior.Restrict);

        // TutorGrade Configuration
        modelBuilder.Entity<TutorGrade>()
            .HasOne(tg => tg.Tutor)
            .WithMany(t => t.TutorGrades)
            .HasForeignKey(tg => tg.TutorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TutorGrade>()
            .HasOne(tg => tg.Grade)
            .WithMany(g => g.TutorGrades)
            .HasForeignKey(tg => tg.GradeId)
            .OnDelete(DeleteBehavior.Restrict);

        // District Configuration
        modelBuilder.Entity<District>()
            .HasMany(d => d.TutorAreas)
            .WithOne(ta => ta.District)
            .HasForeignKey(ta => ta.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        // TutorArea Configuration
        modelBuilder.Entity<TutorArea>()
            .HasOne(ta => ta.District)
            .WithMany(d => d.TutorAreas)
            .HasForeignKey(ta => ta.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TutorArea>()
            .HasOne(ta => ta.Tutor)
            .WithMany(t => t.TutorAreas)
            .HasForeignKey(ta => ta.TutorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Conversation Configuration
        modelBuilder.Entity<Conversation>()
            .HasOne(c => c.Initiator)
            .WithMany(a => a.InitiatedConversations)
            .HasForeignKey(c => c.InitiatorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Conversation>()
            .HasOne(c => c.Receiver)
            .WithMany(a => a.ReceivedConversations)
            .HasForeignKey(c => c.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
