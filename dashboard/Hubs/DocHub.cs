using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace dashboard.Hubs
{
    public class DocHub : Hub
    {
        public void Register(int datasourceId)
        {
            Groups.Add(Context.ConnectionId, datasourceId.ToString());
        }

        public void Unregister(int datasourceId)
        {
            Groups.Remove(Context.ConnectionId, datasourceId.ToString());
        }

    }
}