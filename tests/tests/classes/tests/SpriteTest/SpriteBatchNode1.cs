using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;
using Random = cocos2d.Random;

namespace tests
{
    public class SpriteBatchNode1 : SpriteTestDemo
    {
        public SpriteBatchNode1()
        {
            TouchEnabled = true;

            CCSpriteBatchNode BatchNode = CCSpriteBatchNode.Create("Images/grossini_dance_atlas", 50);
            AddChild(BatchNode, 0, (int)kTags.kTagSpriteBatchNode);

            CCSize s = CCDirector.SharedDirector.WinSize;
            addNewSpriteWithCoords(new CCPoint(s.width / 2, s.height / 2));
        }

        public void addNewSpriteWithCoords(CCPoint p)
        {
            CCSpriteBatchNode BatchNode = (CCSpriteBatchNode)GetChildByTag((int)kTags.kTagSpriteBatchNode);

            int idx = (int)(Random.NextDouble() * 1400 / 100);
            int x = (idx % 5) * 85;
            int y = (idx / 5) * 121;


            CCSprite sprite = CCSprite.Create(BatchNode.Texture, new CCRect(x, y, 85, 121));
            BatchNode.AddChild(sprite);

            sprite.Position = (new CCPoint(p.x, p.y));

            CCActionInterval action = null;
            float random = (float)Random.NextDouble();

            if (random < 0.20)
                action = CCScaleBy.Create(3, 2);
            else if (random < 0.40)
                action = CCRotateBy.Create(3, 360);
            else if (random < 0.60)
                action = CCBlink.Create(1, 3);
            else if (random < 0.8)
                action = CCTintBy.Create(2, 0, -255, -255);
            else
                action = CCFadeOut.Create(2);

            CCActionInterval action_back = (CCActionInterval)action.Reverse();
            CCActionInterval seq = (CCActionInterval)(CCSequence.Create(action, action_back));

            sprite.RunAction(CCRepeatForever.Create(seq));
        }

        public override void TouchesEnded(List<CCTouch> touches, CCEvent event_)
        {
            foreach (CCTouch item in touches)
            {
                if (item == null)
                {
                    break;
                }
                CCPoint location = item.LocationInView;

                location = CCDirector.SharedDirector.ConvertToGl(location);

                addNewSpriteWithCoords(location);
            }
            base.TouchesEnded(touches, event_);
        }

        public override string title()
        {
            return "SpriteBatchNode (tap screen)";
        }
    }
}
