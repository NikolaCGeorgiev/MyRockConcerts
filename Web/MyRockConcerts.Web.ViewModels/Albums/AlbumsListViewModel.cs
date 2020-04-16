namespace MyRockConcerts.Web.ViewModels.Albums
{
    using System.Collections.Generic;

    public class AlbumsListViewModel
    {
        public IEnumerable<AlbumInfoViewModel> Albums { get; set; }
    }
}
