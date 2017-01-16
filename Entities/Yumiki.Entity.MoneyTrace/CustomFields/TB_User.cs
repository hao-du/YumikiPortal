namespace Yumiki.Entity.MoneyTrace
{
    using Base;
    using System;
    using System.Collections.Generic;

    public partial class TB_User
    {
        public string FullName
        {
            get
            {
                string lastName = string.IsNullOrWhiteSpace(LastName) ? string.Empty : LastName;

                return string.IsNullOrWhiteSpace(FirstName) ? lastName : FirstName + " " + LastName;
            }
        }

        public class FieldName
        {
            public const string FullName = "FullName";
        }
    }
}
