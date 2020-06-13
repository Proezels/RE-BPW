using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracks : MonoBehaviour
{
    public Shader _drawShader;
    private Material _snowMaterial, _drawMaterial;
    private RenderTexture _splatmap;
    public GameObject _terrain;
    RaycastHit _groundHit;
    int _layerMask;
    [Range(0, 2)]
    public float _brushSize;
    [Range(0, 1)]
    public float _brushStrength;
    
    
    void Start()
    {
        _layerMask = LayerMask.GetMask("ground");
        _drawMaterial = new Material (_drawShader);
        _snowMaterial = _terrain.GetComponent<MeshRenderer>().material;
        _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _snowMaterial.SetTexture("_splat", _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat));
          
    }

    void Update()
    {
        
        if (Physics.Raycast(transform.position, Vector3.down, out _groundHit, 1f, _layerMask))
            {
                _drawMaterial.SetVector("_coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat("_strength", _brushStrength);
                _drawMaterial.SetFloat("_size", _brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, temp);
                Graphics.Blit(temp, _splatmap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        
    }
}
