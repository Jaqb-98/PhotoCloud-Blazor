using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCloud.Data.Services.Interfaces
{
    interface IState
    {
        event Action Notify;
        public bool AreImagesSelected { get; set; }
    }
}
