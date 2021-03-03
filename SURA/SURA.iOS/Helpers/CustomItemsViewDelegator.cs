using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace SURA.iOS.Helpers
{
    internal sealed class CustomItemsViewDelegator : ItemsViewDelegator<GroupableItemsView, CustomItemsViewController>
    {
        private ItemsViewLayout itemsViewLayout;
        private CustomItemsViewController customItemsViewController;

        public CustomItemsViewDelegator(ItemsViewLayout itemsViewLayout, CustomItemsViewController customItemsViewController)
            :base(itemsViewLayout, customItemsViewController)
        {
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            UICollectionViewCell cell = collectionView.CellForItem(indexPath);
            if(cell is ItemsViewCell itemsViewCell)
            {
                return itemsViewCell.Measure();
            }
            else
            {
                return ItemsViewLayout.EstimatedItemSize;
            }
        }
    }
}