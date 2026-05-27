using UnityEngine;
using Unity.Cinemachine;

public class Transport : MonoBehaviour
{

    public Rigidbody2D rgby2d;
    public CinemachineCamera camaraVirtual;

    public void ChangeArea(Transform areaDestino)
    {
        Vector3 nextPosicion = areaDestino.position;
        nextPosicion.z = this.transform.position.z; 
        Vector3 deltaMovimiento = nextPosicion - this.transform.position;

        this.transform.position = nextPosicion;
        rgby2d.linearVelocity = Vector2.zero;

        if (camaraVirtual != null)
        {
            camaraVirtual.OnTargetObjectWarped(this.transform, deltaMovimiento);
        }
    }
}
