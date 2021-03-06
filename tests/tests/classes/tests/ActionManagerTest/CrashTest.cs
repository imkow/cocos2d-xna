using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cocos2d;

namespace tests
{

    public class CrashTest : ActionManagerTest
    {
        string s_pPathGrossini = "Images/grossini";

        public override string title()
        {
            return "Test 1. Should not crash";
        }

        public override void OnEnter()
        {
            base.OnEnter();

            CCSprite child = CCSprite.Create(s_pPathGrossini);
            child.Position = (new CCPoint(200, 200));
            AddChild(child, 1);

            //Sum of all action's duration is 1.5 second.
            child.RunAction(CCRotateBy.Create(1.5f, 90));
            child.RunAction(CCSequence.Create(
                                                    CCDelayTime.Create(1.4f),
                                                    CCFadeOut.Create(1.1f))
                            );

            //After 1.5 second, self will be removed.
            RunAction(CCSequence.Create(
                                            CCDelayTime.Create(1.4f),
                                            CCCallFunc.Create((removeThis)))
                     );
        }

        public void removeThis()
        {
            m_pParent.RemoveChild(this, true);

            nextCallback(this);
        }
    }
}
