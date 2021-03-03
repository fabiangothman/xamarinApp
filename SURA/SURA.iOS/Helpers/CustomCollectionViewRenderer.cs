using System;
using SURA.iOS.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CollectionView),typeof(CustomCollectionViewRenderer))]
namespace SURA.iOS.Helpers
{
    internal sealed class CustomCollectionViewRenderer: GroupableItemsViewRenderer<GroupableItemsView, CustomItemsViewController>
    {
        protected override CustomItemsViewController CreateController(GroupableItemsView itemsView, ItemsViewLayout layout)
        {
            return new CustomItemsViewController(itemsView, layout);
        }
    }
}
