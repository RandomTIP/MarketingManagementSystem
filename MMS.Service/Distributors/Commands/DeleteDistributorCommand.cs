using MediatR;
using MMS.Core.Commands;

namespace MMS.Service.Distributors.Commands
{
    public class DeleteDistributorCommand : DeleteCommand, IRequest
    {
    }
}
