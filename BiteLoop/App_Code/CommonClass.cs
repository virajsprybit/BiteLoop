using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommonClass
/// </summary>
public class CommonClass
{
    public CommonClass()
    {
    }
}

#region Main Class
public class Response
{
    public Response()
    {
        StatusCode = "200";
    }
    public string success { get; set; }
    public object data { get; set; }   // ← THIS WAS MISSING

    public string message { get; set; }
    //new Add
    public string OTP { get; set; }

    public string StatusCode { get; set; }
    public Category[] Category { get; set; }
    public FoodItems[] FoodItems { get; set; }
    public Discount[] Discount { get; set; }
    public ProfilePhoto[] ProfilePhoto { get; set; }
    public SalesAdmin[] SalesAdmin { get; set; }
    public Business[] Business { get; set; }
    public BusinessProfile[] BusinessProfile { get; set; }
    //public BusinessLogin[] BusinessLogin { get; set; }
    public BusinessLogin BusinessLogin { get; set; }

    public UserLogin[] UserLogin { get; set; }
    public UserProfile[] UserProfile { get; set; }
    public Products[] Products { get; set; }
    public CartSummary[] CartSummary { get; set; }
    public OrderSummary[] OrderSummary { get; set; }
    public BusinessCurrentDayOrders[] BusinessCurrentDayOrders { get; set; }
    public BusinessReport[] BusinessReport { get; set; }
    public CMS[] CMS { get; set; }
    public Donations[] Donations { get; set; }
    public BusinessID[] BusinessID { get; set; }
    public Settings[] Settings { get; set; }
    public UserCards[] UserCards { get; set; }
    public BusinessLocation[] BusinessLocation { get; set; }
    public RestaurantTypes[] RestaurantTypes { get; set; }
    public ImpactAndRewards[] ImpactAndRewards { get; set; }
    public BusinessPickupTimes[] BusinessPickupTimes { get; set; }
    public UserRewardsPoints[] UserRewardsPoints { get; set; }
    public ContactUsSubject[] ContactUsSubject { get; set; }
    public States[] States { get; set; }
    public StateHolidays[] StateHolidays { get; set; }
    public BusinessCustomHolidays[] BusinessCustomHolidays { get; set; }
    public BusinessPublicHolidays[] BusinessPublicHolidays { get; set; }

    public BusinessRegistrationStep3[] BusinessRegistrationStep3 { get; set; }
    public BusinessRegistrationStep4[] BusinessRegistrationStep4 { get; set; }
    public BusinessRegistrationStep5[] BusinessRegistrationStep5 { get; set; }
    public AppleUserDetails[] AppleUserDetails { get; set; }
    public UserReferCodes[] UserReferCodes { get; set; }
    public UserReferCodesForCheckOut[] UserReferCodesForCheckOut { get; set; }
    public BusinessStates[] BusinessStates { get; set; }
    public SubscriptionDetails SubscriptionDetails { get; set; }
    public BusinessReportDetails BusinessReportDetails { get; set; }
    public BusinessSubscriptionStatusDetails BusinessSubscriptionStatusDetails { get; set; }
    public BusinessCards[] BusinessCards { get; set; }
    public Products[] TomorrowProducts { get; set; }
    public BusinessRatings[] BusinessRatings { get; set; }


}
public class ProductResponse
{
    public ProductResponse()
    {
        StatusCode = "200";
    }
    public string success { get; set; }
    public string message { get; set; }
    public string StatusCode { get; set; }
    public int TotalRecords { get; set; }
    public Products[] Products { get; set; }

}
#endregion
public class Settings
{
    public string Title { get; set; }
    public string Description { get; set; }
}
public class BusinessLocation
{
    public string Title { get; set; }
    public long ID { get; set; }
    public string IndicatorType { get; set; }

}
public class BusinessID
{
    public long ID { get; set; }

}
public class Category
{
    public int IndexID { get; set; }
    public int ID { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string CategoryURL { get; set; }

}
public class FoodItems
{
    public int ID { get; set; }
    public string Name { get; set; }
}
public class Donations
{
    public int ID { get; set; }
    public string Amount { get; set; }
}
public class Discount
{
    public int ID { get; set; }
    public string DiscountValue { get; set; }
}
public class ProfilePhoto
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int IsSelected { get; set; }
}
public class CMS
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}

