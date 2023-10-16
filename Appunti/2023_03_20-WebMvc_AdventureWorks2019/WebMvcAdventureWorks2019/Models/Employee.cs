using System;
using System.Collections.Generic;

namespace WebMvcAdventureWorks2019.Models;


public partial class Employee
{
    public int BusinessEntityId { get; set; }

    public string NationalIdnumber { get; set; } = null!;

    public string LoginId { get; set; } = null!;

    public short? OrganizationLevel { get; set; }

    public string JobTitle { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string MaritalStatus { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public bool? SalariedFlag { get; set; }

    public short VacationHours { get; set; }

    public short SickLeaveHours { get; set; }

    public bool? CurrentFlag { get; set; }

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual Person BusinessEntity { get; set; } = null!;
}
