
using System;

namespace cocos2d
{
    /// <summary>
    /// CCOrbitCamera action
    /// Orbits the camera around the center of the screen using spherical coordinates
    /// </summary>
    public class CCOrbitCamera : CCActionCamera
    {
        protected float m_fAngleX;
        protected float m_fAngleZ;
        protected float m_fDeltaAngleX;
        protected float m_fDeltaAngleZ;
        protected float m_fDeltaRadius;
        protected float m_fRadDeltaX;
        protected float m_fRadDeltaZ;
        protected float m_fRadX;
        protected float m_fRadZ;
        protected float m_fRadius;

        public CCOrbitCamera()
        {
            m_fRadius = 0.0f;
            m_fDeltaRadius = 0.0f;
            m_fAngleZ = 0.0f;
            m_fDeltaAngleZ = 0.0f;
            m_fAngleX = 0.0f;
            m_fDeltaAngleX = 0.0f;
            m_fRadZ = 0.0f;
            m_fRadDeltaZ = 0.0f;
            m_fRadX = 0.0f;
            m_fRadDeltaX = 0.0f;
        }

        public static CCOrbitCamera Create(float t, float radius, float deltaRadius, float angleZ, float deltaAngleZ, float angleX,
                                                       float deltaAngleX)
        {
            var pRet = new CCOrbitCamera();
            pRet.InitWithDuration(t, radius, deltaRadius, angleZ, deltaAngleZ, angleX, deltaAngleX);
            return pRet;
        }

        public bool InitWithDuration(float t, float radius, float deltaRadius, float angleZ, float deltaAngleZ, float angleX, float deltaAngleX)
        {
            if (InitWithDuration(t))
            {
                m_fRadius = radius;
                m_fDeltaRadius = deltaRadius;
                m_fAngleZ = angleZ;
                m_fDeltaAngleZ = deltaAngleZ;
                m_fAngleX = angleX;
                m_fDeltaAngleX = deltaAngleX;

                m_fRadDeltaZ = ccMacros.CC_DEGREES_TO_RADIANS(deltaAngleZ);
                m_fRadDeltaX = ccMacros.CC_DEGREES_TO_RADIANS(deltaAngleX);
                return true;
            }

            return false;
        }

        public void SphericalRadius(out float newRadius, out float zenith, out float azimuth)
        {
            float ex, ey, ez, cx, cy, cz, x, y, z;
            float r; // radius
            float s;

            CCCamera pCamera = m_pTarget.Camera;
            pCamera.GetEyeXyz(out ex, out ey, out ez);
            pCamera.GetCenterXyz(out cx, out cy, out cz);

            x = ex - cx;
            y = ey - cy;
            z = ez - cz;

            r = (float) Math.Sqrt((float) Math.Pow(x, 2) + (float) Math.Pow(y, 2) + (float) Math.Pow(z, 2));
            s = (float) Math.Sqrt((float) Math.Pow(x, 2) + (float) Math.Pow(y, 2));
            if (s == 0.0f)
                s = ccMacros.FLT_EPSILON;
            if (r == 0.0f)
                r = ccMacros.FLT_EPSILON;

            zenith = (float) Math.Acos(z / r);
            if (x < 0)
                azimuth = (float) Math.PI - (float) Math.Sin(y / s);
            else
                azimuth = (float) Math.Sin(y / s);

            newRadius = r / CCCamera.GetZEye();
        }

        public override CCObject CopyWithZone(CCZone pZone)
        {
            CCOrbitCamera pRet;

            if (pZone != null && pZone.m_pCopyObject != null) //in case of being called at sub class
                pRet = (CCOrbitCamera) (pZone.m_pCopyObject);
            else
            {
                pRet = new CCOrbitCamera();
                pZone = new CCZone();
            }

            base.CopyWithZone(pZone);

            pRet.InitWithDuration(m_fDuration, m_fRadius, m_fDeltaRadius, m_fAngleZ, m_fDeltaAngleZ, m_fAngleX, m_fDeltaAngleX);

            return pRet;
        }

        public override void StartWithTarget(CCNode target)
        {
            base.StartWithTarget(target);

            float r, zenith, azimuth;
            SphericalRadius(out r, out zenith, out azimuth);

            if (float.IsNaN(m_fRadius))
                m_fRadius = r;

            if (float.IsNaN(m_fAngleZ))
                m_fAngleZ = ccMacros.CC_RADIANS_TO_DEGREES(zenith);
            
            if (float.IsNaN(m_fAngleX))
                m_fAngleX = ccMacros.CC_RADIANS_TO_DEGREES(azimuth);

            m_fRadZ = ccMacros.CC_DEGREES_TO_RADIANS(m_fAngleZ);
            m_fRadX = ccMacros.CC_DEGREES_TO_RADIANS(m_fAngleX);
        }

        public override void Update(float time)
        {
            float r = (m_fRadius + m_fDeltaRadius * time) * CCCamera.GetZEye();
            float za = m_fRadZ + m_fRadDeltaZ * time;
            float xa = m_fRadX + m_fRadDeltaX * time;

            float i = (float) Math.Sin(za) * (float) Math.Cos(xa) * r + m_fCenterXOrig;
            float j = (float) Math.Sin(za) * (float) Math.Sin(xa) * r + m_fCenterYOrig;
            float k = (float) Math.Cos(za) * r + m_fCenterZOrig;

            m_pTarget.Camera.SetEyeXyz(i, j, k);
        }
    }
}