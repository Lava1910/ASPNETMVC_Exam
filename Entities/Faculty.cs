using System;
using System.Collections.Generic;

namespace ASPNETMVC_Exam.Entities;

public partial class Faculty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}
