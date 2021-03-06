namespace cocos2d
{
    public class CCJumpBy : CCActionInterval
    {
        protected CCPoint m_delta;
        protected float m_height;
        protected uint m_nJumps;
        protected CCPoint m_startPosition;

        public bool InitWithDuration(float duration, CCPoint position, float height, uint jumps)
        {
            if (base.InitWithDuration(duration))
            {
                m_delta = position;
                m_height = height;
                m_nJumps = jumps;

                return true;
            }

            return false;
        }

        public override CCObject CopyWithZone(CCZone zone)
        {
            CCZone tmpZone = zone;
            CCJumpBy ret;

            if (tmpZone != null && tmpZone.m_pCopyObject != null)
            {
                ret = tmpZone.m_pCopyObject as CCJumpBy;
                if (ret == null)
                {
                    return null;
                }
            }
            else
            {
                ret = new CCJumpBy();
                tmpZone = new CCZone(ret);
            }

            base.CopyWithZone(tmpZone);

            ret.InitWithDuration(m_fDuration, m_delta, m_height, m_nJumps);

            return ret;
        }

        public override void StartWithTarget(CCNode target)
        {
            base.StartWithTarget(target);
            m_startPosition = target.Position;
        }

        public override void Update(float time)
        {
            if (m_pTarget != null)
            {
                // Is % equal to fmodf()???
                float frac = (time * m_nJumps) % 1f;
                float y = m_height * 4f * frac * (1f - frac);
                y += m_delta.y * time;
                float x = m_delta.x * time;
                m_pTarget.Position = new CCPoint(m_startPosition.x + x, m_startPosition.y + y);
            }
        }

        public override CCFiniteTimeAction Reverse()
        {
            return Create(m_fDuration, new CCPoint(-m_delta.x, -m_delta.y), m_height, m_nJumps);
        }

        public static CCJumpBy Create(float duration, CCPoint position, float height, uint jumps)
        {
            var ret = new CCJumpBy();
            ret.InitWithDuration(duration, position, height, jumps);
            return ret;
        }
    }
}