using System;
using System.Collections.Generic;

namespace ASPNETMVC_Exam.Entities;

public partial class Exam
{
    public int Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime ExamDate { get; set; }

    public int ExamDuration { get; set; }

    public int? ClassId { get; set; }

    public int? SubjectId { get; set; }

    public int? FacultyId { get; set; }

    public string? Status { get; set; }

    public virtual Class? Class { get; set; }

    public virtual Faculty? Faculty { get; set; }

    public virtual Subject? Subject { get; set; }
}
