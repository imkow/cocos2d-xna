
namespace cocos2d
{
    public class CCEaseBounceIn : CCEaseBounce
    {
        public override void Update(float time)
        {
            float newT = 1 - BounceTime(1 - time);
            m_pOther.Update(newT);
        }

        public override CCFiniteTimeAction Reverse()
        {
            return CCEaseBounceOut.Create((CCActionInterval) m_pOther.Reverse());
        }

        public override CCObject CopyWithZone(CCZone pZone)
        {
            CCEaseBounceIn pCopy;

            if (pZone != null && pZone.m_pCopyObject != null)
            {
                //in case of being called at sub class
                pCopy = pZone.m_pCopyObject as CCEaseBounceIn;
            }
            else
            {
                pCopy = new CCEaseBounceIn();
            }

            pCopy.InitWithAction((CCActionInterval) (m_pOther.Copy()));

            return pCopy;
        }

        public new static CCEaseBounceIn Create(CCActionInterval pAction)
        {
            var pRet = new CCEaseBounceIn();
            pRet.InitWithAction(pAction);
            return pRet;
        }
    }
}