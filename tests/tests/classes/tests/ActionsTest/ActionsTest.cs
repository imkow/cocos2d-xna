﻿using System;
using System.Collections.Generic;
using cocos2d;

namespace tests
{
    public enum ActionTest
    {
        ACTION_MANUAL_LAYER = 0,
        ACTION_MOVE_LAYER,
        ACTION_SCALE_LAYER,
        ACTION_ROTATE_LAYER,
        ACTION_SKEW_LAYER,
        ACTION_SKEWROTATE_LAYER,
        ACTION_JUMP_LAYER,
        ACTION_CARDINALSPLINE_LAYER,
        ACTION_CATMULLROM_LAYER,
        ACTION_BEZIER_LAYER,
        ACTION_BLINK_LAYER,
        ACTION_FADE_LAYER,
        ACTION_TINT_LAYER,
        ACTION_ANIMATE_LAYER,
        ACTION_SEQUENCE_LAYER,
        ACTION_SEQUENCE2_LAYER,
        ACTION_SPAWN_LAYER,
        ACTION_REVERSE,
        ACTION_DELAYTIME_LAYER,
        ACTION_REPEAT_LAYER,
        ACTION_REPEATEFOREVER_LAYER,
        ACTION_ROTATETOREPEATE_LAYER,
        ACTION_ROTATEJERK_LAYER,
        ACTION_CALLFUNC_LAYER,
        ACTION_CALLFUNCND_LAYER,
        ACTION_REVERSESEQUENCE_LAYER,
        ACTION_REVERSESEQUENCE2_LAYER,
        ACTION_ORBIT_LAYER,
        ACTION_FLLOW_LAYER,
        ACTION_TARGETED_LAYER,
        PAUSERESUMEACTIONS_LAYER,
        ACTION_ISSUE1305_LAYER,
        ACTION_ISSUE1305_2_LAYER,
        ACTION_ISSUE1288_LAYER,
        ACTION_ISSUE1288_2_LAYER,
        ACTION_ISSUE1327_LAYER,
        ACTION_LAYER_COUNT,
    };


    // the class inherit from TestScene
    // every Scene each test used must inherit from TestScene,
    // make sure the test have the menu item for back to main menu
    public class ActionsTestScene : TestScene
    {
        public static int s_nActionIdx = -1;

        public static CCLayer CreateLayer(int nIndex)
        {
            CCLayer pLayer = null;

            switch (nIndex)
            {
                case (int) ActionTest.ACTION_MANUAL_LAYER:
                    pLayer = new ActionManual();
                    break;
                case (int) ActionTest.ACTION_MOVE_LAYER:
                    pLayer = new ActionMove();
                    break;
                case (int) ActionTest.ACTION_SCALE_LAYER:
                    pLayer = new ActionScale();
                    break;
                case (int) ActionTest.ACTION_ROTATE_LAYER:
                    pLayer = new ActionRotate();
                    break;
                case (int) ActionTest.ACTION_SKEW_LAYER:
                    pLayer = new ActionSkew();
                    break;
                case (int) ActionTest.ACTION_SKEWROTATE_LAYER:
                    pLayer = new ActionSkewRotateScale();
                    break;
                case (int) ActionTest.ACTION_JUMP_LAYER:
                    pLayer = new ActionJump();
                    break;
                case (int) ActionTest.ACTION_BEZIER_LAYER:
                    pLayer = new ActionBezier();
                    break;
                case (int) ActionTest.ACTION_BLINK_LAYER:
                    pLayer = new ActionBlink();
                    break;
                case (int) ActionTest.ACTION_FADE_LAYER:
                    pLayer = new ActionFade();
                    break;
                case (int) ActionTest.ACTION_TINT_LAYER:
                    pLayer = new ActionTint();
                    break;
                case (int) ActionTest.ACTION_ANIMATE_LAYER:
                    pLayer = new ActionAnimate();
                    break;
                case (int) ActionTest.ACTION_SEQUENCE_LAYER:
                    pLayer = new ActionSequence();
                    break;
                case (int) ActionTest.ACTION_SEQUENCE2_LAYER:
                    pLayer = new ActionSequence2();
                    break;
                case (int) ActionTest.ACTION_SPAWN_LAYER:
                    pLayer = new ActionSpawn();
                    break;
                case (int) ActionTest.ACTION_REVERSE:
                    pLayer = new ActionReverse();
                    break;
                case (int) ActionTest.ACTION_DELAYTIME_LAYER:
                    pLayer = new ActionDelayTime();
                    break;
                case (int) ActionTest.ACTION_REPEAT_LAYER:
                    pLayer = new ActionRepeat();
                    break;
                case (int) ActionTest.ACTION_REPEATEFOREVER_LAYER:
                    pLayer = new ActionRepeatForever();
                    break;
                case (int) ActionTest.ACTION_ROTATETOREPEATE_LAYER:
                    pLayer = new ActionRotateToRepeat();
                    break;
                case (int) ActionTest.ACTION_ROTATEJERK_LAYER:
                    pLayer = new ActionRotateJerk();
                    break;
                case (int) ActionTest.ACTION_CALLFUNC_LAYER:
                    pLayer = new ActionCallFunc();
                    break;
                case (int) ActionTest.ACTION_CALLFUNCND_LAYER:
                    pLayer = new ActionCallFuncND();
                    break;
                case (int) ActionTest.ACTION_REVERSESEQUENCE_LAYER:
                    pLayer = new ActionReverseSequence();
                    break;
                case (int) ActionTest.ACTION_REVERSESEQUENCE2_LAYER:
                    pLayer = new ActionReverseSequence2();
                    break;
                case (int) ActionTest.ACTION_ORBIT_LAYER:
                    pLayer = new ActionOrbit();
                    break;
                case (int) ActionTest.ACTION_FLLOW_LAYER:
                    pLayer = new ActionFollow();
                    break;
                case (int) ActionTest.ACTION_TARGETED_LAYER:
                    pLayer = new ActionTargeted();
                    break;
                case (int) ActionTest.ACTION_ISSUE1305_LAYER:
                    pLayer = new Issue1305();
                    break;
                case (int) ActionTest.ACTION_ISSUE1305_2_LAYER:
                    pLayer = new Issue1305_2();
                    break;
                case (int) ActionTest.ACTION_ISSUE1288_LAYER:
                    pLayer = new Issue1288();
                    break;
                case (int) ActionTest.ACTION_ISSUE1288_2_LAYER:
                    pLayer = new Issue1288_2();
                    break;
                case (int) ActionTest.ACTION_ISSUE1327_LAYER:
                    pLayer = new Issue1327();
                    break;
                case (int) ActionTest.ACTION_CARDINALSPLINE_LAYER:
                    pLayer = new ActionCardinalSpline();
                    break;
                case (int) ActionTest.ACTION_CATMULLROM_LAYER:
                    pLayer = new ActionCatmullRom();
                    break;
                case (int) ActionTest.PAUSERESUMEACTIONS_LAYER:
                    pLayer = new PauseResumeActions();
                    break;
                default:
                    break;
            }

            return pLayer;
        }
        protected override void NextTestCase()
        {
            NextAction();
        }
        protected override void PreviousTestCase()
        {
            BackAction();
        }
        protected override void RestTestCase()
        {
            RestartAction();
        }

