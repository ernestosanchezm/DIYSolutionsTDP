using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateHitHelper : MonoBehaviour
{
    public GameObject objetoGolpeado;
    public GameObject objetoaGolpear;
    public List<float> positions;
    public List<float> rotations;
    public List<float> scales;
    public List<float> positionsDestino;
    public bool golpeado = false;

    void Start()
    {
        positions = new List<float>();
        rotations = new List<float>();
        scales = new List<float>();
        positionsDestino = new List<float>();
        objetoGolpeado = new GameObject();
        objetoaGolpear = new GameObject();
    }

    public void EjecutarMovimiento()
    {
        if (objetoaGolpear.transform.position.x == positions[0] &&
            objetoaGolpear.transform.position.y == positions[1] &&
            objetoaGolpear.transform.position.z == positions[2] &&
            objetoaGolpear.transform.rotation.x == rotations[0] &&
            objetoaGolpear.transform.rotation.y == rotations[1] &&
            objetoaGolpear.transform.rotation.z == rotations[2] &&
            objetoaGolpear.transform.localScale.x == scales[0] &&
            objetoaGolpear.transform.localScale.y == scales[1] &&
            objetoaGolpear.transform.localScale.z == scales[2])
        {
            transform.position += new Vector3(positionsDestino[0], positionsDestino[1], positionsDestino[2]);
            golpeado = true;
        }
    }
}
