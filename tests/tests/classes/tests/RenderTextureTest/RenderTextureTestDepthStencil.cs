using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using cocos2d;
using Random = cocos2d.Random;

namespace tests
{
    public class RenderTextureTestDepthStencil : RenderTextureTestDemo
    {
        public RenderTextureTestDepthStencil()
        {
            CCSize s = CCDirector.SharedDirector.WinSize;

            CCSprite sprite = CCSprite.Create("Images/fire");
            sprite.Position = new CCPoint(s.width * 0.25f, 0);
            sprite.Scale = 10;
            CCRenderTexture rend = CCRenderTexture.Create((int)s.width, (int)s.height, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, RenderTargetUsage.DiscardContents);

            rend.BeginWithClear(0, 0, 0, 0, 0);

            var save = DrawManager.DepthStencilState;

            DrawManager.DepthStencilState = new DepthStencilState()
                {
                    ReferenceStencil = 1,

                    DepthBufferEnable = false,
                    StencilEnable = true,
                    StencilFunction = CompareFunction.Always,
                    StencilPass = StencilOperation.Replace,
                    
                    TwoSidedStencilMode = true,
                    CounterClockwiseStencilFunction = CompareFunction.Always,
                    CounterClockwiseStencilPass = StencilOperation.Replace,
                };

            sprite.Visit();

            DrawManager.DepthStencilState = new DepthStencilState()
            {
                DepthBufferEnable = false,
                StencilEnable = true,
                StencilFunction = CompareFunction.NotEqual,
                StencilPass = StencilOperation.Keep,
                ReferenceStencil = 1
            };
            DrawManager.BlendFunc(new ccBlendFunc(OGLES.GL_ONE, OGLES.GL_ONE_MINUS_SRC_ALPHA));
            
            //! move sprite half width and height, and draw only where not marked
            sprite.Position = sprite.Position + new CCPoint(sprite.ContentSize.width * sprite.Scale, sprite.ContentSize.height * sprite.Scale) * 0.5f;

            sprite.Visit();

            DrawManager.DepthStencilState = save;

            rend.End();


            rend.Position = new CCPoint(s.width * 0.5f, s.height * 0.5f);

            AddChild(rend);
        }

        public override string title()
        {
            return "Testing depthStencil attachment";
        }

        public override string subtitle()
        {
            return "Circle should be missing 1/4 of its region";
        }
    }
}