        public static CCLayer NextAction()
        {
            ++s_nActionIdx;
            s_nActionIdx = s_nActionIdx % (int) ActionTest.ACTION_LAYER_COUNT;

            var pLayer = CreateLayer(s_nActionIdx);

            return pLayer;
        }

        public static CCLayer BackAction()
        {
            --s_nActionIdx;
            if (s_nActionIdx < 0)
                s_nActionIdx += (int) ActionTest.ACTION_LAYER_COUNT;

            var pLayer = CreateLayer(s_nActionIdx);

            return pLayer;
        }

        public static CCLayer RestartAction()
        {
            var pLayer = CreateLayer(s_nActionIdx);

            return pLayer;
        }


        public override void runThisTest()
        {
            s_nActionIdx = -1;
            AddChild(NextAction());

            CCDirector.SharedDirector.ReplaceScene(this);
        }
    }

    public class ActionsDemo : CCLayer
    {
        protected CCSprite m_grossini;
        protected CCSprite m_kathia;
        protected CCSprite m_tamara;

        public virtual string title()
        {
            return "ActionsTest";
        }

        public virtual string subtitle()
        {
            return "";
        }

        public override void OnEnter()
        {
            base.OnEnter();

            // Or you can create an sprite using a filename. only PNG is supported now. Probably TIFF too
            m_grossini = CCSprite.Create(TestResource.s_pPathGrossini);

            m_tamara = CCSprite.Create(TestResource.s_pPathSister1);

            m_kathia = CCSprite.Create(TestResource.s_pPathSister2);

            AddChild(m_grossini, 1);
            AddChild(m_tamara, 2);
            AddChild(m_kathia, 3);

            var s = CCDirector.SharedDirector.WinSize;

            m_grossini.Position = new CCPoint(s.width / 2, s.height / 3);
            m_tamara.Position = new CCPoint(s.width / 2, 2 * s.height / 3);
            m_kathia.Position = new CCPoint(s.width / 2, s.height / 2);

            // add title and subtitle
            var str = title();
            var pTitle = str;
            var label = CCLabelTTF.Create(pTitle, "arial", 18);
            AddChild(label, 1);
            label.Position = new CCPoint(s.width / 2, s.height - 30);

            var strSubtitle = subtitle();
            if (! strSubtitle.Equals(""))
            {
                var l = CCLabelTTF.Create(strSubtitle, "arial", 22);
                AddChild(l, 1);
                l.Position = new CCPoint(s.width / 2, s.height - 60);
            }

            // add menu
            var item1 = CCMenuItemImage.Create(TestResource.s_pPathB1, TestResource.s_pPathB2, backCallback);
            var item2 = CCMenuItemImage.Create(TestResource.s_pPathR1, TestResource.s_pPathR2, restartCallback);
            var item3 = CCMenuItemImage.Create(TestResource.s_pPathF1, TestResource.s_pPathF2, nextCallback);

            var menu = CCMenu.Create(item1, item2, item3);

            menu.Position = new CCPoint(0, 0);
            item1.Position = new CCPoint(s.width / 2 - 100, 30);
            item2.Position = new CCPoint(s.width / 2, 30);
            item3.Position = new CCPoint(s.width / 2 + 100, 30);

            AddChild(menu, 1);
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public void restartCallback(CCObject pSender)
        {
            var s = new ActionsTestScene();
            s.AddChild(ActionsTestScene.RestartAction());
            CCDirector.SharedDirector.ReplaceScene(s);
        }

        public void nextCallback(CCObject pSender)
        {
            var s = new ActionsTestScene();
            s.AddChild(ActionsTestScene.NextAction());
            CCDirector.SharedDirector.ReplaceScene(s);
        }

        public void backCallback(CCObject pSender)
        {
            var s = new ActionsTestScene();
            s.AddChild(ActionsTestScene.BackAction());
            CCDirector.SharedDirector.ReplaceScene(s);
        }

        public void centerSprites(uint numberOfSprites)
        {
            var s = CCDirector.SharedDirector.WinSize;

            if (numberOfSprites == 0)
            {
                m_tamara.Visible = false;
                m_kathia.Visible = false;
                m_grossini.Visible = false;
            }
            else if (numberOfSprites == 1)
            {
                m_tamara.Visible = false;
                m_kathia.Visible = false;
                m_grossini.Position = new CCPoint(s.width / 2, s.height / 2);
            }
            else if (numberOfSprites == 2)
            {
                m_kathia.Position = new CCPoint(s.width / 3, s.height / 2);
                m_tamara.Position = new CCPoint(2 * s.width / 3, s.height / 2);
                m_grossini.Visible = false;
            }
            else if (numberOfSprites == 3)
            {
                m_grossini.Position = new CCPoint(s.width / 2, s.height / 2);
                m_tamara.Position = new CCPoint(s.width / 4, s.height / 2);
                m_kathia.Position = new CCPoint(3 * s.width / 4, s.height / 2);
            }
        }

        public void alignSpritesLeft(uint numberOfSprites)
        {
            var s = CCDirector.SharedDirector.WinSize;

            if (numberOfSprites == 1)
            {
                m_tamara.Visible = false;
                m_kathia.Visible = false;
                m_grossini.Position = new CCPoint(60, s.height / 2);
            }
            else if (numberOfSprites == 2)
            {
                m_kathia.Position = new CCPoint(60, s.height / 3);
                m_tamara.Position = new CCPoint(60, 2 * s.height / 3);
                m_grossini.Visible = false;
            }
            else if (numberOfSprites == 3)
            {
                m_grossini.Position = new CCPoint(60, s.height / 2);
                m_tamara.Position = new CCPoint(60, 2 * s.height / 3);
                m_kathia.Position = new CCPoint(60, s.height / 3);
            }
        }
    };

