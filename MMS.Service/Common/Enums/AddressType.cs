using System.ComponentModel.DataAnnotations;

namespace MMS.Service.Common.Enums;

public enum AddressType
{
    [Display(Name = "ფაქტიური")]
    Factual = 1,
    [Display(Name = "რეგისტრირებული")]
    Registered = 2
}