using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace masterpiecedatabasefirst.Models;

[Index("CityId", Name = "IX_Universities_CityID")]
public partial class University
{
    [Key]
    [Column("UniversityID")]
    public int UniversityId { get; set; }

    public string UniversityName { get; set; } = null!;

    public string UniversityNameAr { get; set; } = null!;

    [Column("CityID")]
    public int CityId { get; set; }

    public string UniType { get; set; } = null!;

    public int FoundedYear { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Universities")]
    public virtual City City { get; set; } = null!;
}
