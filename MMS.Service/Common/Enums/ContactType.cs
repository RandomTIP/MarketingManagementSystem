using System.ComponentModel.DataAnnotations;

namespace MMS.Service.Common.Enums;

public enum ContactType
{
    [Display(Name = "ტელეფონი")]
    Telephone = 1,
    [Display(Name = "მობილური")]
    Mobile = 2,
    [Display(Name = "იმეილი")]
    Email = 3,
    [Display(Name = "ფაქსი")]
    Fax = 4
}