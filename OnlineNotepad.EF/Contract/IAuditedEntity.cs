
using System;

namespace OnlineNotepad.EF.Contract
{
    public interface IAuditedEntity
    {
        DateTime CreatedOn { get; set; }
    }
}
