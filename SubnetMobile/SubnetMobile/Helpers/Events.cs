using Prism.Events;
using SubnetMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubnetMobile.Helpers
{
    public class IpEntryEvent : PubSubEvent<IpQuery> { }
    public class IpResultsEvent : PubSubEvent<IpResults> { }
}
