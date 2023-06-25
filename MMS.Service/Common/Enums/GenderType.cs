using System.ComponentModel.DataAnnotations;

namespace MMS.Service.Common.Enums;

public enum GenderType
{
    [Display(Name = "მამრობითი")]
    Male = 1,
    [Display(Name = "მდედრობითი")]
    Female = 2
}