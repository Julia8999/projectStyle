using System;
using System.Collections.Generic;

namespace style.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? ClientId { get; set; }

    public int? EmployeeId { get; set; }

    public int? ServiceId { get; set; }

    public DateOnly? AppointmentDate { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Service? Service { get; set; }
}
