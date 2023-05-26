using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVC_Exam.Entities;

public partial class MvcExamContext : DbContext
{
    public MvcExamContext()
    {
    }

    public MvcExamContext(DbContextOptions<MvcExamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=Vietanh-PC;Initial Catalog=MVC_EXAM;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__classes__3213E83FFB3994CE");

            entity.ToTable("classes");

            entity.HasIndex(e => e.Name, "UQ__classes__72E12F1B92991E28").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__exams__3213E83F8E16C57B");

            entity.ToTable("exams");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassId).HasColumnName("class_id");
            entity.Property(e => e.ExamDate)
                .HasColumnType("datetime")
                .HasColumnName("exam_date");
            entity.Property(e => e.ExamDuration).HasColumnName("exam_duration");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");

            entity.HasOne(d => d.Class).WithMany(p => p.Exams)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__exams__class_id__47DBAE45");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Exams)
                .HasForeignKey(d => d.FacultyId)
                .HasConstraintName("FK__exams__faculty_i__49C3F6B7");

            entity.HasOne(d => d.Subject).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__exams__subject_i__48CFD27E");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__facultie__3213E83F9C29CFB4");

            entity.ToTable("faculties");

            entity.HasIndex(e => e.Name, "UQ__facultie__72E12F1B19F890FB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__subjects__3213E83F15B718B8");

            entity.ToTable("subjects");

            entity.HasIndex(e => e.Name, "UQ__subjects__72E12F1B15944AD6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
