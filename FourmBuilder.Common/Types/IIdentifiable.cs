using System;

namespace FourmBuilder.Common.Types
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}