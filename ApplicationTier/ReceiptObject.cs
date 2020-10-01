using System;

namespace ApplicationTier
{
    /// <summary>
    ///     This class contains getters and setters for each column in the Receipts table in the Database.
    /// 
    ///     The layout of my three-layer architecture was based heavily on The Code Project's 'Three Layer Architecture in C# .NET'
    ///     by Parikshit Patel found here: https://www.codeproject.com/Articles/36847/Three-Layer-Architecture-in-C-NET-2. As such 
    ///     the DbConnection.cs, DataAccessLayer.cs, BusinessLogicLayer, 'X'Object.cs and some of the .xaml.cs classes found 
    ///     throughout the Presentation Tier use either whole or small bits of code from this walk-through. However, many aspects 
    ///     of the code found in the walk-through have either been built upon or modified heavily and will bare very little 
    ///     resemblance, if any, to the original; to suit the nature of my project, and are my own ideas. This paragraph will be 
    ///     found in the DbConnection.cs, DataAccessLayer.cs, BusinessLogicLayer.cs, XObject.cs classes as well as in all .xaml.cs 
    ///     classes found in the Presentation Tier for referencing purposes.
    /// </summary>
    public class ReceiptObject
    {
        // Constructor
        public ReceiptObject()
        {
        }

        // Getters & Setters
        public int receipt_id { get; set; }

        public DateTime date_added { get; set; }

        public string receipt_reference { get; set; }

        public int account_id { get; set; }

        public int warehouse_id { get; set; }

        public DateTime expected_date { get; set; }

        public DateTime receipted_date { get; set; }
    }
}