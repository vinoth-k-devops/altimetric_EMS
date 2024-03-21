using System;
namespace EMS_Common
{
	public static class Common
	{
		public static string INSERT = "Record added successfully..";
        public static string UPDATE = "Record updated successfully..";
        public static string DELETE = "Record removed successfully..";

        public static string ElectionAddError = "Active election already exists, once it completed start a new election..";
        public static string ElectionUpdateError = "Active records only update..";

        public static string APPROVE = "Approved user {0}..";
        public static string ErrorApprove = "Internal error occurred..";
        public static string ErrorNoRecord = "No record found..";

        public static string MPSeatError = "Already State is added for Current Election..";
        public static string MPSeatGreaterError = "No of MP Seats greater than actual State seats..";

        public static string CanditureError = "Already assigned the Election City Canditure..";
        public static string CanditureExistsError = "Already assigned the Canditure to Some other..";

        public static string LogOut = "Logout successful..";

        public static string? GetPropertyValue(object source, string propertyName)
        {
            try
            {
                return GetInnerProp(source, propertyName) as string;
            }
            catch { return null; }
        }

        private static object GetInnerProp(object source, string propertyName)
        {
            if (propertyName.Contains('.'))
            {
                var propertyNames = propertyName.Split(".");
                var firstProp = propertyNames.First();
                var newSource = source.GetType().GetProperty(firstProp)!.GetValue(source, null);
                var rest = string.Join(".", propertyNames.Skip(1));

                return GetInnerProp(newSource!, rest);
            }

            return source.GetType().GetProperty(propertyName)!.GetValue(source, null)!;

        }
    }
}

