using PhotoCloud.Data.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCloud.Data.Services
{
    public class State : IState
    {
        public event Action Notify;

        bool imagesAreSelected = false;

        public bool AreImagesSelected
        {
            get => imagesAreSelected;

            set
            {
                if (imagesAreSelected != value)
                {
                    imagesAreSelected = value;

                    if (Notify != null)
                    {
                        Notify?.Invoke();
                    }
                }
            }
        }



    }
}
