using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIParticleSystem : MaskableGraphic
{
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        int particlesCount = 50 ;
        for (int i = 0; i < particlesCount; i++)
        {
            Color vertexColor = Color.red ;

            UIVertex[] quadVerts = new UIVertex[4];
            for (int j = 0; j < 4; j++)
            {
                quadVerts[j] = new UIVertex()
                {

                    color = vertexColor,

                };
            }
            vh.AddUIVertexQuad(quadVerts);
        }

    }
    protected void Update()
    {
        SetVerticesDirty();
    }
}
