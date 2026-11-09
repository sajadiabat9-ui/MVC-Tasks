using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace masterpiecedatabasefirst.Models;

[Index("ChosenMajorId", Name = "IX_Students_ChosenMajorID")]
[Index("CityId", Name = "IX_Students_CityID")]
public partial class Student
{
    [Key]
    [Column("StudentID")]
    public int StudentId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    [Column("CityID")]
    public int? CityId { get; set; }

    [Column("ChosenMajorID")]
    public int? ChosenMajorId { get; set; }

    public string Role { get; set; } = null!;

    [ForeignKey("ChosenMajorId")]
    [InverseProperty("Students")]
    public virtual Major? ChosenMajor { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Students")]
    public virtual City? City { get; set; }

    [InverseProperty("Student")]
    public virtual TawjihiResult? TawjihiResult { get; set; }
}
