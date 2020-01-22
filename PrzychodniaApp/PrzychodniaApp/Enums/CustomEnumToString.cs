namespace PrzychodniaApp.Enums
{
    public static class CustomEnumToString
    {
        public static string GetVaccineStatusText(VaccineStatus status)
        {
            switch (status)
            {
                case VaccineStatus.Optional:
                    return "Opcjonalne";

                case VaccineStatus.ObligatoryByDate:
                    return "Niewykonane - obowiązkowe";

                case VaccineStatus.ObligatoryAlreadyVaccined:
                    return "Wykonane - obowiązkowe";

                default:
                    return "";
            }
        }

        public static string GetEmailContactText(EmailContact contact)
        {
            switch (contact)
            {
                case EmailContact.Full:
                    return "Pełny kontakt";

                case EmailContact.ConfirmationsAndReminders:
                    return "Przypomnienia";

                case EmailContact.OnlyForConfirmations:
                    return "Potwierdzenia";

                case EmailContact.No:
                    return "Brak";

                default:
                    return "";

            }
        }
    }
}