    public class ActionManual : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            var s = CCDirector.SharedDirector.WinSize;

            m_tamara.ScaleX = 2.5f;
            m_tamara.ScaleY = -1.0f;
            m_tamara.Position = new CCPoint(100, 70);
            m_tamara.Opacity = 128;

            m_grossini.Rotation = 120;
            m_grossini.Position = new CCPoint(s.width / 2, s.height / 2);
            m_grossini.Color = new ccColor3B(255, 0, 0);

            m_kathia.Position = new CCPoint(s.width - 100, s.height / 2);
            m_kathia.Color = new ccColor3B(0, 0, 255); // ccTypes.ccBLUE
        }

        public override string subtitle()
        {
            return "Manual Transformation";
        }
    };

    public class ActionMove : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(3);

            var s = CCDirector.SharedDirector.WinSize;

            var actionTo = CCMoveTo.Create(2, new CCPoint(s.width - 40, s.height - 40));
            var actionBy = CCMoveBy.Create(2, new CCPoint(80, 80));
            var actionByBack = actionBy.Reverse();

            m_tamara.RunAction(actionTo);
            m_grossini.RunAction(CCSequence.Create(actionBy, actionByBack));
            m_kathia.RunAction(CCMoveTo.Create(1, new CCPoint(40, 40)));
        }

        public override string subtitle()
        {
            return "MoveTo / MoveBy";
        }
    }

    public class ActionScale : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(3);

            var actionTo = CCScaleTo.Create(2, 0.5f);
            var actionBy = CCScaleBy.Create(2, 1, 10);
            var actionBy2 = CCScaleBy.Create(2, 5f, 1.0f);
            var actionByBack = actionBy.Reverse();

            m_grossini.RunAction(actionTo);
            m_tamara.RunAction(CCSequence.Create(actionBy, actionBy.Reverse()));
            m_kathia.RunAction(CCSequence.Create(actionBy2, actionBy2.Reverse()));
        }

        public override string subtitle()
        {
            return "ScaleTo / ScaleBy";
        }
    };

    public class ActionSkew : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(3);

            var actionTo = CCSkewTo.Create(2, 37.2f, -37.2f);
            var actionToBack = CCSkewTo.Create(2, 0, 0);
            var actionBy = CCSkewBy.Create(2, 0.0f, -90.0f);
            var actionBy2 = CCSkewBy.Create(2, 45.0f, 45.0f);
            var actionByBack = actionBy.Reverse();

            m_tamara.RunAction(CCSequence.Create(actionTo, actionToBack));
            m_grossini.RunAction(CCSequence.Create(actionBy, actionByBack));

            m_kathia.RunAction(CCSequence.Create(actionBy2, actionBy2.Reverse()));
        }

        public override string subtitle()
        {
            return "SkewTo / SkewBy";
        }
    };

    public class ActionSkewRotateScale : ActionsDemo
    {
        private const float markrside = 10.0f;

        public override void OnEnter()
        {
            // todo: CCLayerColor hasn't been implemented

            base.OnEnter();

            m_tamara.RemoveFromParentAndCleanup(true);
            m_grossini.RemoveFromParentAndCleanup(true);
            m_kathia.RemoveFromParentAndCleanup(true);

            var boxSize = new CCSize(100.0f, 100.0f);

            var box = CCLayerColor.Create(new ccColor4B(255, 255, 0, 255));
            box.AnchorPoint = new CCPoint(0, 0);
            box.Position = new CCPoint(190, 110);
            box.ContentSize = boxSize;

            var uL = CCLayerColor.Create(new ccColor4B(255, 0, 0, 255));
            box.AddChild(uL);
            uL.ContentSize = new CCSize(markrside, markrside);
            uL.Position = new CCPoint(0.0f, boxSize.height - markrside);
            uL.AnchorPoint = new CCPoint(0, 0);

            var uR = CCLayerColor.Create(new ccColor4B(0, 0, 255, 255));
            box.AddChild(uR);
            uR.ContentSize = new CCSize(markrside, markrside);
            uR.Position = new CCPoint(boxSize.width - markrside, boxSize.height - markrside);
            uR.AnchorPoint = new CCPoint(0, 0);
            AddChild(box);

            var actionTo = CCSkewTo.Create(2, 0.0f, 2.0f);
            var rotateTo = CCRotateTo.Create(2, 61.0f);
            var actionScaleTo = CCScaleTo.Create(2, -0.44f, 0.47f);

            var actionScaleToBack = CCScaleTo.Create(2, 1.0f, 1.0f);
            var rotateToBack = CCRotateTo.Create(2, 0);
            var actionToBack = CCSkewTo.Create(2, 0, 0);

            box.RunAction(CCSequence.Create(actionTo, actionToBack));
            box.RunAction(CCSequence.Create(rotateTo, rotateToBack));
            box.RunAction(CCSequence.Create(actionScaleTo, actionScaleToBack));
        }

        public override string subtitle()
        {
            return "Skew + Rotate + Scale";
        }
    };

    public class ActionRotate : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(3);

            var actionTo = CCRotateTo.Create(2, 45);
            var actionTo2 = CCRotateTo.Create(2, -45);
            var actionTo0 = CCRotateTo.Create(2, 0);
            m_tamara.RunAction(CCSequence.Create(actionTo, actionTo0));

            var actionBy = CCRotateBy.Create(2, 360);
            var actionByBack = actionBy.Reverse();
            m_grossini.RunAction(CCSequence.Create(actionBy, actionByBack));

            // m_kathia->runAction( CCSequence::actions(actionTo2, actionTo0->copy()->autorelease(), NULL));
            m_kathia.RunAction(CCSequence.Create(actionTo2, (CCActionInterval) actionTo0.Copy()));
        }

        public override string subtitle()
        {
            return "RotateTo / RotateBy";
        }
    };

    public class ActionJump : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(3);

            var actionTo = CCJumpBy.Create(2, new CCPoint(300, 300), 50, 4);
            var actionBy = CCJumpBy.Create(2, new CCPoint(300, 0), 50, 4);
            var actionUp = CCJumpBy.Create(2, new CCPoint(0, 0), 80, 4);
            var actionByBack = actionBy.Reverse();

            m_tamara.RunAction(actionTo);
            m_grossini.RunAction(CCSequence.Create(actionBy, actionByBack));
            m_kathia.RunAction(CCRepeatForever.Create(actionUp));
        }

        public override string subtitle()
        {
            return "JumpTo / JumpBy";
        }
    };

    public class ActionBezier : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            var s = CCDirector.SharedDirector.WinSize;

            //
            // startPosition can be any coordinate, but since the movement
            // is relative to the Bezier curve, make it (0,0)
            //

            centerSprites(3);

            // sprite 1
            ccBezierConfig bezier;
            bezier.ControlPoint1 = new CCPoint(0, s.height / 2);
            bezier.ControlPoint2 = new CCPoint(300, -s.height / 2);
            bezier.EndPosition = new CCPoint(300, 100);

            var bezierForward = CCBezierBy.Create(3, bezier);
            var bezierBack = bezierForward.Reverse();
            var rep = CCRepeatForever.Create((CCActionInterval)CCSequence.Create(bezierForward, bezierBack));


            // sprite 2
            m_tamara.Position = new CCPoint(80, 160);
            ccBezierConfig bezier2;
            bezier2.ControlPoint1 = new CCPoint(100, s.height / 2);
            bezier2.ControlPoint2 = new CCPoint(200, -s.height / 2);
            bezier2.EndPosition = new CCPoint(240, 160);

            var bezierTo1 = CCBezierTo.Create(2, bezier2);

            // sprite 3
            m_kathia.Position = new CCPoint(400, 160);
            var bezierTo2 = CCBezierTo.Create(2, bezier2);

            m_grossini.RunAction(rep);
            m_tamara.RunAction(bezierTo1);
            m_kathia.RunAction(bezierTo2);
        }

        public override string subtitle()
        {
            return "BezierBy / BezierTo";
        }
    };

    public class ActionBlink : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(2);

            var action1 = CCBlink.Create(2, 10);
            var action2 = CCBlink.Create(2, 5);

            m_tamara.RunAction(action1);
            m_kathia.RunAction(action2);
        }

        public override string subtitle()
        {
            return "Blink";
        }
    };

    public class ActionFade : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(2);

            m_tamara.Opacity = 0;
            var action1 = CCFadeIn.Create(1.0f);
            var action1Back = action1.Reverse();

            var action2 = CCFadeOut.Create(1.0f);
            var action2Back = action2.Reverse();

            m_tamara.RunAction(CCSequence.Create(action1, action1Back));
            m_kathia.RunAction(CCSequence.Create(action2, action2Back));
        }

        public override string subtitle()
        {
            return "FadeIn / FadeOut";
        }
    };

    public class ActionTint : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(2);

            var action1 = CCTintTo.Create(2, 255, 0, 255);
            var action2 = CCTintBy.Create(2, -127, -255, -127);
            var action2Back = action2.Reverse();

            m_tamara.RunAction(action1);
            m_kathia.RunAction(CCSequence.Create(action2, action2Back));
        }

        public override string subtitle()
        {
            return "TintTo / TintBy";
        }
    };

    public class ActionAnimate : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(3);

            //
            // Manual animation
            //
            var animation = CCAnimation.Create();
            for (var i = 1; i < 15; i++)
            {
                var szName = String.Format("Images/grossini_dance_{0:00}", i);
                animation.AddSpriteFrameWithFileName(szName);
            }

            // should last 2.8 seconds. And there are 14 frames.
            animation.DelayPerUnit = 2.8f / 14.0f;
            animation.RestoreOriginalFrame = true;

            var action = CCAnimate.Create(animation);
            m_grossini.RunAction(CCSequence.Create(action, action.Reverse()));

            //
            // File animation
            //
            // With 2 loops and reverse
            var cache = CCAnimationCache.SharedAnimationCache;
            cache.AddAnimationsWithFile("animations/animations-2.plist");
            var animation2 = cache.AnimationByName("dance_1");

            var action2 = CCAnimate.Create(animation2);
            m_tamara.RunAction(CCSequence.Create(action2, action2.Reverse()));

            // TODO:
            //     observer_ = [[NSNotificationCenter defaultCenter] addObserverForName:CCAnimationFrameDisplayedNotification object:nil queue:nil usingBlock:^(NSNotification* notification) {
            // 
            //         NSDictionary *userInfo = [notification userInfo];
            //         NSLog(@"object %@ with data %@", [notification object], userInfo );
            //     }];


            //
            // File animation
            //
            // with 4 loops
            var animation3 = (CCAnimation) animation2.Copy();
            animation3.Loops = 4;


            var action3 = CCAnimate.Create(animation3);
            m_kathia.RunAction(action3);
        }

        public override string title()
        {
            return "Animation";
        }

        public override string subtitle()
        {
            return "Center: Manual animation. Border: using file format animation";
        }
    }

    public class ActionSequence : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            alignSpritesLeft(1);

            var action = CCSequence.Create(
                CCMoveBy.Create(2, new CCPoint(240, 0)),
                CCRotateBy.Create(2, 540));

            m_grossini.RunAction(action);
        }

        public override string subtitle()
        {
            return "Sequence: Move + Rotate";
        }
    };

    public class ActionSequence2 : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            alignSpritesLeft(1);

            m_grossini.Visible = false;

            var action = CCSequence.Create(
                CCPlace.Create(new CCPoint(200, 200)),
                CCShow.Create(),
                CCMoveBy.Create(1, new CCPoint(100, 0)),
                CCCallFunc.Create(callback1),
                CCCallFuncN.Create(callback2),
                CCCallFuncND.Create(callback3, 0xbebabeba));

            m_grossini.RunAction(action);
        }

        public void callback1()
        {
            var s = CCDirector.SharedDirector.WinSize;
            var label = CCLabelTTF.Create("callback 1 called", "arial", 16);
            label.Position = new CCPoint(s.width / 4 * 1, s.height / 2);

            AddChild(label);
        }

        public void callback2(CCNode sender)
        {
            var s = CCDirector.SharedDirector.WinSize;
            var label = CCLabelTTF.Create("callback 2 called", "arial", 16);
            label.Position = new CCPoint(s.width / 4 * 2, s.height / 2);

            AddChild(label);
        }

        public void callback3(CCNode sender, object data)
        {
            var s = CCDirector.SharedDirector.WinSize;
            var label = CCLabelTTF.Create("callback 3 called", "arial", 16);
            label.Position = new CCPoint(s.width / 4 * 3, s.height / 2);

            AddChild(label);
        }

        public override string subtitle()
        {
            return "Sequence of InstantActions";
        }
    };

    public class ActionCallFunc : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(3);

            var action = CCSequence.Create(
                CCMoveBy.Create(2, new CCPoint(200, 0)),
                CCCallFunc.Create(callback1));

            var action2 = CCSequence.Create(
                CCScaleBy.Create(2, 2),
                CCFadeOut.Create(2),
                CCCallFuncN.Create(callback2));

            var action3 = CCSequence.Create(
                CCRotateBy.Create(3, 360),
                CCFadeOut.Create(2),
                CCCallFuncND.Create(callback3, 0xbebabeba));

            m_grossini.RunAction(action);
            m_tamara.RunAction(action2);
            m_kathia.RunAction(action3);
        }


        public void callback1()
        {
            var s = CCDirector.SharedDirector.WinSize;
            var label = CCLabelTTF.Create("callback 1 called", "arial", 16);
            label.Position = new CCPoint(s.width / 4 * 1, s.height / 2);

            AddChild(label);
        }

        public void callback2(CCNode pSender)
        {
            var s = CCDirector.SharedDirector.WinSize;
            var label = CCLabelTTF.Create("callback 2 called", "arial", 16);
            label.Position = new CCPoint(s.width / 4 * 2, s.height / 2);

            AddChild(label);
        }

        public void callback3(CCNode target, object data)
        {
            var s = CCDirector.SharedDirector.WinSize;
            var label = CCLabelTTF.Create("callback 3 called", "arial", 16);
            label.Position = new CCPoint(s.width / 4 * 3, s.height / 2);
            AddChild(label);
        }

        public override string subtitle()
        {
            return "Callbacks: CallFunc and friends";
        }
    };

    public class ActionCallFuncND : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(1);

            var action = CCSequence.Create(
                CCMoveBy.Create(2.0f, new CCPoint(200, 0)),
                CCCallFuncND.Create(removeFromParentAndCleanup, true)
                );

            m_grossini.RunAction(action);
        }

        public override string title()
        {
            return "CallFuncND + auto remove";
        }

        public override string subtitle()
        {
            return "CallFuncND + removeFromParentAndCleanup. Grossini dissapears in 2s";
        }

        private void removeFromParentAndCleanup(CCNode pSender, object data)
        {
            var bCleanUp = (bool) data;
            m_grossini.RemoveFromParentAndCleanup(bCleanUp);
        }
    }

    public class ActionSpawn : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            alignSpritesLeft(1);

            var action = CCSpawn.Create(
                CCJumpBy.Create(2, new CCPoint(300, 0), 50, 4),
                CCRotateBy.Create(2, 720));

            m_grossini.RunAction(action);
        }

        public override string subtitle()
        {
            return "Spawn: Jump + Rotate";
        }
    };

    public class ActionRepeatForever : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(1);

            var action = CCSequence.Create(
                CCDelayTime.Create(1),
                CCCallFuncN.Create(repeatForever));

            m_grossini.RunAction(action);
        }

        public void repeatForever(CCNode pSender)
        {
            var repeat = CCRepeatForever.Create(CCRotateBy.Create(1.0f, 360));

            pSender.RunAction(repeat);
        }

        public override string subtitle()
        {
            return "CallFuncN + RepeatForever";
        }
    };

    public class ActionRotateToRepeat : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(2);

            var act1 = CCRotateTo.Create(1, 90);
            var act2 = CCRotateTo.Create(1, 0);
            var seq = (CCSequence.Create(act1, act2));
            var rep1 = CCRepeatForever.Create((CCActionInterval)seq);
            var rep2 = CCRepeat.Create((CCFiniteTimeAction)(seq.Copy()), 10);

            m_tamara.RunAction(rep1);
            m_kathia.RunAction(rep2);
        }

        public override string subtitle()
        {
            return "Repeat/RepeatForever + RotateTo";
        }
    };

    public class ActionRotateJerk : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(2);

            var seq = CCSequence.Create(
                CCRotateTo.Create(0.5f, -20),
                CCRotateTo.Create(0.5f, 20));

            var rep1 = CCRepeat.Create(seq, 10);
            var rep2 = CCRepeatForever.Create((CCActionInterval)(seq.Copy()));

            m_tamara.RunAction(rep1);
            m_kathia.RunAction(rep2);
        }

        public override string subtitle()
        {
            return "RepeatForever / Repeat + Rotate";
        }
    };

    public class ActionReverse : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            alignSpritesLeft(1);

            var jump = CCJumpBy.Create(2, new CCPoint(300, 0), 50, 4);
            var action = CCSequence.Create(jump, jump.Reverse());

            m_grossini.RunAction(action);
        }

        public override string subtitle()
        {
            return "Reverse an action";
        }
    };

    public class ActionDelayTime : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            alignSpritesLeft(1);

            var move = CCMoveBy.Create(1, new CCPoint(150, 0));
            var action = CCSequence.Create(move, CCDelayTime.Create(2), move);

            m_grossini.RunAction(action);
        }

        public override string subtitle()
        {
            return "DelayTime: m + delay + m";
        }
    };

    public class ActionReverseSequence : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            alignSpritesLeft(1);

            var move1 = CCMoveBy.Create(1, new CCPoint(250, 0));
            var move2 = CCMoveBy.Create(1, new CCPoint(0, 50));
            var seq = CCSequence.Create(move1, move2, move1.Reverse());
            var action = CCSequence.Create(seq, seq.Reverse());

            m_grossini.RunAction(action);
        }

        public override string subtitle()
        {
            return "Reverse a sequence";
        }
    };

    public class ActionReverseSequence2 : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            alignSpritesLeft(2);

            // Test:
            //   Sequence should work both with IntervalAction and InstantActions
            var move1 = CCMoveBy.Create(1, new CCPoint(250, 0));
            var move2 = CCMoveBy.Create(1, new CCPoint(0, 50));
            var tog1 = new CCToggleVisibility();
            var tog2 = new CCToggleVisibility();
            var seq = CCSequence.Create(move1, tog1, move2, tog2, move1.Reverse());
            var action = CCRepeat.Create((CCSequence.Create(seq, seq.Reverse())), 3);

            // Test:
            //   Also test that the reverse of Hide is Show, and vice-versa
            m_kathia.RunAction(action);

            var move_tamara = CCMoveBy.Create(1, new CCPoint(100, 0));
            var move_tamara2 = CCMoveBy.Create(1, new CCPoint(50, 0));
            var hide = new CCHide();
            var seq_tamara = CCSequence.Create(move_tamara, hide, move_tamara2);
            var seq_back = seq_tamara.Reverse();
            m_tamara.RunAction(CCSequence.Create(seq_tamara, seq_back));
        }

        public override string subtitle()
        {
            return "Reverse sequence 2";
        }
    }

    public class ActionRepeat : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            alignSpritesLeft(2);

            var a1 = CCMoveBy.Create(1, new CCPoint(150, 0));
            var action1 = CCRepeat.Create(
                CCSequence.Create(CCPlace.Create(new CCPoint(60, 60)), a1),
                3);
            var action2 = CCRepeatForever.Create(
                (CCSequence.Create((CCActionInterval) (a1.Copy()), a1.Reverse()))
                );

            m_kathia.RunAction(action1);
            m_tamara.RunAction(action2);
        }

        public override string subtitle()
        {
            return "Repeat / RepeatForever actions";
        }
    };

    public class ActionOrbit : ActionsDemo
    {
        public override void OnEnter()
        {
            // todo : CCOrbitCamera hasn't been implement

            base.OnEnter();

            centerSprites(3);

            var orbit1 = CCOrbitCamera.Create(2, 1, 0, 0, 180, 0, 0);
            var action1 = CCSequence.Create(orbit1,orbit1.Reverse());

            var orbit2 = CCOrbitCamera.Create(2, 1, 0, 0, 180, -45, 0);
            var action2 = CCSequence.Create(orbit2, orbit2.Reverse());

            var orbit3 = CCOrbitCamera.Create(2, 1, 0, 0, 180, 90, 0);
            var action3 = CCSequence.Create(orbit3, orbit3.Reverse());

            m_kathia.RunAction(CCRepeatForever.Create(action1));
            m_tamara.RunAction(CCRepeatForever.Create(action2));
            m_grossini.RunAction(CCRepeatForever.Create(action3));

            var move = CCMoveBy.Create(3, new CCPoint(100, -100));
            var move_back = move.Reverse();
            var seq = CCSequence.Create(move, move_back);
            var rfe = CCRepeatForever.Create(seq);
            m_kathia.RunAction(rfe);
            m_tamara.RunAction((CCAction) (rfe.Copy()));
            m_grossini.RunAction((CCAction) (rfe.Copy()));
        }

        public override string subtitle()
        {
            return "OrbitCamera action";
        }
    };

    public class ActionFollow : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(1);
            var s = CCDirector.SharedDirector.WinSize;

            m_grossini.Position = new CCPoint(-200, s.height / 2);
            var move = CCMoveBy.Create(2, new CCPoint(s.width * 3, 0));
            var move_back = move.Reverse();
            var seq = CCSequence.Create(move, move_back);
            var rep = CCRepeatForever.Create(seq);

            m_grossini.RunAction(rep);

            RunAction(CCFollow.Create(m_grossini, new CCRect(0, 0, s.width * 2 - 100, s.height)));
        }

        public override string subtitle()
        {
            return "Follow action";
        }
    }


    public class ActionCardinalSpline : ActionsDemo
    {
        private readonly List<CCPoint> m_pArray = new List<CCPoint>();

        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(2);

            var s = CCDirector.SharedDirector.WinSize;

            m_pArray.Add(new CCPoint(0, 0));
            m_pArray.Add(new CCPoint(s.width / 2 - 30, 0));
            m_pArray.Add(new CCPoint(s.width / 2 - 30, s.height - 80));
            m_pArray.Add(new CCPoint(0, s.height - 80));
            m_pArray.Add(new CCPoint(0, 0));

            //
            // sprite 1 (By)
            //
            // Spline with no tension (tension==0)
            //

            var action = CCCardinalSplineBy.Create(3, m_pArray, 0);
            var reverse = action.Reverse();

            var seq = CCSequence.Create(action, reverse);

            m_tamara.Position = new CCPoint(50, 50);
            m_tamara.RunAction(seq);

            //
            // sprite 2 (By)
            //
            // Spline with high tension (tension==1)
            //

            var action2 = CCCardinalSplineBy.Create(3, m_pArray, 1);
            var reverse2 = action2.Reverse();

            var seq2 = CCSequence.Create(action2, reverse2);

            m_kathia.SetPosition(s.width / 2, 50);
            m_kathia.RunAction(seq2);
        }

        public override void Draw()
        {
            base.Draw();

            // move to 50,50 since the "by" path will start at 50,50
            DrawManager.PushMatrix();
            DrawManager.Translate(50, 50, 0);
            CCDrawingPrimitives.DrawCardinalSpline(m_pArray, 0, 100);
            DrawManager.PopMatrix();

            var s = CCDirector.SharedDirector.WinSize;

            DrawManager.PushMatrix();
            DrawManager.Translate(s.width / 2, 50, 0);
            CCDrawingPrimitives.DrawCardinalSpline(m_pArray, 1, 100);
            DrawManager.PopMatrix();
        }

        public override string title()
        {
            return "CardinalSplineBy / CardinalSplineAt";
        }

        public override string subtitle()
        {
            return "Cardinal Spline paths. Testing different tensions for one array";
        }
    }

    public class ActionCatmullRom : ActionsDemo
    {
        private readonly List<CCPoint> m_pArray = new List<CCPoint>();
        private readonly List<CCPoint> m_pArray2 = new List<CCPoint>();

        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(2);

            var s = CCDirector.SharedDirector.WinSize;

            //
            // sprite 1 (By)
            //
            // startPosition can be any coordinate, but since the movement
            // is relative to the Catmull Rom curve, it is better to start with (0,0).
            //

            m_tamara.Position = new CCPoint(50, 50);

            m_pArray.Clear();

            m_pArray.Add(new CCPoint(0, 0));
            m_pArray.Add(new CCPoint(80, 80));
            m_pArray.Add(new CCPoint(s.width - 80, 80));
            m_pArray.Add(new CCPoint(s.width - 80, s.height - 80));
            m_pArray.Add(new CCPoint(80, s.height - 80));
            m_pArray.Add(new CCPoint(80, 80));
            m_pArray.Add(new CCPoint(s.width / 2, s.height / 2));

            var action = CCCatmullRomBy.Create(3, m_pArray);
            var reverse = action.Reverse();

            CCFiniteTimeAction seq = CCSequence.Create(action, reverse);

            m_tamara.RunAction(seq);

            //
            // sprite 2 (To)
            //
            // The startPosition is not important here, because it uses a "To" action.
            // The initial position will be the 1st point of the Catmull Rom path
            //    

            m_pArray2.Clear();

            m_pArray2.Add(new CCPoint(s.width / 2, 30));
            m_pArray2.Add(new CCPoint(s.width - 80, 30));
            m_pArray2.Add(new CCPoint(s.width - 80, s.height - 80));
            m_pArray2.Add(new CCPoint(s.width / 2, s.height - 80));
            m_pArray2.Add(new CCPoint(s.width / 2, 30));

            var action2 = CCCatmullRomTo.Create(3, m_pArray2);
            var reverse2 = action2.Reverse();

            CCFiniteTimeAction seq2 = CCSequence.Create(action2, reverse2);

            m_kathia.RunAction(seq2);
        }

        public override void Draw()
        {
            base.Draw();

            // move to 50,50 since the "by" path will start at 50,50
            DrawManager.PushMatrix();
            DrawManager.Translate(50, 50, 0);
            CCDrawingPrimitives.DrawCatmullRom(m_pArray, 50);
            DrawManager.PopMatrix();

            CCDrawingPrimitives.DrawCatmullRom(m_pArray2, 50);
        }

        public override string title()
        {
            return "CatmullRomBy / CatmullRomTo";
        }

        public override string subtitle()
        {
            return "Catmull Rom spline paths. Testing reverse too";
        }
    }

    public class ActionTargeted : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(2);

            var jump1 = CCJumpBy.Create(2, CCPoint.Zero, 100, 3);
            var jump2 = (CCJumpBy) jump1.Copy();
            var rot1 = CCRotateBy.Create(1, 360);
            var rot2 = (CCRotateBy) rot1.Copy();

            var t1 = CCTargetedAction.Create(m_kathia, jump2);
            var t2 = CCTargetedAction.Create(m_kathia, rot2);


            var seq = CCSequence.Create(jump1, t1, rot1, t2);
            var always = CCRepeatForever.Create(seq);

            m_tamara.RunAction(always);
        }

        public override string title()
        {
            return "ActionTargeted";
        }

        public override string subtitle()
        {
            return "Action that runs on another target. Useful for sequences";
        }
    }


    public class Issue1305 : ActionsDemo
    {
        private CCSprite m_pSpriteTmp;

        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(0);

            m_pSpriteTmp = CCSprite.Create("Images/grossini");
            /* c++ can't support block, so we use CCCallFuncN instead.
            [spriteTmp_ runAction:[CCCallBlockN actionWithBlock:^(CCNode* node) {
                NSLog(@"This message SHALL ONLY appear when the sprite is added to the scene, NOT BEFORE");
            }] ];
            */

            m_pSpriteTmp.RunAction(CCCallFuncN.Create(log));

            ScheduleOnce(addSprite, 2);
        }

        private void log(CCNode pSender)
        {
            CCLog.Log("This message SHALL ONLY appear when the sprite is added to the scene, NOT BEFORE");
        }

        private void addSprite(float dt)
        {
            m_pSpriteTmp.Position = new CCPoint(250, 250);
            AddChild(m_pSpriteTmp);
        }

        public override string title()
        {
            return "Issue 1305";
        }

        public override string subtitle()
        {
            return "In two seconds you should see a message on the console. NOT BEFORE.";
        }
    }

    public class Issue1305_2 : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(0);

            var spr = CCSprite.Create("Images/grossini");
            spr.Position = new CCPoint(200, 200);
            AddChild(spr);

            var act1 = CCMoveBy.Create(2, new CCPoint(0, 100));

            var act2 = CCCallFunc.Create(log1);
            var act3 = CCMoveBy.Create(2, new CCPoint(0, -100));
            var act4 = CCCallFunc.Create(log2);
            var act5 = CCMoveBy.Create(2, new CCPoint(100, -100));
            var act6 = CCCallFunc.Create(log3);
            var act7 = CCMoveBy.Create(2, new CCPoint(-100, 0));
            var act8 = CCCallFunc.Create(log4);

            var actF = CCSequence.Create(act1, act2, act3, act4, act5, act6, act7, act8);

            CCDirector.SharedDirector.ActionManager.AddAction(actF, spr, false);
        }

        private void log4()
        {
            CCLog.Log("4st block");
        }

        private void log3()
        {
            CCLog.Log("3st block");
        }

        private void log2()
        {
            CCLog.Log("2st block");
        }

        private void log1()
        {
            CCLog.Log("1st block");
        }


        public override string title()
        {
            return "Issue 1305 #2";
        }

        public override string subtitle()
        {
            return "See console. You should only see one message for each block";
        }
    }

    public class Issue1288 : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(0);

            var spr = CCSprite.Create("Images/grossini");
            spr.Position = new CCPoint(100, 100);
            AddChild(spr);

            var act1 = CCMoveBy.Create(0.5f, new CCPoint(100, 0));
            var act2 = (CCMoveBy) act1.Reverse();
            var act3 = CCSequence.Create(act1, act2);
            var act4 = CCRepeat.Create(act3, 2);

            spr.RunAction(act4);
        }

        public override string title()
        {
            return "Issue 1288";
        }

        public override string subtitle()
        {
            return "Sprite should end at the position where it started.";
        }
    }

    public class Issue1288_2 : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(0);

            var spr = CCSprite.Create("Images/grossini");
            spr.Position = new CCPoint(100, 100);
            AddChild(spr);

            var act1 = CCMoveBy.Create(0.5f, new CCPoint(100, 0));
            spr.RunAction(CCRepeat.Create(act1, 1));
        }

        public override string title()
        {
            return "Issue 1288 #2";
        }

        public override string subtitle()
        {
            return "Sprite should move 100 pixels, and stay there";
        }
    }

    public class Issue1327 : ActionsDemo
    {
        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(0);

            var spr = CCSprite.Create("Images/grossini");
            spr.Position = new CCPoint(100, 100);
            AddChild(spr);

            var act1 = CCCallFuncN.Create(logSprRotation);
            var act2 = CCRotateBy.Create(0.25f, 45);
            var act3 = CCCallFuncN.Create(logSprRotation);
            var act4 = CCRotateBy.Create(0.25f, 45);
            var act5 = CCCallFuncN.Create(logSprRotation);
            var act6 = CCRotateBy.Create(0.25f, 45);
            var act7 = CCCallFuncN.Create(logSprRotation);
            var act8 = CCRotateBy.Create(0.25f, 45);
            var act9 = CCCallFuncN.Create(logSprRotation);

            var actF = CCSequence.Create(act1, act2, act3, act4, act5, act6, act7, act8, act9);
            spr.RunAction(actF);
        }

        private void logSprRotation(CCNode sender)
        {
            CCLog.Log("{0}", sender.Rotation);
        }

        public override string title()
        {
            return "Issue 1327";
        }

        public override string subtitle()
        {
            return "See console: You should see: 0, 45, 90, 135, 180";
        }
    }

    public class PauseResumeActions : ActionsDemo
    {
        private List<CCObject> m_pPausedTargets;

        public override void OnEnter()
        {
            base.OnEnter();

            centerSprites(2);

            m_tamara.RunAction(CCRepeatForever.Create(CCRotateBy.Create(3, 360)));
            m_grossini.RunAction(CCRepeatForever.Create(CCRotateBy.Create(3, -360)));
            m_kathia.RunAction(CCRepeatForever.Create(CCRotateBy.Create(3, 360)));

            ScheduleOnce(pause, 3);
            ScheduleOnce(resume, 5);
        }

        public override string title()
        {
            return "PauseResumeActions";
        }

        public override string subtitle()
        {
            return "All actions pause at 3s and resume at 5s";
        }

        private void pause(float dt)
        {
            CCLog.Log("Pausing");
            var director = CCDirector.SharedDirector;

            m_pPausedTargets = director.ActionManager.PauseAllRunningActions();
        }

        private void resume(float dt)
        {
            CCLog.Log("Resuming");
            var director = CCDirector.SharedDirector;
            director.ActionManager.ResumeTargets(m_pPausedTargets);
        }
    }
}