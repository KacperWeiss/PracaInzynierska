﻿using PrzychodniaApp.DataBaseStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrzychodniaApp.UserControlers.DataRepresantations
{
    public static class DataHolderForMainWindow
    {
        public static bool IsUserLogedIn { get; set; } = false;
        public static int PatientId { get; set; } = -1;
        public static int ChosenVisitId { get; set; } = -1;
        public static DbUser User { get; set; }
    }
}
