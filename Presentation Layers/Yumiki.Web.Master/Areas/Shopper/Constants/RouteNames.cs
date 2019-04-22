using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yumiki.Web.Shopper.Constants
{
    public class RouteNames
    {
        public const string DefaultMVCRoute = "Shopper_default";

        public const string FeeTypeGetAll = "FeeTypeGetAll";
        public const string FeeTypeGetByID = "FeeTypeGetByID";
        public const string FeeTypeGetByTerm = "FeeTypeGetByTerm";
        public const string FeeTypeSave = "FeeTypeSave";

        public const string ProductGetAll = "ProductGetAll";
        public const string ProductGetByTerm = "ProductGetByTerm";
        public const string ProductGetByID = "ProductGetByID";
        public const string ProductSave = "ProductSave";

        public const string AdditionalFeeGetAll = "AdditionalFeeGetAll";
        public const string AdditionalFeeGetByID = "AdditionalFeeGetByID";
        public const string AdditionalFeeSave = "AdditionalFeeSave";

        public const string ReceiptGetAll = "ReceiptGetAll";
        public const string ReceiptGetByID = "ReceiptGetByID";
        public const string ReceiptSave = "ReceiptSave";
    }
}