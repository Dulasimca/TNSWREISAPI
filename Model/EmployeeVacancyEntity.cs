using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TNSWREISAPI.Model
{
    public class EmployeeVacancyEntity
    {
        public int Id { get; set; }
        public int DCode { get; set; }
        public int TCode { get; set; }
        public int HCode { get; set; }
        public int DesignationId { get; set; }
        public int SanctionNo { get; set; }
        public int FilledNo { get; set; }
        public int VacancyNo { get; set; }
        public string VacantDate { get; set; }
        public string Reason { get; set; }

    }
}
