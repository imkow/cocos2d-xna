using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{
    public class BugsTestBaseLayer : CCLayer
    {
        public override void OnEnter()
        {
            base.OnEnter();

            CCSize s = CCDirector.SharedDirector.WinSize;

            CCMenuItemFont.FontName = "arial";
            CCMenuItemFont.FontSize = 24;
            CCMenuItemFont pMainItem = CCMenuItemFont.Create("Back",
                backCallback);
            pMainItem.Position = new CCPoint(s.width - 50, 25);
            CCMenu pMenu = CCMenu.Create(pMainItem, null);
            pMenu.Position = new CCPoint(0, 0);
            AddChild(pMenu);
        }

        public void backCallback(CCObject pSender)
        {
            //CCDirector.SharedDirector.EnableRetinaDisplay(false);
            BugsTestScene pScene = new BugsTestScene();
            pScene.runThisTest();
        }
    }
}