public class SalesAdmin
{
    public long UserID { get; set; }
    public string FirtName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string SecretKey { get; set; }
    public string AuthToken { get; set; }
}
public class Business
{
    public long ID { get; set; }
    public string BusinessName { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string BusinessPhone { get; set; }
    public string Status { get; set; }
}
public class BusinessProfile
{
    public long ID { get; set; }
    public string BusinessName { get; set; }
    public string ABN { get; set; }
    public string StreetAddress { get; set; }
    public string Location { get; set; }
    public string FullName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    //public string LastName { get; set; }
    public string Suburb { get; set; }
    public string State { get; set; }
    public string BusinessPhone { get; set; }
    public string Mobile { get; set; }

    public string Password { get; set; }
    public long ProfilePhotoID { get; set; }
    public string ProfilePhoto { get; set; }
    public string[] StoreImages { get; set; }
    public string Note { get; set; }
    public string AboutUs { get; set; }
    public byte RegisterGst { get; set; }
    public byte CategoryTaxItemOrNot { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string StoreName { get; set; }
    public string Description { get; set; }
    public string BSBNo { get; set; }
    public string AccountNumber { get; set; }
    public string BankName { get; set; }
    public string AccountName { get; set; }
    public int GSTRegistered { get; set; }
    public int ReceiveMarketingMails { get; set; }
    public string Status { get; set; }
    public string PostCode { get; set; }
    public int StateID { get; set; }
    public string StateCode { get; set; }
    public string StateFullName { get; set; }
    public BusinessType[] BusinessType { get; set; }
    public DietaryTypesModel[] DietaryTypes { get; set; }

    public BusinessFoodItems[] BusinessFoodItems { get; set; }
    //public BusinessSchedule[] BusinessSchedule { get; set; }
    //public BusinessSchedulePickupTime2[] BusinessSchedulePickupTime2 { get; set; }
    //public BusinessSchedulePickupTime3[] BusinessSchedulePickupTime3 { get; set; }
    //public RestaurantTypes[] RestaurantTypes { get; set; }
    public CurrentDaySchedule CurrentDaySchedule { get; set; }
    public List<WeeklySchedule> WeeklySchedule { get; set; }


}

public class CurrentDaySchedule
{
    public long BusinessID { get; set; }
    public string CurrentDate { get; set; }
    //public string PackSize { get; set; }
    //public int NumberOfPack { get; set; }
    public string Pickup_from { get; set; }
    public string Pickup_To { get; set; }

    public decimal OriginalPrice1 { get; set; }
    public decimal DiscountedPrice1 { get; set; }
    public int NumberOfPack1 { get; set; }

    public decimal OriginalPrice2 { get; set; }
    public decimal DiscountedPrice2 { get; set; }
    public int NumberOfPack2 { get; set; }

    public decimal OriginalPrice3 { get; set; }
    public decimal DiscountedPrice3 { get; set; }
    public int NumberOfPack3 { get; set; }
    public int Repeted { get; set; }
    public string[] SelectedDays { get; set; }

    //public int InsertedID { get; set; }
}

public class CurrentDaySchedulePrices
{
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
    public int NumberOfPack { get; set; }
}


public class WeeklySchedule
{
    public int ID { get; set; }
    public long BusinessID { get; set; }
    public int DayNumber { get; set; }
    public string DayName { get; set; }
    public string CurrentDate { get; set; }
    //public string PackSize { get; set; }
    //public int NumberOfPack { get; set; }
    public decimal WOriginalPrice1 { get; set; }
    public decimal WDiscountedPrice1 { get; set; }
    public int WNumberOfPack1 { get; set; }

    public decimal WOriginalPrice2 { get; set; }
    public decimal WDiscountedPrice2 { get; set; }
    public int WNumberOfPack2 { get; set; }

