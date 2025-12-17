using Grpc.Core;
using System;
using System.Threading.Tasks;
using static MessServiceApp.SenderIDService;

namespace MessServiceApp
{
    public class SenderIDService : SenderID.SenderIDBase
    {
        public delegate long _OnUsernameReceived(string username);
        public event _OnUsernameReceived? OnUsernameReceived;
        public override async Task<Response> GetID(Request request, ServerCallContext context)
        {
            long id;
            if (OnUsernameReceived == null)
                throw new InvalidOperationException("No handler for OnUsernameReceived event.");
            else
                id = OnUsernameReceived(request.Name);

            return new Response { Id = id };
        }
    }
}