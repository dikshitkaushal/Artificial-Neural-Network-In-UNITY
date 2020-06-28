using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron
{
    public int numinput;
    public double bias;
    public double output;
    public double errorgradient;
    public List<double> weights = new List<double>();
    public List<double> input = new List<double>();

    public Neuron(int nInputs)
    {
        bias = UnityEngine.Random.Range(-1.0f, 1.0f);
        numinput = nInputs;
        for (int i = 0; i < nInputs; i++)
            weights.Add(UnityEngine.Random.Range(-1.0f, 1.0f));
    }
    /* public int numinput;
     public double bias;
     public double output;
     public double errorgradient;
     public List<double> weights = new List<double>();
     public List<double> input = new List<double>();

     public Neuron(int ninput)
     {
         bias = Random.Range(-1.0f, 1.0f);
         numinput = ninput;
         for(int i=0;i<ninput;i++)
         {
             weights.Add(Random.Range(-1.0f, 1.0f));
         }    
     }*/
}
