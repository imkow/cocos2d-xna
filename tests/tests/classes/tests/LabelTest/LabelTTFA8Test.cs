﻿using cocos2d;

namespace tests
{
    internal class LabelTTFA8Test : AtlasDemo
    {
        public LabelTTFA8Test()
        {
            CCSize s = CCDirector.SharedDirector.WinSize;

            CCLayerColor layer = CCLayerColor.Create(new ccColor4B(128, 128, 128, 255));
            AddChild(layer, -10);

            // CCLabelBMFont
            CCLabelTTF label1 = CCLabelTTF.Create("Testing A8 Format", "Marker Felt", 38);
            AddChild(label1);
            label1.Color = ccTypes.ccRED;
            label1.Position = new CCPoint(s.width / 2, s.height / 2);

            CCFadeOut fadeOut = CCFadeOut.Create(2);
            CCFadeIn fadeIn = CCFadeIn.Create(2);
            CCFiniteTimeAction seq = CCSequence.Create(fadeOut, fadeIn);
            CCRepeatForever forever = CCRepeatForever.Create((CCActionInterval) seq);
            label1.RunAction(forever);
        }

        public override string title()
        {
            return "Testing A8 Format";
        }

        public override string subtitle()
        {
            return "RED label, fading In and Out in the center of the screen";
        }
    }
}