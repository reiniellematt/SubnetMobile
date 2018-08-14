using Prism.Events;
using SubnetMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubnetMobile.Events
{
    public class IpEntryEvent : PubSubEvent<IpQuery> { }
    public class IpResultsEvent : PubSubEvent<IpAddress> { }
}
