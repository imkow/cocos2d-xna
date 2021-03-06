using System;

namespace cocos2d
{
    public class CCBezierBy : CCActionInterval
    {
        protected ccBezierConfig m_sConfig;
        protected CCPoint m_startPosition;

        public bool InitWithDuration(float t, ccBezierConfig c)
        {
            if (base.InitWithDuration(t))
            {
                m_sConfig = c;
                return true;
            }

            return false;
        }

        public override CCObject CopyWithZone(CCZone zone)
        {
            CCZone tmpZone = zone;
            CCBezierBy ret;

            if (tmpZone != null && tmpZone.m_pCopyObject != null)
            {
                ret = tmpZone.m_pCopyObject as CCBezierBy;
                if (ret == null)
                {
                    return null;
                }
            }
            else
            {
                ret = new CCBezierBy();
                tmpZone = new CCZone(ret);
            }

            base.CopyWithZone(tmpZone);

            ret.InitWithDuration(m_fDuration, m_sConfig);

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
                float xa = 0;
                float xb = m_sConfig.ControlPoint1.x;
                float xc = m_sConfig.ControlPoint2.x;
                float xd = m_sConfig.EndPosition.x;

                float ya = 0;
                float yb = m_sConfig.ControlPoint1.y;
                float yc = m_sConfig.ControlPoint2.y;
                float yd = m_sConfig.EndPosition.y;

                float x = Bezierat(xa, xb, xc, xd, time);
                float y = Bezierat(ya, yb, yc, yd, time);
                m_pTarget.Position = CCPointExtension.ccpAdd(m_startPosition, CCPointExtension.ccp(x, y));
            }
        }

        public override CCFiniteTimeAction Reverse()
        {
            ccBezierConfig r;

            r.EndPosition = CCPointExtension.ccpNeg(m_sConfig.EndPosition);
            r.ControlPoint1 = CCPointExtension.ccpAdd(m_sConfig.ControlPoint2, CCPointExtension.ccpNeg(m_sConfig.EndPosition));
            r.ControlPoint2 = CCPointExtension.ccpAdd(m_sConfig.ControlPoint1, CCPointExtension.ccpNeg(m_sConfig.EndPosition));

            CCBezierBy action = Create(m_fDuration, r);
            return action;
        }

        public static CCBezierBy Create(float t, ccBezierConfig c)
        {
            var ret = new CCBezierBy();
            ret.InitWithDuration(t, c);

            return ret;
        }

        // Bezier cubic formula:
        //	((1 - t) + t)3 = 1 
        // Expands to�� 
        //   (1 - t)3 + 3t(1-t)2 + 3t2(1 - t) + t3 = 1 
        protected float Bezierat(float a, float b, float c, float d, float t)
        {
            return (float) ((Math.Pow(1 - t, 3) * a +
                             3 * t * (Math.Pow(1 - t, 2)) * b +
                             3 * Math.Pow(t, 2) * (1 - t) * c +
                             Math.Pow(t, 3) * d));
        }
    }

    public struct ccBezierConfig
    {
        public CCPoint ControlPoint1;
        public CCPoint ControlPoint2;
        public CCPoint EndPosition;
    }
}