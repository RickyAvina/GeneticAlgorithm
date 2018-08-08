using System;

/* DNA is represetnative of a single object, a car, a dot, a parser etc. */

public class DNA<T>
{ // template variables
    public T[] Genes { get; private set; }
    public float Fitness { get; private set; }

    private Random random; // random number sequence needs to be defined only once
    private Func<T> getRandomGene;
    public Func<float, int> fitnessFunction; // generic fitness function

    public DNA(int size, Random random, Func<T> getRandomGene, Func<float, int> fitnessFunction, bool shouldInitGenes = true) // Func<t> is a reference to a function that generate random objects of type T
    {
        Genes = new T[size];
        this.random = random;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        if (shouldInitGenes) { // to provent double random initialization in Crossover
            for (int i = 0; i < Genes.Length; i++) // start with random genes
            {
                    Genes[i] = getRandomGene();
            }
        }
    }

    public float CalculateFitness(int index) // natural selection, determine which individuals to reproduce
    {
        Fitness = fitnessFunction(index);
        return Fitness;
    }

    public DNA<T> Crossover(DNA<T> otherParent) // reproduction, returning new child
    {
        DNA<T> child = new DNA<T>(Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);

        for (int i = 0; i < Genes.Length; i++) // combine genes of parents
        {
            child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i]: otherParent.Genes[i];
        }

        return child;
    }

    public void Mutate(float mutationRate) // how likely it is to mutate genes
    { 
        for (int i = 0; i < Genes.Length; i++)
        {
            if (random.NextDouble() < mutationRate) // assign random gene
            {
                Genes[i] = getRandomGene();
            }
        }
    }
}