    public decimal WOriginalPrice3 { get; set; }
    public decimal WDiscountedPrice3 { get; set; }
    public int WNumberOfPack3 { get; set; }
    public string Pickup_from { get; set; }
    public string Pickup_To { get; set; }
    public int Repeted { get; set; }
    public string CreatedOn { get; set; }
    public string UpdatedOn { get; set; }
}

public class WeeklySchedulePrices
{
    public decimal OriginalPrice { get; set; }
    public decimal DiscountedPrice { get; set; }
    public int NumberOfPack { get; set; }
}


public class BusinessType
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int IsSelected { get; set; }
    public string ImageName { get; set; }
}
public class BusinessFoodItems
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int IsSelected { get; set; }
}
public class BusinessSchedule
{
    public int setColor { get; set; }
    public int DayNo { get; set; }
    public string DayName { get; set; }
    public string NoOfItems { get; set; }
    public string OriginalPrice { get; set; }
    public int DiscountID { get; set; }
    public string Discount { get; set; }
    public string PickupFromTime { get; set; }
    public string PickupToTime { get; set; }
}
public class BusinessSchedulePickupTime2
{
    public int setColor { get; set; }
    public int DayNo { get; set; }
    public string DayName { get; set; }
    public string NoOfItems { get; set; }
    public string OriginalPrice { get; set; }
    public int DiscountID { get; set; }
    public string Discount { get; set; }
    public string PickupFromTime { get; set; }
    public string PickupToTime { get; set; }
}
public class BusinessSchedulePickupTime3
{
    public int setColor { get; set; }
    public int DayNo { get; set; }
    public string DayName { get; set; }
    public string NoOfItems { get; set; }
    public string OriginalPrice { get; set; }
    public int DiscountID { get; set; }
    public string Discount { get; set; }
    public string PickupFromTime { get; set; }
    public string PickupToTime { get; set; }
}
public class BusinessLogin
{
    public long UserID { get; set; }
    public string BusinessName { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public string BusinessPhone { get; set; }
    public string Mobile { get; set; }
    public string SecretKey { get; set; }
    public string AuthToken { get; set; }
    public int IsSalesAdmin { get; set; }

    public int Step { get; set; }
    public int StateID { get; set; }
    public string StateCode { get; set; }
    public string StateFullName { get; set; }
    //new
    public bool IsProfileDetails { get; set; }

    public string ABN { get; set; }
    public string StreetAddress { get; set; }
    public string Location { get; set; }
    public string ContactPersonName { get; set; }

    [JsonIgnore]
    public int GstVerified { get; set; }
    public int ReceiveMarketingEmail { get; set; }

    // ✅ Add these properties
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Note { get; set; }
    public string StoreName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Suburb { get; set; }
    public string State { get; set; }
    public string PostCode { get; set; }

}
public class UserLogin
{
    public long UserID { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string Mobile { get; set; }
    public string SecretKey { get; set; }
    public string AuthToken { get; set; }
    public string LastName { get; set; }
    public string PostCode { get; set; }
    public string ProfilePhoto { get; set; }
    public string StateName { get; set; }
    public int StateID { get; set; }
    public int IsSignupByFacebook { get; set; }

}
public class UserProfile
{
    public long ID { get; set; }
    public string FullName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Mobile { get; set; }
    public string BirthDate { get; set; }
    public string Gender { get; set; }
    public string StreetAddress { get; set; }
    public string Location { get; set; }
    public string RewardsPoints { get; set; }
    public int StateID { get; set; }
    public string StateName { get; set; }
}

public class Products
{
    public long ID { get; set; }
    public string BusinessName { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string BusinessPhone { get; set; }
    public string Mobile { get; set; }
    public string ImageFile { get; set; }
    public string StreetAddress { get; set; }
    public string Location { get; set; }
    //  public string FoodItems { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public int TotalQty { get; set; }
    public int PurchaseQty { get; set; }
    public int RemainingQty { get; set; }
    //  public string ServingLeft { get; set; }
    public string OriginalPrice { get; set; }
    public string DiscountPrice { get; set; }
    public string PickupFromTime { get; set; }
    public string PickupToTime { get; set; }
    public string Distance { get; set; }
    public int IsFavourite { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    //  public string Discount { get; set; }
    public string RestaurantTypes { get; set; }
    public string RestaurantTypesValues { get; set; }
    // public string PickupTimes { get; set; }
    public string StoreImages { get; set; }
    public string ProfilePhoto { get; set; }
    public string Rating { get; set; }
    public string DietryType { get; set; }
    public string StoreName { get; set; }
}

public class CartSummary
{
    public long UserID { get; set; }
    public long BusinessID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Qty { get; set; }
    public string OriginalPrice { get; set; }
    public string ItemPrice { get; set; }
    public string VendorGST { get; set; }
    public string Donation { get; set; }
    public string TransactionFee { get; set; }
    public string BiteloopFee { get; set; }
    public string GrandTotal { get; set; }
}


public class OrderSummary
{
    public string OrderNo { get; set; }
    public long OrderID { get; set; }
    public long BusinessID { get; set; }
    public string BusinessName { get; set; }
    public string BusinessPhone { get; set; }
    public string Mobile { get; set; }
    public string StreetAddress { get; set; }
    public string Location { get; set; }
    public long UserID { get; set; }
    public string PickupDate { get; set; }
    public string PickupFromTime { get; set; }
    public string PickupToTime { get; set; }
    public string Qty { get; set; }
    public string OriginalPrice { get; set; }
    public string ItemPrice { get; set; }
    public string RedeemPoints { get; set; }
    public string Donation { get; set; }
    public string TransactionFee { get; set; }
    public string BiteloopFee { get; set; }
    public string GrandTotal { get; set; }
    public int IsCollected { get; set; }
    public string RewardsPoints { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string PromocodeDiscountAmount { get; set; }
    public string StoreName { get; set; }
    public string PickupCode { get; set; }
    public int OrderStatusID { get; set; }
    public string OrderStatusText { get; set; }
    public string Note { get; set; }
}


public class BusinessCurrentDayOrders
{
    public long ID { get; set; }
    public string BusinessName { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public int ScheduleOn { get; set; }
    public int PickupTime { get; set; }
    public int TotalQty { get; set; }
    public int PurchaseQty { get; set; }
    public int RemainingQty { get; set; }
    public string OriginalPrice { get; set; }
    public string PickupFromTime { get; set; }
    public string PickupToTime { get; set; }
    public int DiscountID { get; set; }
    public string DiscountValue { get; set; }
    public string GrandTotalQty { get; set; }
    public string GrandPurchaseQty { get; set; }
    public string GrandRemainingQty { get; set; }
    public int IsActive { get; set; }
    public BusinessOrders[] BusinessOrders { get; set; }
}

public class BusinessOrders
{
    public Int64 ID { get; set; }
    public string OrderID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Qty { get; set; }
    public string OriginalPrice { get; set; }
    public string DiscountValue { get; set; }
    public string GrandTotal { get; set; }
    public string OrderTime { get; set; }
    public int IsCollected { get; set; }
    public string PickupFromTime { get; set; }
    public string PickupToTime { get; set; }
    public int OrderStatusID { get; set; }
    public string OrderStatusText { get; set; }
}
public class BusinessReport
{
    public string Month { get; set; }
    public string Year { get; set; }
    public string Price { get; set; }
}

public class UserCards
{
    public long ID { get; set; }
    public string CustomerID { get; set; }
    public string CardType { get; set; }
    public string LastDigits { get; set; }
    public string Mobile { get; set; }
    public string ExpiryMonth { get; set; }
    public string ExpiryYear { get; set; }
    public int IsDefault { get; set; }
}
public class RestaurantTypes
{
    public long ID { get; set; }
    public string Name { get; set; }
    //public string IconUrl { get; set; }
    public string ImageName { get; set; }
    public string Description { get; set; }
}

public class DietaryTypesModel
{
    public int ID { get; set; }
    public string DietType { get; set; }
    public string Description { get; set; }
    public string ImageName { get; set; }
    public int IsSelected { get; set; }

    public DietaryTypesModel()
    {
        IsSelected = 0;
    }
}

public class ImpactAndRewards
{
    public string YouSaved { get; set; }
    public string YouRescued { get; set; }
    public string TogetherSaved { get; set; }
    public string RedeemableAmount { get; set; }
    public string RewardsPoints { get; set; }
    public string Co2Text { get; set; }
}
public class BusinessPickupTimes
{
    public long BusinessID { get; set; }
    public long PickUpTimeID { get; set; }
    public string PickupFromTime { get; set; }
    public string PickupToTime { get; set; }
    public int RemainingQty { get; set; }
}
public class UserRewardsPoints
{
    public long UserID { get; set; }
    public string RewardsPoints { get; set; }
    public ReedemPoints[] ReedemPoints { get; set; }
}
public class ReedemPoints
{
    public long Points { get; set; }
    public string Text { get; set; }
    public string Amount { get; set; }
}

public class ContactUsSubject
{
    public int ID { get; set; }
    public string Name { get; set; }
    public bool Status { get; set; }
}
public class States
{
    public int ID { get; set; }
    public string StateName { get; set; }
}

public class StateHolidays
{
    public int StateHolidayID { get; set; }
    public int StateID { get; set; }
    public string Title { get; set; }
}

public class BusinessCustomHolidays
{
    public long BusinessHolidayID { get; set; }
    public long BusinessID { get; set; }
    public string HolidayFromDate { get; set; }
    public string HolidayToDate { get; set; }
    public string Title { get; set; }
}
public class BusinessPublicHolidays
{
    public int StateHolidayID { get; set; }
    public long BusinessID { get; set; }
    public int OnOff { get; set; }
    public string Title { get; set; }
}

public class BusinessDetailsForApproval
{
    public long UserID { get; set; }
    public string BusinessName { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string BusinessPhone { get; set; }
    public string Mobile { get; set; }
    public string SecretKey { get; set; }
    public string AuthToken { get; set; }
    public int IsSalesAdmin { get; set; }
}
public class StateSuburbs
{
    public string Suburb { get; set; }
}

public class BusinessRegistrationStep3
{
    public string BusinessName { get; set; }
    public string Location { get; set; }
    public string State { get; set; }
    public string Phone { get; set; }
    public string StoreManagerName { get; set; }
    public int MultipleStore { get; set; }
}

public class BusinessRegistrationStep4
{
    public string BusinessTypes { get; set; }
    public int BYOContainers { get; set; }
}
public class BusinessRegistrationStep5
{
    public string FoodTypes { get; set; }
}

#region Apple User Details
public class AppleUserDetails
{
    public string AppleID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

#endregion

public class UserReferCodes
{
    public string ID { get; set; }
    public string ReferCode { get; set; }
    public string ReferText { get; set; }
    public string ReferAmount { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

public class UserReferCodesForCheckOut
{
    public string UserPromoCodeID { get; set; }
    public string Code { get; set; }
    public string DiscountType { get; set; }
    public string ExpiryText { get; set; }
    public decimal Amount { get; set; }
    public decimal MinimumOrderAmount { get; set; }
}
public class BusinessStates
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string StateCode { get; set; }
}
public class SubscriptionDetails
{
    public string GST { get; set; }
    public string SubscriptionCharge { get; set; }
}
public class BusinessReportDetails
{
    public string TotalIncome { get; set; }
    public string Co2Saved { get; set; }
    public string MealsRescued { get; set; }
    public string FoodWastePrevented { get; set; }
    public string Summary { get; set; }
}
public class BusinessSubscriptionStatusDetails
{
    public string SubscriptionActive { get; set; }
    public string DeactivatedDate { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}


public class BusinessCards
{
    public long ID { get; set; }
    public string CustomerID { get; set; }
    public string CardType { get; set; }
    public string LastDigits { get; set; }
    public string ExpiryMonth { get; set; }
    public string ExpiryYear { get; set; }
    public int IsDefault { get; set; }


}
public class BusinessRatings

{

    public long ID { get; set; }

    public string BusinessName { get; set; }

    public string FullName { get; set; }

    public string EmailAddress { get; set; }

    public string AverageRating { get; set; }

    public RatingDetails[] Ratings { get; set; }

}

public class RatingDetails

{
    public long BusinessID { get; set; }

    public long UserID { get; set; }

    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string UserMobile { get; set; }

    public string Point1Rating { get; set; }

    public string Point2Rating { get; set; }

    public string Point3Rating { get; set; }

    public string Point4Rating { get; set; }

    public string AverageRating { get; set; }

    public string CreatedOn { get; set; }

}