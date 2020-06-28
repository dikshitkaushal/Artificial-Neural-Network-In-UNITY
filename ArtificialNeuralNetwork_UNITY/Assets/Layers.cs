using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers
{
    public int numneurons;
    public List<Neuron> neurons = new List<Neuron>();

    public Layers(int nNeurons, int numNeuronInputs)
    {
        numneurons = nNeurons;
        for (int i = 0; i < nNeurons; i++)
        {
            neurons.Add(new Neuron(numNeuronInputs));
        }
    }
    /* public int numneurons;
     public List<Neuron> neurons = new List<Neuron>();

     public Layers(int nneurons,int NumNeuronsInputs)
     {
         for(int i=0;i<nneurons;i++)
         {
             neurons.Add(new Neuron(NumNeuronsInputs));
         }
     }*/
}
