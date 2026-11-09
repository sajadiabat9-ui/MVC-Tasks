using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace masterpiecedatabasefirst.Models;

public partial class Major
{
    [Key]
    [Column("MajorID")]
    public int MajorId { get; set; }

    public string MajorName { get; set; } = null!;

    public string MajorNameAr { get; set; } = null!;

    public string Description { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal MinAverage { get; set; }

    public int StudyYears { get; set; }

    [InverseProperty("ChosenMajor")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
