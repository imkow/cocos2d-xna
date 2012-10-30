/****************************************************************************
Copyright (c) 2010-2012 cocos2d-x.org
Copyright (c) 2008-2010 Ricardo Quesada
Copyright (c) 2011 Zynga Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/

namespace cocos2d
{
    public class CCTransitionSlideInL : CCTransitionScene, ICCTransitionEaseScene
    {
        #region ICCTransitionEaseScene Members

        public CCFiniteTimeAction EaseAction(CCActionInterval action)
        {
            return CCEaseOut.Create(action, 2.0f);
        }

        #endregion

        /// <summary>
        /// initializes the scenes
        /// </summary>
        public virtual void InitScenes()
        {
            CCSize s = CCDirector.SharedDirector.WinSize;
            m_pInScene.Position = new CCPoint(-(s.width - 0.5f), 0);
        }

        /// <summary>
        /// returns the action that will be performed by the incomming and outgoing scene
        /// </summary>
        /// <returns></returns>
        public virtual CCActionInterval Action()
        {
            CCSize s = CCDirector.SharedDirector.WinSize;
            return CCMoveBy.Create(m_fDuration, new CCPoint(s.width - 0.5f, 0));
        }

        public override void OnEnter()
        {
            base.OnEnter();
            InitScenes();

            CCActionInterval incAction = Action();
            CCActionInterval outcAction = Action();

            CCFiniteTimeAction inAction = EaseAction(incAction);
            CCActionInterval outAction = CCSequence.Create
                (
                    EaseAction(outcAction),
                    CCCallFunc.Create((Finish))
                );
            m_pInScene.RunAction(inAction);
            m_pOutScene.RunAction(outAction);
        }

        public new static CCTransitionSlideInL Create(float t, CCScene scene)
        {
            var pScene = new CCTransitionSlideInL();
            pScene.InitWithDuration(t, scene);
            return pScene;
        }

        protected override void SceneOrder()
        {
            m_bIsInSceneOnTop = false;
        }
    }
}