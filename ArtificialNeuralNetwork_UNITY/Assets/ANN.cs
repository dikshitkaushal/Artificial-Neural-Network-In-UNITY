using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANN
{

    public int numInputs;
    public int numOutputs;
    public int numHidden;
    public int numNPerHidden;
    public double alpha;
    List<Layers> layers = new List<Layers>();

    public ANN(int nI, int nO, int nH, int nPH, double a)
    {
        numInputs = nI;
        numOutputs = nO;
        numHidden = nH;
        numNPerHidden = nPH;
        alpha = a;

        if (numHidden > 0)
        {
            layers.Add(new Layers(numNPerHidden, numInputs));

            for (int i = 0; i < numHidden - 1; i++)
            {
                layers.Add(new Layers(numNPerHidden, numNPerHidden));
            }

            layers.Add(new Layers(numOutputs, numNPerHidden));
        }
        else
        {
            layers.Add(new Layers(numOutputs, numInputs));
        }
    }

    public List<double> Go(List<double> inputValues, List<double> desiredOutput)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();

        if (inputValues.Count != numInputs)
        {
            Debug.Log("ERROR: Number of Inputs must be " + numInputs);
            return outputs;
        }

        inputs = new List<double>(inputValues);
        for (int i = 0; i < numHidden + 1; i++)
        {
            if (i > 0)
            {
                inputs = new List<double>(outputs);
            }
            outputs.Clear();

            for (int j = 0; j < layers[i].numneurons; j++)
            {
                double N = 0;
                layers[i].neurons[j].input.Clear();

                for (int k = 0; k < layers[i].neurons[j].numinput; k++)
                {
                    layers[i].neurons[j].input.Add(inputs[k]);
                    N += layers[i].neurons[j].weights[k] * inputs[k];
                }

                N -= layers[i].neurons[j].bias;
                layers[i].neurons[j].output = ActivationFunction(N);
                outputs.Add(layers[i].neurons[j].output);
            }
        }

        UpdateWeights(outputs, desiredOutput);

        return outputs;
    }

    void UpdateWeights(List<double> outputs, List<double> desiredOutput)
    {
        double error;
        for (int i = numHidden; i >= 0; i--)
        {
            for (int j = 0; j < layers[i].numneurons; j++)
            {
                if (i == numHidden)
                {
                    error = desiredOutput[j] - outputs[j];
                    layers[i].neurons[j].errorgradient = outputs[j] * (1 - outputs[j]) * error;
                    //errorGradient calculated with Delta Rule: en.wikipedia.org/wiki/Delta_rule
                }
                else
                {
                    layers[i].neurons[j].errorgradient = layers[i].neurons[j].output * (1 - layers[i].neurons[j].output);
                    double errorGradSum = 0;
                    for (int p = 0; p < layers[i + 1].numneurons; p++)
                    {
                        errorGradSum += layers[i + 1].neurons[p].errorgradient * layers[i + 1].neurons[p].weights[j];
                    }
                    layers[i].neurons[j].errorgradient *= errorGradSum;
                }
                for (int k = 0; k < layers[i].neurons[j].numinput; k++)
                {
                    if (i == numHidden)
                    {
                        error = desiredOutput[j] - outputs[j];
                        layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].input[k] * error;
                    }
                    else
                    {
                        layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].input[k] * layers[i].neurons[j].errorgradient;
                    }
                }
                layers[i].neurons[j].bias += alpha * -1 * layers[i].neurons[j].errorgradient;
            }

        }

    }

    //for full list of activation functions
    //see en.wikipedia.org/wiki/Activation_function
    double ActivationFunction(double value)
    {
        return Sigmoid(value);
    }

    double Step(double value) //(aka binary step)
    {
        if (value < 0) return 0;
        else return 1;
    }

    double Sigmoid(double value) //(aka logistic softstep)
    {
        double k = (double)System.Math.Exp(value);
        return k / (1.0f + k);
    }
}

/*using System.Collections;
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
    public List<double>Go(List<double>inputsvalues,List<double>desired_outputs)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        if(numinput!=inputsvalues.Count)
        {
            Debug.Log("The number of inputs should be : " + numinput);
            return outputs;
        }
        inputs = new List<double>(inputsvalues);

        for (int i=0;i<numhiddenlayer+1;i++)
        {
            if(i>0)
            {
                inputs = new List<double>(outputs);
            }
            outputs.Clear();
            for(int j=0;j<layers[i].numneurons;j++)
            {
                double N = 0;

                Debug.Log("start1");
                layers[i].neurons[j].input.Clear();
                for(int k=0;k<layers[i].neurons[j].numinput;k++)
                {
                    layers[i].neurons[j].input.Add(inputs[k]);
                    N += layers[i].neurons[j].weights[k] * inputs[k];
                }
                N -= layers[i].neurons[j].bias;
                layers[i].neurons[j].output = ActivationFunction(N);
                outputs.Add(layers[i].neurons[j].output);
            }
        }

        updateweights(outputs, desired_outputs);
        return outputs;
    }
    public void updateweights(List<double>output,List<double>desiredoutput)
    {
        double error;
        for(int i=numhiddenlayer;i>=0;i--)
        {
            for(int j=0;j<layers[i].numneurons;j++)
            {
                if(i==numhiddenlayer)
                {
                    error = desiredoutput[j] - output[j];
                    layers[i].neurons[j].errorgradient = output[j] * (1 - output[j]) * error;
                }
                else
                {
                    layers[i].neurons[j].errorgradient = layers[i].neurons[j].output - (1 - layers[i].neurons[j].output);
                    double errorgradsum = 0;
                    for(int p=0;p<layers[i+1].numneurons;p++)
                    {
                        errorgradsum += layers[i + 1].neurons[p].errorgradient * layers[i + 1].neurons[p].weights[j];
                    }
                    layers[i].neurons[j].errorgradient += errorgradsum;
                }
                for(int k=0;k<layers[i].neurons[j].numinput;k++)
                {
                    if(i==numhiddenlayer)
                    {
                        error = output[j] - desiredoutput[j];
                        layers[i].neurons[j].weights[k] += alpha*layers[i].neurons[j].input[k] * error;
                    }
                    else
                    {

                        layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].input[k] * layers[i].neurons[j].errorgradient;
                    }
                    layers[i].neurons[j].bias += alpha * -1 * layers[i].neurons[j].errorgradient;
                }
            }
        }
    }
    double step(double value)
    {
        if (value > 0) return 1;
        return 0;
    }
    double sigmoid(double value)
    {
        double k = (double)System.Math.Exp(value);
        return k / (1.0f + k);
    }
    double ActivationFunction(double value)
    {
        return sigmoid(value);
    }
}
*/
