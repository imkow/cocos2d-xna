namespace cocos2d
{
    public class CCMoveTo : CCActionInterval
    {
        protected CCPoint m_delta;
        protected CCPoint m_endPosition;
        protected CCPoint m_startPosition;

        public bool InitWithDuration(float duration, CCPoint position)
        {
            if (base.InitWithDuration(duration))
            {
                m_endPosition = position;
                return true;
            }

            return false;
        }

        public override CCObject CopyWithZone(CCZone zone)
        {
            CCZone tmpZone = zone;
            CCMoveTo ret;

            if (tmpZone != null && tmpZone.m_pCopyObject != null)
            {
                ret = (CCMoveTo) tmpZone.m_pCopyObject;
            }
            else
            {
                ret = new CCMoveTo();
                tmpZone = new CCZone(ret);
            }

            base.CopyWithZone(tmpZone);
            ret.InitWithDuration(m_fDuration, m_endPosition);

            return ret;
        }

        public override void StartWithTarget(CCNode target)
        {
            base.StartWithTarget(target);
            m_startPosition = target.Position;
            m_delta = CCPointExtension.ccpSub(m_endPosition, m_startPosition);
        }

        public override void Update(float time)
        {
            if (m_pTarget != null)
            {
                m_pTarget.Position = CCPointExtension.ccp(m_startPosition.x + m_delta.x * time,
                                                          m_startPosition.y + m_delta.y * time);
            }
        }

        public static CCMoveTo Create(float duration, CCPoint position)
        {
            var moveTo = new CCMoveTo();
            moveTo.InitWithDuration(duration, position);

            return moveTo;
        }
    }
}