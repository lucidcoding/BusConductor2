using System;

namespace BusConductor.Domain.Enumerations
{
    [Flags]
    public enum BusDayBookingStatus
    {
        Free = 0,
        PendingAllDay = 1,
        ConfirmedAllDay = 2,
        PendingAm = 4,
        PendingPm = 8,
        ConfirmedAm = 16,
        ConfirmedPm = 32
    }
}
