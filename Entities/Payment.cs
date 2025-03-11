using System;
using System.Collections.Generic;

namespace style.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? AppointmentId { get; set; }

    public decimal Amount { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public virtual Appointment? Appointment { get; set; }
}
