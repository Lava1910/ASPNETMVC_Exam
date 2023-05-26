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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=Vietanh-PC;Initial Catalog=MVC_EXAM;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__classes__72E12F1A78ABE8D1");

            entity.ToTable("classes");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__exams__3213E83FE0D73A72");

            entity.ToTable("exams");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("class_name");
            entity.Property(e => e.ExamDate)
                .HasColumnType("datetime")
                .HasColumnName("exam_date");
            entity.Property(e => e.ExamDuration).HasColumnName("exam_duration");
            entity.Property(e => e.FacultyName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("faculty_name");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject_name");

            entity.HasOne(d => d.ClassNameNavigation).WithMany(p => p.Exams)
                .HasForeignKey(d => d.ClassName)
                .HasConstraintName("FK__exams__class_nam__52593CB8");

            entity.HasOne(d => d.FacultyNameNavigation).WithMany(p => p.Exams)
                .HasForeignKey(d => d.FacultyName)
                .HasConstraintName("FK__exams__faculty_n__5441852A");

            entity.HasOne(d => d.SubjectNameNavigation).WithMany(p => p.Exams)
                .HasForeignKey(d => d.SubjectName)
                .HasConstraintName("FK__exams__subject_n__534D60F1");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__facultie__72E12F1A16CFFFCD");

            entity.ToTable("faculties");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__subjects__72E12F1A7ACA383F");

            entity.ToTable("subjects");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
