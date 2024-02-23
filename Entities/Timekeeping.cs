using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Timekeeping : BaseEntity
    {
        [ForeignKey("EmployeeId")]
        public virtual User? Employee { get; set; } = null;
        public DateTime Date { get; set; }
        public string? Shift { get; set; }
        public string? TimeIn { get; set; }
        public string? TimeOut { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
    }
}
