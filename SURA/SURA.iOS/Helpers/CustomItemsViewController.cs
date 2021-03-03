using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace SURA.iOS.Helpers
{
    internal class CustomItemsViewController: GroupableItemsViewController<GroupableItemsView>
    {
        private GroupableItemsView itemsView;
        private ItemsViewLayout layout;

        public CustomItemsViewController(GroupableItemsView itemsView, ItemsViewLayout layout)
            :base(itemsView, layout)
        {
        }

        protected override UICollectionViewDelegateFlowLayout CreateDelegator()
        {
            return new CustomItemsViewDelegator(ItemsViewLayout, this as CustomItemsViewController);
        }
    }
}