using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public class PrescriptionPDFRequiredData
    {
        public string MedicalWorkerName { get; set; }
        public string PacientName { get; set; }
        public string PacientPESEL { get; set; }
        public string Medicines { get; set; }
        public string MedicinesPayment { get; set; }
    }
}
