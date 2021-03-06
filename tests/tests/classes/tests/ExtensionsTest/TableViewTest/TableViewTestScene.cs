using System;
using cocos2d;

namespace tests.Extensions
{
	public class TableViewTestLayer : CCLayer, CCTableViewDataSource, CCTableViewDelegate
	{
		public static void runTableViewTest()
		{
			var pScene = CCScene.Create();
			var pLayer = new TableViewTestLayer();
			pLayer.Init();
			pScene.AddChild(pLayer);
			CCDirector.SharedDirector.ReplaceScene(pScene);
		}

		public override bool Init()
		{
			if (!base.Init())
				return false;

			var winSize = CCDirector.SharedDirector.WinSize;

			var tableView = CCTableView.Create(this, new CCSize(250, 60));
			tableView.Direction = CCScrollViewDirection.Horizontal;
			tableView.Position = new CCPoint(20,winSize.height/2-30);
			tableView.Delegate = this;
			this.AddChild(tableView);
			tableView.ReloadData();

			tableView = CCTableView.Create(this, new CCSize(60, 280));
			tableView.Direction = CCScrollViewDirection.Vertical;
			tableView.Position = new CCPoint(winSize.width - 150, winSize.height/2 - 120);
			tableView.Delegate = this;
			tableView.VerticalFillOrder = CCTableViewVerticalFillOrder.FillTopDown;
			this.AddChild(tableView);
			tableView.ReloadData();

			// Back Menu
			var itemBack = CCMenuItemFont.Create("Back", toExtensionsMainLayer);
			itemBack.Position = new CCPoint(winSize.width - 50, 25);
			var menuBack = CCMenu.Create(itemBack);
			menuBack.Position = CCPoint.Zero;
			AddChild(menuBack);

			return true;
		}
   
		public void toExtensionsMainLayer(CCObject sender)
		{
			var pScene = new ExtensionsTestScene();
			pScene.runThisTest();
		}

		//CREATE_FUNC(TableViewTestLayer);
    
		public virtual void scrollViewDidScroll(CCScrollView view)
		{
		}

		public virtual void scrollViewDidZoom(CCScrollView view)
		{
		}

		public virtual void TableCellTouched(CCTableView table, CCTableViewCell cell)
		{
			CCLog.Log("cell touched at index: %i", cell.getIdx());
		}

		public virtual CCSize CellSizeForTable(CCTableView table)
		{
			return new CCSize(60, 60);
		}

		public virtual CCTableViewCell TableCellAtIndex(CCTableView table, int idx)
		{
			string str = idx.ToString();
			var cell = table.DequeueCell();
			
			if (cell == null) {
				cell = new CustomTableViewCell();
				var sprite = CCSprite.Create("Images/Icon");
				sprite.AnchorPoint = CCPoint.Zero;
				sprite.Position = new CCPoint(0, 0);
				cell.AddChild(sprite);

				var label = CCLabelTTF.Create(str, "Helvetica", 20.0f);
				label.Position = CCPoint.Zero;
				label.AnchorPoint = CCPoint.Zero;
				label.Tag = 123;
				cell.AddChild(label);
			}
			else
			{
				var label = (CCLabelTTF)cell.GetChildByTag(123);
				label.SetString(str);
			}


			return cell;
		}

		public virtual int NumberOfCellsInTableView(CCTableView table)
		{
			return 20;
		}
	}
}