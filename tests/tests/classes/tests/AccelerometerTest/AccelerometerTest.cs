﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using cocos2d;

namespace tests
{
    public class AccelerometerTest : CCLayer
    {

        protected CCSprite m_pBall;
        protected double m_fLastTime;

        public override void DidAccelerate(CCAcceleration pAccelerationValue)
        {
            CCDirector pDir = CCDirector.SharedDirector;
            CCSize winSize = pDir.WinSize;

            /*FIXME: Testing on the Nexus S sometimes m_pBall is NULL */
            if (m_pBall == null)
            {
                return;
            }

            CCSize ballSize = m_pBall.ContentSize;

            CCPoint ptNow = m_pBall.Position;
            CCPoint ptTemp = pDir.ConvertToUi(ptNow);

            ptTemp.x += (float) pAccelerationValue.x * 9.81f;
            ptTemp.y -= (float) pAccelerationValue.y * 9.81f;

            CCPoint ptNext = pDir.ConvertToGl(ptTemp);
            ptNext.x = MathHelper.Clamp(ptNext.x, (ballSize.width / 2.0f), (winSize.width - ballSize.width / 2.0f));
            ptNext.y = MathHelper.Clamp(ptNext.y, (ballSize.height / 2.0f), (winSize.height - ballSize.height / 2.0f));
            m_pBall.Position = ptNext;
        }

        public virtual string title()
        {
            return "AccelerometerTest";
        }

        public override void OnEnter()
        {
            base.OnEnter();

            AccelerometerEnabled = true;

            CCSize s = CCDirector.SharedDirector.WinSize;

            CCLabelTTF label = CCLabelTTF.Create(title(), "Arial", 32);
            AddChild(label, 1);
            label.Position = new CCPoint(s.width / 2, s.height - 50);

            m_pBall = CCSprite.Create("Images/ball");
            m_pBall.Position = new CCPoint(s.width / 2, s.height / 2);
            AddChild(m_pBall);
        }
    }

    public class AccelerometerTestScene : TestScene
    {
        protected override void NextTestCase()
        {
        }
        protected override void PreviousTestCase()
        {
        }
        protected override void RestTestCase()
        {
        }
        public override void runThisTest()
        {
            CCLayer pLayer = new AccelerometerTest();
            AddChild(pLayer);

            CCDirector.SharedDirector.ReplaceScene(this);
        }
    }
}
