using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANN 
{
    public int numinput;
    public int numoutput;
    public int numhiddenlayer;
    public int numperhiddenlayer;
    public double alpha;                    //to set the speed of the ANN (Tells the model which training set should affect the model) 
    List<Layers> layers = new List<Layers>();

    public ANN(int ni,int no,int nhl,int nphl,double a)
    {
        numinput = ni;
        numoutput = no;
        numhiddenlayer = nhl;
        numperhiddenlayer = nphl;
        alpha = a;
        if(nhl>0)
        {
            layers.Add(new Layers(numperhiddenlayer, numinput));
            for(int i=0;i<numhiddenlayer-1;i++)
            {
                layers.Add(new Layers(numperhiddenlayer, numperhiddenlayer));
            }
            layers.Add(new Layers(numoutput, numperhiddenlayer));
        }
        else
        {
            layers.Add(new Layers(numoutput, numinput));
        }
    }
}
