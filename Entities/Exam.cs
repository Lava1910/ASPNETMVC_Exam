using System;
using System.Collections.Generic;

namespace ASPNETMVC_Exam.Entities;

public partial class Exam
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime ExamDate { get; set; }

    public int ExamDuration { get; set; }

    public string? ClassName { get; set; }

    public string? SubjectName { get; set; }

    public string? FacultyName { get; set; }

    public string? Status { get; set; }

    public virtual Class? ClassNameNavigation { get; set; }

    public virtual Faculty? FacultyNameNavigation { get; set; }

    public virtual Subject? SubjectNameNavigation { get; set; }
}
