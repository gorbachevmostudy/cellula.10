using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class UIParticleSystem : MaskableGraphic
public class Particle : MonoBehaviour
{
    //[SerializeField] private ParticleSystemRenderer _particleSystemRenderer;
    //[SerializeField] private Camera _bakeCamera;

    //[SerializeField] private Texture _texture;

    //public override Texture mainTexture => _texture ?? base.mainTexture; 

    //private void FixedUpdate()
    //{
    //    SetVerticesDirty();
    //}

    //protected override void OnPopulateMesh(Mesh mesh)
    //{
    //    mesh.Clear();
    //    if (_particleSystemRenderer != null && _bakeCamera != null)
    //        _particleSystemRenderer.BakeMesh(mesh, _bakeCamera);
    //}




    private Vector3 vecScale;
    private float camsize;
    // start is called before the first frame update
    void Start()
    {

    }

    // update is called once per frame
    void FixedUpdate()
    {
        camsize = GameObject.FindGameObjectWithTag("player").GetComponent<AgarController>().camSize;  // при отдалении камеры размер модели будет увеличиваться
        vecScale.Set((camsize / 5f), (camsize / 5f), (camsize / 5f));// (camsize / 5f));
        transform.localScale = vecScale;
    }
}
