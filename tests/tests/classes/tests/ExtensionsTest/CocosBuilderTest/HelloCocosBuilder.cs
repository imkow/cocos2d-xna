﻿using cocos2d;

namespace tests.Extensions
{
    public class HelloCocosBuilderLayer : BaseLayer
    {
        public CCSprite mBurstSprite;
        public CCLabelTTF mTestTitleLabelTTF;

        public override void OnNodeLoaded(CCNode node, CCNodeLoader nodeLoader)
        {
            CCRotateBy ccRotateBy = CCRotateBy.Create(20.0f, 360);
            CCRepeatForever ccRepeatForever = CCRepeatForever.Create(ccRotateBy);
            mBurstSprite.RunAction(ccRepeatForever);
        }

        public void openTest(string pCCBFileName, string pCCNodeName, CCNodeLoader pCCNodeLoader)
        {
            /* Create an autorelease CCNodeLoaderLibrary. */
            CCNodeLoaderLibrary ccNodeLoaderLibrary = CCNodeLoaderLibrary.NewDefaultCCNodeLoaderLibrary();

            ccNodeLoaderLibrary.RegisterCCNodeLoader("TestHeaderLayer", new Loader<TestHeaderLayer>());
            if (pCCNodeName != null && pCCNodeLoader != null)
            {
                ccNodeLoaderLibrary.RegisterCCNodeLoader(pCCNodeName, pCCNodeLoader);
            }

            /* Create an autorelease CCBReader. */
            var ccbReader = new CCBReader(ccNodeLoaderLibrary);

            /* Read a ccbi file. */
            // Load the scene from the ccbi-file, setting this class as
            // the owner will cause lblTestTitle to be set by the CCBReader.
            // lblTestTitle is in the TestHeader.ccbi, which is referenced
            // from each of the test scenes.
            CCNode node = ccbReader.ReadNodeGraphFromFile(pCCBFileName, this);

            mTestTitleLabelTTF.SetString(pCCBFileName);

            CCScene scene = CCScene.Create();
            scene.AddChild(node);

            /* Push the new scene with a fancy transition. */
            ccColor3B transitionColor;
            transitionColor.r = 0;
            transitionColor.g = 0;
            transitionColor.b = 0;

            CCDirector.SharedDirector.PushScene(CCTransitionFade.Create(0.5f, scene, transitionColor));
        }

        public void onMenuTestClicked(CCObject pSender, CCControlEvent pCCControlEvent)
        {
            openTest("ccb/ccb/TestMenus.ccbi", "TestMenusLayer", new Loader<MenuTestLayer>());
        }

        public void onSpriteTestClicked(CCObject pSender, CCControlEvent pCCControlEvent)
        {
            openTest("ccb/ccb/TestSprites.ccbi", "TestSpritesLayer", new Loader<SpriteTestLayer>());
        }

        public void onButtonTestClicked(CCObject pSender, CCControlEvent pCCControlEvent)
        {
            openTest("ccb/ccb/TestButtons.ccbi", "TestButtonsLayer", new Loader<ButtonTestLayer>());
        }

        public void onAnimationsTestClicked(CCObject pSender, CCControlEvent pCCControlEvent)
        {
            // Load node graph (TestAnimations is a sub class of CCLayer) and retrieve the ccb action manager
            CCBAnimationManager actionManager = null;

            /* Create an autorelease CCNodeLoaderLibrary. */
            CCNodeLoaderLibrary ccNodeLoaderLibrary = CCNodeLoaderLibrary.NewDefaultCCNodeLoaderLibrary();

            ccNodeLoaderLibrary.RegisterCCNodeLoader("TestHeaderLayer", new Loader<TestHeaderLayer>());
            ccNodeLoaderLibrary.RegisterCCNodeLoader("TestAnimationsLayer", new Loader<AnimationsTestLayer>());


            /* Create an autorelease CCBReader. */
            var ccbReader = new CCBReader(ccNodeLoaderLibrary);

            /* Read a ccbi file. */
            // Load the scene from the ccbi-file, setting this class as
            // the owner will cause lblTestTitle to be set by the CCBReader.
            // lblTestTitle is in the TestHeader.ccbi, which is referenced
            // from each of the test scenes.
            CCNode animationsTest = ccbReader.ReadNodeGraphFromFile("ccb/ccb/TestAnimations.ccbi", this, ref actionManager);
            ((AnimationsTestLayer) animationsTest).setAnimationManager(actionManager);

            mTestTitleLabelTTF.SetString("TestAnimations.ccbi");

            CCScene scene = CCScene.Create();
            scene.AddChild(animationsTest);

            /* Push the new scene with a fancy transition. */
            ccColor3B transitionColor;
            transitionColor.r = 0;
            transitionColor.g = 0;
            transitionColor.b = 0;

            CCDirector.SharedDirector.PushScene(CCTransitionFade.Create(0.5f, scene, transitionColor));
        }

        public void onParticleSystemTestClicked(CCObject pSender, CCControlEvent pCCControlEvent)
        {
            openTest("ccb/ccb/TestParticleSystems.ccbi", "TestParticleSystemsLayer", new Loader<ParticleSystemTestLayer>());
        }

        public void onScrollViewTestClicked(CCObject pSender, CCControlEvent pCCControlEvent)
        {
            openTest("ccb/ccb/TestScrollViews.ccbi", "TestScrollViewsLayer", new Loader<ScrollViewTestLayer>());
        }
    }
}