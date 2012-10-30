﻿using cocos2d;

namespace tests
{
    internal class SpriteProgressBarVarious : SpriteDemo
    {
        private string s_pPathSister1 = "Images/grossinis_sister1";
        private string s_pPathSister2 = "Images/grossinis_sister2";

        public override void OnEnter()
        {
            base.OnEnter();

            CCSize s = CCDirector.SharedDirector.WinSize;

            CCProgressTo to = CCProgressTo.Create(2, 100);

            CCProgressTimer left = CCProgressTimer.Create(CCSprite.Create(s_pPathSister1));
            left.Type = CCProgressTimerType.Bar;

            //    Setup for a bar starting from the bottom since the midpoint is 0 for the y
            left.Midpoint = new CCPoint(0.5f, 0.5f);
            //    Setup for a vertical bar since the bar change rate is 0 for x meaning no horizontal change
            left.BarChangeRate = new CCPoint(1, 0);
            AddChild(left);
            left.Position = new CCPoint(100, s.height / 2);
            left.RunAction(CCRepeatForever.Create((CCActionInterval) to.Copy()));

            CCProgressTimer middle = CCProgressTimer.Create(CCSprite.Create(s_pPathSister2));
            middle.Type = CCProgressTimerType.Bar;
            //    Setup for a bar starting from the bottom since the midpoint is 0 for the y
            middle.Midpoint = new CCPoint(0.5f, 0.5f);
            //    Setup for a vertical bar since the bar change rate is 0 for x meaning no horizontal change
            middle.BarChangeRate = new CCPoint(1, 1);
            AddChild(middle);
            middle.Position = new CCPoint(s.width / 2, s.height / 2);
            middle.RunAction(CCRepeatForever.Create((CCActionInterval) to.Copy()));

            CCProgressTimer right = CCProgressTimer.Create(CCSprite.Create(s_pPathSister2));
            right.Type = CCProgressTimerType.Bar;
            //    Setup for a bar starting from the bottom since the midpoint is 0 for the y
            right.Midpoint = new CCPoint(0.5f, 0.5f);
            //    Setup for a vertical bar since the bar change rate is 0 for x meaning no horizontal change
            right.BarChangeRate = new CCPoint(0, 1);
            AddChild(right);
            right.Position = new CCPoint(s.width - 100, s.height / 2);
            right.RunAction(CCRepeatForever.Create((CCActionInterval) to.Copy()));
        }

        public override string subtitle()
        {
            return "ProgressTo Bar Mid";
        }
    }
}