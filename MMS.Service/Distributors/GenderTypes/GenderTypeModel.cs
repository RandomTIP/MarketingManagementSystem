using MMS.Core.Domain;

namespace MMS.Service.Distributors.GenderTypes
{
    public class GenderTypeModel : CommonEntity
    {
        private GenderTypeModel(){}
        public GenderTypeModel(string name)
        {
            Name = name;
        }
    }
}
