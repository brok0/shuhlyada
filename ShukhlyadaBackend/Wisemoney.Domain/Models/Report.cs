﻿using Shukhlyada.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shukhlyada.Domain.Models
{
    public class Report : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public Guid? PostId { get; set; }
        public Post ReportedPost { get; set; }

        public Guid? ChannelId { get; set; }
        public Channel ReportedChannel { get; set; }

        public string Reason { get; set; }
        public ReportType Type { get; set; }

    }

    public enum ReportType
    {
        Racism,
        //...
    }
}
