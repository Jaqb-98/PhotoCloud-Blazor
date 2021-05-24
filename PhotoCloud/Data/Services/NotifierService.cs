using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoCloud.Data.Services
{
    public class NotifierService
    {
        private readonly List<string> photoIds = new List<string>();
        public IReadOnlyList<string> PhotoIds => photoIds;

        public event Func<Task> Notify;

        public async void AddToList(ICollection<string> values)
        {
            foreach (var item in values)
            {
                photoIds.Add(item);
            }

            if (Notify != null)
                await Notify?.Invoke();
        }

        public List<string> GetSelectedPhotos()
        {
            var photos = photoIds.ToList();
            photoIds.Clear();
            return photos;
        }


    }
}
