namespace cocos2d
{
    public class CCSkewTo : CCActionInterval
    {
        protected float m_fDeltaX;
        protected float m_fDeltaY;
        protected float m_fEndSkewX;
        protected float m_fEndSkewY;
        protected float m_fSkewX;
        protected float m_fSkewY;
        protected float m_fStartSkewX;
        protected float m_fStartSkewY;

        public virtual bool InitWithDuration(float t, float sx, float sy)
        {
            bool bRet = false;

            if (base.InitWithDuration(t))
            {
                m_fEndSkewX = sx;
                m_fEndSkewY = sy;

                bRet = true;
            }

            return bRet;
        }

        public override CCObject CopyWithZone(CCZone pZone)
        {
            CCSkewTo pCopy;

            if (pZone != null && pZone.m_pCopyObject != null)
            {
                //in case of being called at sub class
                pCopy = (CCSkewTo) (pZone.m_pCopyObject);
            }
            else
            {
                pCopy = new CCSkewTo();
                pZone = new CCZone(pCopy);
            }

            base.CopyWithZone(pZone);

            pCopy.InitWithDuration(m_fDuration, m_fEndSkewX, m_fEndSkewY);

            return pCopy;
        }

        public override void StartWithTarget(CCNode target)
        {
            base.StartWithTarget(target);

            m_fStartSkewX = target.SkewX;

            if (m_fStartSkewX > 0)
            {
                m_fStartSkewX = m_fStartSkewX % 180f;
            }
            else
            {
                m_fStartSkewX = m_fStartSkewX % -180f;
            }

            m_fDeltaX = m_fEndSkewX - m_fStartSkewX;

            if (m_fDeltaX > 180)
            {
                m_fDeltaX -= 360;
            }
            if (m_fDeltaX < -180)
            {
                m_fDeltaX += 360;
            }

            m_fStartSkewY = target.SkewY;

            if (m_fStartSkewY > 0)
            {
                m_fStartSkewY = m_fStartSkewY % 360f;
            }
            else
            {
                m_fStartSkewY = m_fStartSkewY % -360f;
            }

            m_fDeltaY = m_fEndSkewY - m_fStartSkewY;

            if (m_fDeltaY > 180)
            {
                m_fDeltaY -= 360;
            }
            if (m_fDeltaY < -180)
            {
                m_fDeltaY += 360;
            }
        }

        public override void Update(float time)
        {
            m_pTarget.SkewX = m_fStartSkewX + m_fDeltaX * time;
            m_pTarget.SkewY = m_fStartSkewY + m_fDeltaY * time;
        }

        public static CCSkewTo Create(float t, float sx, float sy)
        {
            var pSkewTo = new CCSkewTo();
            pSkewTo.InitWithDuration(t, sx, sy);
            return pSkewTo;
        }
    }
}