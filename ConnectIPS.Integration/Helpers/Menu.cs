using MainLibrary.SAPB1;

namespace ConnectIPS.Integration.Helpers
{
    public class Menu
    {
        public static void AddMenu()
        {
            B1Helper.addMenuItem("43520", "TransactionReport", "Transaction Report");
        }

        public static void RemoveMenu()
        {
        }
    }
}
